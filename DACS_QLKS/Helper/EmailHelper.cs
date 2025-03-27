using System.Net;
using System.Net.Mail;

namespace DACS_QLKS.Helper
{
    public static class EmailHelper
    {
        public static void SendEmail(string toEmail, string subject, string body)
        {
            var fromAddress = new MailAddress("your_email@gmail.com", "Your Name");
            var toAddress = new MailAddress(toEmail, "Receiver Name");

            const string fromPassword = "your_password";
            const string smtpServer = "smtp.gmail.com";
            const int smtpPort = 587;

            var smtp = new SmtpClient
            {
                Host = smtpServer,
                Port = smtpPort,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}

