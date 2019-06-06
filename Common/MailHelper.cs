using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace Common
{
    public class MailHelper
    {
        public  void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FormEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FormEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enanledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;


            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;


            var client = new SmtpClient
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword),
                Host = smtpHost,
                EnableSsl = enanledSsl,
                Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0
            };
            client.Send(message);

        }

        
    }
}
