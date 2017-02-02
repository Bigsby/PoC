using System;
using System.Net.Mail;
using System.Web.Http;

namespace GoDaddyEmailing.Controllers
{
    public class EmailController : ApiController
    {
        [HttpPost]
        public string Send([FromBody]Message message)
        {
            var m = new MailMessage();

            m.Subject = message.Subject;
            m.Body = message.Content;

            m.To.Add(new MailAddress(message.To));
            var client = new SmtpClient();
            try
            {
                client.Send(m);
                return "Sent!";
            }
            catch (Exception ex)
            {
                return GetMessage(ex);
            }
        }

        private string GetMessage(Exception ex)
        {
            var current = ex;

            while (null != current.InnerException)
                current = current.InnerException;

            return current.Message;
        }
    }

    public class Message {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
