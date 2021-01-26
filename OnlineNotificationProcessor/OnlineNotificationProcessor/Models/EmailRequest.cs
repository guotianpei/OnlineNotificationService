using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ONP.BackendProcessor.Models
{
    public class EmailRequest
    {
        /// <summary>
        /// Mail's message
        /// </summary>
        
        public string Message {get;set;}

        /// <summary>
        /// mail's subject
        /// </summary>
        
        public string subject { get; set; }

        /// <summary>
        /// Any attachments that are included with this
        /// message.
        /// </summary>

        public List<string> Attachments { get; set; }

        /// <summary>
        /// Any attachment (usually images) that need to be embedded in the message
        /// </summary>
        
        public List<string> EmbeddedPictures { get; set; }

        /// <summary>
        /// The priority of this message
        /// </summary>
        
        public MailPriority Priority { get; set; }
        
        
        public string Server { get; set; }

        /// <summary>
        /// Source email address
        /// </summary>

        public string From { get; set; }

        /// <summary>
        /// Destination email address
        /// </summary>

        public string To { get; set; }

        /// <summary>
        /// User Name for the server
        /// </summary>

        public string UserName { get; set; }

        /// <summary>
        /// Password for the server
        /// </summary>

        public string Password { get; set; }

        /// <summary>
        /// SMTP server's port number
        /// </summary>

        public int Port { get; set; }

        /// <summary>
        /// Decides whether SSL is used or not
        /// </summary>
        
        public bool UseSSL { get; set; }

        /// <summary>
        /// Decides whether the mail body is HTML or not
        /// </summary>

        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// Carbon copy send (seperate email addresses with a comma)
        /// </summary>

        public string CC { get; set; }

        /// <summary>
        /// Blind carbon copy send (seperate email addresses with a comma)
        /// </summary>

        public string Bcc { get; set; }

        /// <summary>
        /// To enable email read receipt option
        /// </summary>

        public bool EnableReadReceipt { get; set; }

        /// <summary>
        /// Indicate wethere the email is sending via exchange server service
        /// </summary>

        public bool SendEmailViaExchangeServer { get; set; }

        /// <summary>
        /// Exchange server URL
        /// </summary>

        public string ExchangeServerURL { get; set; }

        /// <summary>
        /// Exchange server username
        /// </summary>

        public string ExchangeServerUserName { get; set; }

        /// <summary>
        /// Exchange server password
        /// </summary>

        public string ExchangeServerPwd { get; set; }                           

        /// <summary>
        /// Carry reference text about email
        /// </summary>

        public string References { get; set; }

    }
}
