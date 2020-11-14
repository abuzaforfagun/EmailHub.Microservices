using System.Threading;
using System.Threading.Tasks;
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
            var processor = _emailProcessorFactory.GetEmailProcessor(0);
            await processor.SendEmailAsync(request);
        }
    }
}
