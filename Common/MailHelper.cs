using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace Common
{
    public class MailHelper
    {
        public void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();           
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            using (SmtpClient client = new SmtpClient(smtpHost, !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0))
            {
                client.UseDefaultCredentials = false;// luôn phải đứng trước .Credentials vì sẽ đặt lại client.Credentials thành null.
                client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);                
                client.EnableSsl = enabledSsl;
                client.Send(message);
            }
            //var client = new SmtpClient();
            //client.UseDefaultCredentials = false; // luôn phải đứng trước .Credentials vì sẽ đặt lại client.Credentials thành null.
            //client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            //client.Host = smtpHost;
            //client.EnableSsl = enabledSsl;
            
            //client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            //client.Send(message);
            
        }
    }
}
