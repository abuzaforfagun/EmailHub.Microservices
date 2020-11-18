using System;
using System.Net;
using System.Threading.Tasks;
using EmailProcessor.Contracts;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EmailProcessor.Services
{
    public class SenderGridEmailProcessor : IEmailProcessor
    {
        private readonly SendGridClient _senderClient;

        public SenderGridEmailProcessor(SendGridClient senderClient)
        {
            _senderClient = senderClient;
        }

        public async Task SendEmailAsync(SendEmailCommand emailDetails, EmailServiceConfiguration configuration)
        {
            var from = new EmailAddress(
                configuration.SenderGrid.IsSandbox ? configuration.SenderGrid.DefaultEmail : emailDetails.SenderEmail, 
                emailDetails.SenderName);
            var (subject, htmlContent) = (emailDetails.Subject, emailDetails.Content);
            var to = new EmailAddress(emailDetails.ReciverEmail, emailDetails.ReciverName);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, default, htmlContent);
            var response = await _senderClient.SendEmailAsync(msg).ConfigureAwait(false);

            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                throw new Exception();
            }
        }
    }
}
