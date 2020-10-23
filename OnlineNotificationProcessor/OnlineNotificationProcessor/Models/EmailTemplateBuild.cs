using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace OnlineNotificationProcessor.Models
{
    [XmlRoot("MailBulidTemplate")]
    public class EmailTemplateBuild
    {        /// <summary>
        /// Entity class for holding the data that needs to be sent in mail
        /// </summary>    
       
            [XmlElement("MailSubject")]
            public string MailSubject { get; set; }

            [XmlElement("From")]
            public string From { get; set; }

            [XmlArray("To")]
            [XmlArrayItem("add")]
            public List<string> To { get; set; }

            [XmlArray("CC")]
            [XmlArrayItem("add")]
            public List<string> CC { get; set; }

            [XmlArray("Bcc")]
            [XmlArrayItem("add")]
            public List<string> Bcc { get; set; }

            [XmlElement("MailBody")]
            public string MailBody { get; set; }

            [XmlElement("IsBodyHtml")]
            public bool IsBodyHtml { get; set; }

            public EmailTemplateBuild()
            {
                this.To = new List<string>();
                this.CC = new List<string>();
                this.Bcc = new List<string>();
            }
        
    }
}
