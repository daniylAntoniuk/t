using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GeekStore.Services
{
    //public class EmailSender : IEmailSender
    //{
      
    //        private readonly IConfiguration _configuration;
    //        public EmailSender(IConfiguration configuration)
    //        {
    //            _configuration = configuration;
    //        }


    //        public Task SendEmailAsync(string email, string subject, string htmlMessage)
    //        {
    //            return Execute(_configuration.GetValue<string>("EmailGridKey"), subject, htmlMessage, email);
    //        }


    //        public Task Execute(string apiKey, string subject, string message, string email)
    //        {
    //            var client = new SendGridClient(apiKey);
    //            var msg = new SendGridMessage()
    //            {
    //                From = new EmailAddress("home.realtor.suport@gmail.com", "Geek Store"),
    //                Subject = subject,
    //                PlainTextContent = message,
    //                HtmlContent = message
    //            };
    //            msg.AddTo(new EmailAddress(email));
    //            msg.SetClickTracking(false, false);
    //            return client.SendEmailAsync(msg);
    //        }
        
    //}
    public class EmailSender
    {
        public void SendEmail(string Email, string Message)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("pluzharasemen@gmail.com");
            mail.To.Add(Email);
            mail.Subject = "Forgot password";
            mail.IsBodyHtml = true;
            mail.Body = Message;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("pluzharasemen@gmail.com", "Qwerty1-"),
                EnableSsl = true
            };
            client.Send(mail);
            // MailMessage mail = new MailMessage();
            // SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            // mail.From = new MailAddress("home.realtor.suport@gmail.com");
            // mail.To.Add(Email);
            // mail.Subject = "Forgot password";
            // mail.IsBodyHtml = true;
            // mail.Body = "" +
            // "<head>" +
            // "Your account is locked press button to unlock :" +
            // "</head>" +
            //// $" <a href=\" https://localhost:44325/api/user/unlock/{code}/ \">" +
            // "<button>" +
            // "Unlock" +
            // "</button>" +
            // " </a>  ";
            // SmtpServer.Port = 587;
            // SmtpServer.Credentials = new System.Net.NetworkCredential("home.realtor.suport@gmail.com", "00752682");
            // SmtpServer.EnableSsl = true;
            // SmtpServer.Send(mail);
        }
    }
}
