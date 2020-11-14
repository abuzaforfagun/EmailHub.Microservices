using System.Threading;
using System.Threading.Tasks;
using Common.Domain;
using EmailProcessor.Contracts;
using EmailProcessor.Services;
using MediatR;

namespace EmailProcessor.Domain.Features.SendEmail
{
    public class CommandHandler : AsyncRequestHandler<SendEmailCommand>
    {
        private readonly IEmailProcessorFactory _emailProcessorFactory;

        public CommandHandler(IEmailProcessorFactory emailProcessorFactory)
        {
            _emailProcessorFactory = emailProcessorFactory;
        }
        protected override async Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await RetryHelper.InvokeAsync(async (retryAttempt) =>
            {
                var processor = _emailProcessorFactory.GetEmailProcessor(retryAttempt);
                await processor.SendEmailAsync(request);
            });
        }
    }
}
