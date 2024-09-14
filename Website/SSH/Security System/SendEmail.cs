using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
namespace MailSpace
{
    public class SendEmail
    {
        public string ClientEmail { get; set; }
        public string ClientSubject { get; set; }
        public string ClientQuery { get; set; }

        public SendEmail(string clientEmail, string clientSubject, string userName, string HomeOwner)
        {

            this.ClientEmail = clientEmail;
            this.ClientSubject = clientSubject;
            this.ClientQuery = $"Dear { userName } \nYou have been added under { HomeOwner }'s house as one of the occupants. \n\n" +
                               $"Below are your credentials to log in \n" +
                               $"Username: { clientEmail } \n" +
                               $"Password: { clientEmail } \n\n" +
                               $"SSH";
        }
        public void sendToClient()
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                };
                client.Credentials = new NetworkCredential("keletsovincent92@gmail.com", "#Keletsotv1997");
                MailMessage message = new MailMessage();
                message.To.Add(ClientEmail);
                message.From = new MailAddress("keletsovincent92@gmail.com");
                message.Subject = ClientSubject;
                message.Body += $"{ClientQuery}";
                client.Send(message);
                client.Dispose();
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
    }
}