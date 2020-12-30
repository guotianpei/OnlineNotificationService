using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;
using ONP.BackendProcessor.Models;
using System.Threading.Tasks;

namespace ONP.BackendProcessor.Services
{
    public class EmailService
    {
        internal async Tasks<NotificationResponse> SendMail(EmailRequest mailrequest)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            //AddMailProperties(mailrequest, null, message);

            message.From = new System.Net.Mail.MailAddress(mailrequest.From);
            message.Subject = mailrequest.subject;
            message.Body = mailrequest.Message;
            message.Priority = mailrequest.Priority;
            message.IsBodyHtml = mailrequest.IsBodyHtml;
            message.To.Add(mailrequest.To);

            SmtpClient client = new SmtpClient();
            try
            {
                client.Host = "email-smtp.us-east-1.amazonaws.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential("AKIAV2JBCNC55R4PJIXB", "BMwZ2wcrGmqKgZf0CaI66kbnjibSJlIgvlmaVBPfeOP3");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(message);               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
