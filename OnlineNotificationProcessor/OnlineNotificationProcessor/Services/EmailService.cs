using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Reflection;
using Microsoft.Extensions.Options;
using ONP.BackendProcessor.Models;
using System.Threading.Tasks;
using ONP.BackendProcessor.Configuration;
using ONP.Infrastructure.Repositories.Interfaces;
using ONP.Domain.Events;
using ONP.Domain.Models;
using ONP.BackendProcessor.Tasks;
using Polly;
using System.ComponentModel;
using ONP.Domain.Seedwork;



namespace ONP.BackendProcessor.Services
{
    public class EmailService : Entity, IEmailService
    {

        private readonly EmailServiceConfig _configuration;
        private static readonly int NUMBER_OF_RETRIES = 3;
        private List<NotificationResponse> _responseList;
        private readonly INotificationRequestRepository _requestRepository;
        //private readonly IEntityProfileRepository _entityRepository;

        public EmailService(IOptions<EmailServiceConfig> emailConfig,
        INotificationRequestRepository notificationRequestRepository)
        //IEntityProfileRepository entityRepository)
        {
            _configuration = emailConfig.Value;
            _requestRepository = notificationRequestRepository ?? throw new ArgumentNullException(nameof(notificationRequestRepository));
            //_templateRepository = templateRepository;
            //_entityRepository = entityRepository;
        }



        public void SendMailAsync(List<NotificationData> lstNotificationData)
        {
            SmtpClient client = new SmtpClient();
            client.Host = _configuration.MailServerHost;
            client.Credentials = new System.Net.NetworkCredential(_configuration.ACCESS_KEY, _configuration.SECRET_KEY);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

            //Return Notification Response.
            List<NotificationResponse> lstresponse = new List<NotificationResponse>();
            try
            {
                foreach (var mailrequest in lstNotificationData)
                {
                    message.From = new System.Net.Mail.MailAddress(mailrequest.From);
                    message.Subject = mailrequest.Subject;
                    message.Body = mailrequest.RequestMessageData;
                    //message.Priority = mailrequest.Priority;
                    // message.IsBodyHtml = mailrequest.IsBodyHtml;
                    message.To.Add(mailrequest.To);

                    // The userState can be any object that allows your callback
                    // method to identify this send operation.
                    
                    object userState = mailrequest;
                    // Set the method that is called back when the send operation ends.
                    client.SendCompleted += new
                    SendCompletedEventHandler(SendCompletedCallback);

                    //Specify Polly retry Policy
                    var retry = Policy
                        // TO-DO: Update condition check to GetRetrySmtpStatusCodes
                        .Handle<SmtpFailedRecipientException>(ex => ex.StatusCode == SmtpStatusCode.MailboxUnavailable)
                        //.Or<SmtpException>() No need retry
                        .WaitAndRetry(NUMBER_OF_RETRIES, currentRetry => TimeSpan.FromSeconds(Math.Pow(2, currentRetry)));

                    try
                    {
                        retry.Execute(() =>
                        {
                           client.SendAsync(message, userState);
                        });

                    }
                    catch (SmtpFailedRecipientsException ex)
                    {
                        //then handler it with event 
                        HandleFailedStatusCode(mailrequest, ex.StatusCode, ex);
                    }
                    catch (Exception ex)
                    {
                        HandleFailedStatusCode(mailrequest, SmtpStatusCode.GeneralFailure,  ex);
                    }
                    finally
                    {
                       
                        //some clean up.
                        message.Dispose();
                        //TO-DO: Update stage 
                       
                    }
                }
            }
            catch (Exception ex)
            { }
            finally
             {
                

            }


        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            //TO-DO: Logging
            
            // Get the request for this asynchronous operation.
            NotificationData requestData = (NotificationData)e.UserState;
            NotificationResponse response = new NotificationResponse();            

            if (e.Cancelled)
            {
                response.ResponseCode = "Send canceled.";
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", requestData.TrackingID, e.Error.ToString());
                Exception ex = e.Error;
                if (ex is SmtpException smtpException)
                {
                    response.ResponseCode = smtpException.StatusCode.ToString();
                    response.Error = smtpException;
                }
            }
            else //"Mail sent successfully"  
            {
                Console.WriteLine("Send success: {0}]  ", requestData.TrackingID);
                response.ResponseCode = SmtpStatusCode.Ok.ToString();
            }
            response.TrackingID = requestData.TrackingID;
            _responseList.Add(response);
            //Raise domain event to update status.
            RequestCompletedDomainEvent requestComplated = new RequestCompletedDomainEvent(requestData.TrackingID, response.ResponseCode, response.Error);
            this.AddDomainEvent(requestComplated);

        }

        private void HandleFailedStatusCode(NotificationData request,  SmtpStatusCode statuscode=SmtpStatusCode.GeneralFailure, Exception exception=null)
        {

            if (GetfailureNotifyingCodes().Contains(statuscode))
            {
                //Raise PublishFailedDomainEvent   as side effect.
                var failedEvent = new PublishFailedDomainEvent(request.TrackingID, request.To, 
                    request.RequestMessageData, ComChannelTypes.Email, statuscode, exception);
                AddDomainEvent(failedEvent);
            }
            
        }

        //TO-GO: Load during application start
        public List<SmtpStatusCode> GetfailureNotifyingCodes()
        {
            List<SmtpStatusCode> codes = new List<SmtpStatusCode>();
            return codes;
        }

    }
}
