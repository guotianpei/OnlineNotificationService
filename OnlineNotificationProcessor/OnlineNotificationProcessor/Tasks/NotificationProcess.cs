using ONP.BackendProcessor.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;


namespace ONP.BackendProcessor.Tasks
{
    public class NotificationProcess
    {
        ISendNotification _sendnotificatoin;
        public NotificationProcess(ISendNotification objSN)
        {
            _sendnotificatoin = objSN;
        }
        public void SendNotification(List<NotificationRequest> lstPendingReq)
        {
            NotificationResponse notificationResponse;

            foreach (NotificationRequest notificationReq in lstPendingReq)
            {
                notificationResponse = _sendnotificatoin.SendNotification(BuildNotificationData(notificationReq));

                if (notificationResponse != null )
                {

                    //Domain event should be triggered here.
                    if (notificationResponse.NotificationStage == NotificationStage.Success)
                    {
                        //Update request status completed
                        //Update lstPendingReq

                    }
                    else if (notificationResponse.NotificationStage == NotificationStage.Failed)
                    {
                        //Raise Domain event to update
                        //Update request status failed and lof failed reason
                    }
                }
            }            
        }

        NotificationData BuildNotificationData(NotificationRequest req)
        {
            try
            {
                NotificationData _notificationData = new NotificationData();
                NotificationTemplate _notificationTemplate = GetNotificatoinTemplate(req.TopicID);
                EntityProfile _entprof = GetEntityProfile(req.EntityID);
                BuildTemplate _bt = new BuildTemplate();

                if (_notificationTemplate != null)
                {
                    _notificationData.From = _notificationTemplate.From;
                    _notificationData.Subject = _notificationTemplate.Subject;
                    _notificationData.To = _entprof.Email;//chnage based on comchannel

                    EmailTemplateBuild _etb = _bt.BuildEmailTemplate(_notificationTemplate.TemplateFile);
                    PlaceHoldReplacer _phr = new PlaceHoldReplacer();
                    _notificationData.RequestMessageData = _phr.Replace(_etb.MailBody.Trim(), AddPlaceHolder(_entprof), false).Trim(); //construct message with template
                    //Replace custom tag
                    _notificationData.RequestMessageData = _notificationData.RequestMessageData.Replace("$CustomTag$", req.RequestMessageData).Trim();
                }
                return _notificationData;
            }
            catch (Exception ex)
            {
                //need to log exception
                throw ex;
            }
        }

        List<PlaceHolder> AddPlaceHolder(EntityProfile entProf)
        {
            List<PlaceHolder> lstPH = new List<PlaceHolder>();
            PlaceHolder _pholder;
            foreach (PropertyInfo prop in entProf.GetType().GetProperties())
            {
                _pholder = new PlaceHolder(string.Format("${0}$", prop.Name), prop.GetValue(entProf).ToString());
                lstPH.Add(_pholder);
            }
            return lstPH;
        }

        EntityProfile GetEntityProfile(string EntityID)
        {
            EntityProfile _entprof = new EntityProfile();
            _entprof.FirstName = "Scott";
            _entprof.LastName = "George";
            _entprof.Email = "Scott.George@gainwelltechnologies.com";
            return _entprof;
        }

        NotificationTemplate GetNotificatoinTemplate(string TopicID)
        {
            NotificationTemplate _notificationTemplate = new NotificationTemplate();
            _notificationTemplate.ID = "123";
            _notificationTemplate.Subject = "PEA Registration";
            _notificationTemplate.From = "NoReply.PEA@Gainwelltechnologies.com";
            _notificationTemplate.ComChannel = "Email";
            _notificationTemplate.TemplateFile = "";
            //Need to validate template termination date
            return _notificationTemplate;
        }
       
    }
}
