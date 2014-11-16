using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace seoWebApplication.Framework
{
    public static class emailSend
    {
        public static bool send(string To, string Subject, string Body) {
            try
            {
                var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
                var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
                var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
                var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

                MailMessage o = new MailMessage(fromEmailAddress, To, Subject, Body);
                NetworkCredential netCred = new NetworkCredential(fromEmailAddress, fromEmailPassword);
                SmtpClient smtpobj = new SmtpClient(smtpHost, Convert.ToInt32(smtpPort));
                smtpobj.EnableSsl = true;
                smtpobj.Timeout = 5;
                smtpobj.Credentials = netCred;
                smtpobj.Send(o);

                return true;
            }
            catch
            {
                return false;
            }
        
        }

        public static bool sendGmail(string To, string Subject, string Body){
            try
            {
                var fromGmailEmailAddress = ConfigurationManager.AppSettings["FromGmailEmailAddress"].ToString();
                var fromGmailEmailDisplayName = ConfigurationManager.AppSettings["FromGmailEmailDisplayName"].ToString();
                var fromGmailEmailPassword = ConfigurationManager.AppSettings["FromGmailEmailPassword"].ToString();
                var GmailSmtpHost = ConfigurationManager.AppSettings["GmailSMTPHost"].ToString();
                var GmailSmtpPort = ConfigurationManager.AppSettings["GmailSMTPPort"].ToString();

                MailMessage o = new MailMessage(fromGmailEmailAddress, To, Subject, Body);
                NetworkCredential netCred= new NetworkCredential(fromGmailEmailAddress,fromGmailEmailPassword);
                SmtpClient smtpobj= new SmtpClient(GmailSmtpHost, Convert.ToInt32(GmailSmtpPort)); 
                smtpobj.EnableSsl = true;
                smtpobj.Credentials = netCred;
                smtpobj.Send(o);
                  
                return true;
    
            }
            catch 
            {
                return false;
            }
        }
    }
}
