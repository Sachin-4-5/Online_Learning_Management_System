using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace OLMS.BLL.Services
{
    public class EmailService
    {
        public void SendEmail(string to, string subject, string body)
        {
            try
            {
                SmtpClient smtp = new SmtpClient
                {
                    Host = ConfigurationManager.AppSettings["SMTPHost"] ?? "smtp.yourserver.com",
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"] ?? "587"),
                    EnableSsl = true,
                    UseDefaultCredentials = false,   // important!
                    Credentials = new NetworkCredential(
                        ConfigurationManager.AppSettings["SMTPUser"] ?? "your_email@domain.com",
                        ConfigurationManager.AppSettings["SMTPPass"] ?? "your_password"
                    )
                };

                MailMessage message = new MailMessage
                {
                    From = new MailAddress(ConfigurationManager.AppSettings["SMTPUser"] ?? "your_email@domain.com"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                message.To.Add(to);

                smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw new Exception("Email sending failed: " + ex.Message);
            }
        }
    }
}