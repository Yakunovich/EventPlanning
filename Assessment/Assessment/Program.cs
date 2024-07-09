using System;

using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using System.Net.Mail;

namespace TestClient
{
    class Program
    {
        public static void Main(string[] args)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Sender Name", "event.planning.send@outlook.com"));
            email.To.Add(new MailboxAddress("Receiver Name", "jokerfly235@gmail.com"));

            email.Subject = "Testing out email sending";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = "<b>Hello all the way from the land of C#</b>"
            };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect("smtp.office365.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("event.planning.send@outlook.com", "P@ssw0rd1234");

                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}