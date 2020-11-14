using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmailProcessor.Contracts;
using Newtonsoft.Json;
using Pepipost;
using Pepipost.Controllers;
using Pepipost.Models;

namespace EmailProcessor.Services
{
    public class PepipostEmailProcessor : IEmailProcessor
    {
        private const string Api = "https://api.pepipost.com";
        private readonly MailSendController _mailSendController;

        public PepipostEmailProcessor(PepipostClient client)
        {
            _mailSendController = client.MailSend;
        }
        public async Task SendEmailAsync(SendEmailCommand emailDetails)
        {
            var result = await _mailSendController.CreateGeneratethemailsendrequestAsync(CreateBody(emailDetails), Api);
            var res = JsonConvert.DeserializeObject<PepipostError>(result.ToString());
            if (res.Error.Count > 0)
            {
                throw new Exception();
            }
        }

        private Send CreateBody(SendEmailCommand emailDetails)
        {
            var bodyHtml = new Content
            {
                Type = TypeEnum.HTML,
                Value = emailDetails.Content
            };

            var reciverDetails = new EmailStruct {Name = emailDetails.ReciverName, Email = emailDetails.ReciverEmail};

            var recivers = new Personalizations
            {
                To = new List<EmailStruct>
                {
                    reciverDetails
                },
                Cc = new List<EmailStruct>(),
                Bcc = new List<EmailStruct>()
            };

            return new Send
            {
                From = new From
                {
                    Email = "faguncloud@pepisandbox.com",
                    Name = emailDetails.SenderName,
                },
                Subject = emailDetails.Subject,
                Content = new List<Content>
                {
                    bodyHtml
                },
                Personalizations = new List<Personalizations>
                {
                    recivers
                },
                Tags = new List<string> {"Send from Email Service"}
            };
        }
    }
}
