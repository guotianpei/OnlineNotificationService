using ONP.BackendProcessor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ONP.BackendProcessor.Tasks
{
    public class BuildTemplate
    {
        public EmailTemplateBuild BuildEmailTemplate(string template)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(template);

            // Deserialize and return the Mail Template
            XmlSerializer Emailserializer = new XmlSerializer(typeof(EmailTemplateBuild));
            return (EmailTemplateBuild)Emailserializer.Deserialize(new StringReader(xmldoc.InnerXml));
        }

    }
}
