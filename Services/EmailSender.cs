using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace shortenyour.link.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            using (var smtp = new SmtpClient("smtp-relay.sendinblue.com", 587))
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("berkay.sarac420@gmail.com", "VGD1wPLd4KWnH8bf");
                smtp.EnableSsl = true;

                var message = new MailMessage
                {
                    From = new MailAddress("berkay.sarac420@gmail.com"),
                    Subject = subject,
                    Body = htmlMessage
                };
                message.To.Add(email);

                smtp.Send(message);
            }
            return Task.CompletedTask;
        }
    }
}