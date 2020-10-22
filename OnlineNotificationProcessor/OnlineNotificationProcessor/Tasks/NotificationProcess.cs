using OnlineNotificationProcessor.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace OnlineNotificationProcessor.Tasks
{
    public class NotificationProcess
    {
        ISendNotification _sendnotificatoin;
        public NotificationProcess(ISendNotification objSN)
        {
            _sendnotificatoin = objSN;
        }
        public void SendNotification(List<NotificationRequest> lstnotreq)
        {
            NotificationResponse notResponse;

            foreach (NotificationRequest notreq in lstnotreq)
            {
                notResponse = _sendnotificatoin.SendNotification(BuildNotificationData(notreq));

                if (notResponse != null )
                {

                    //Domain event should be triggered here.
                    if (notResponse.NotificationStage == NotificationStage.Success)
                    {
                        //Update request status completed
                    }
                    else if (notResponse.NotificationStage == NotificationStage.Failed)
                    {
                        //Raise Domain event to update
                        //Update request status failed and lof failed reason
                    }
                }
            }            
        }

        NotificationData BuildNotificationData(NotificationRequest notreq)
        {
            try
            {
                NotificationData _notData = new NotificationData();
                NotificationTemplate _nottemp = GetNotificatoinTemplate(notreq.TopicID);
                EntityProfile _entprof = GetEntityProfile(notreq.EntityID);
                BuildTemplate _bt = new BuildTemplate();

                if (_nottemp != null)
                {
                    _notData.From = _nottemp.From;
                    _notData.Subject = _nottemp.Subject;
                    _notData.To = _entprof.Email;//chnage based on comchannel

                    EmailTemplateBuild _etb = _bt.BuildEmailTemplate(_nottemp.TemplateFile);
                    PlaceHoldReplacer _phr = new PlaceHoldReplacer();
                    _notData.RequestMessageData = _phr.Replace(_etb.MailBody.Trim(), AddPlaceHolder(_entprof), false).Trim(); //construct message with template
                    //Replace custom tag
                    _notData.RequestMessageData = _notData.RequestMessageData.Replace("$CustomTag$", notreq.RequestMessageData).Trim();
                }
                return _notData;
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
            NotificationTemplate _nottemp = new NotificationTemplate();
            _nottemp.ID = "123";
            _nottemp.Subject = "PEA Registration";
            _nottemp.From = "NoReply.PEA@Gainwelltechnologies.com";
            _nottemp.ComChannel = "Email";
            _nottemp.TemplateFile = "";
            //Need to validate template termination date
            return _nottemp;
        }
       
    }
}
