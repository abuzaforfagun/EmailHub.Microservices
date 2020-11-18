using System.Threading;
using System.Threading.Tasks;
using Common.Domain;
using EmailProcessor.Contracts;
using EmailProcessor.Services;
using MediatR;

namespace EmailProcessor.Domain.Features.SendEmail
{
    public partial class SendEmail
    {
        public class CommandHandler : AsyncRequestHandler<SendEmailCommand>
        {
            private readonly IEmailProcessorFactory _emailProcessorFactory;
            private readonly IRetryHelper _retryHelper;
            private readonly EmailServiceConfiguration _serviceConfiguration;

            public CommandHandler(IEmailProcessorFactory emailProcessorFactory, 
                                IRetryHelper retryHelper, 
                                EmailServiceConfiguration serviceConfiguration)
            {
                _emailProcessorFactory = emailProcessorFactory;
                _retryHelper = retryHelper;
                _serviceConfiguration = serviceConfiguration;
            }
            protected override async Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
            {
                await _retryHelper.InvokeAsync(async (retryAttempt) =>
                {
                    var processor = _emailProcessorFactory.GetEmailProcessor(retryAttempt);
                    await processor.SendEmailAsync(request, _serviceConfiguration);
                });
            }
        }
    }
}
