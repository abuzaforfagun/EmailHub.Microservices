using System.Threading;
using System.Threading.Tasks;
using Communication;
using EmailProcessor.Contracts;
using MediatR;

namespace Gateway.Domain.Features.SendEmail
{
    public partial class SendEmail
    {
        public class CommandHandler : AsyncRequestHandler<SendEmailCommand>
        {
            private readonly IDistributedSender _distributedSender;
            private readonly ICommunicationConfigurationProvider _communicationConfigurationProvider;

            public CommandHandler(IDistributedSender distributedSender, ICommunicationConfigurationProvider communicationConfigurationProvider)
            {
                _distributedSender = distributedSender;
                _communicationConfigurationProvider = communicationConfigurationProvider;
            }
            protected override async Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
            {
                await _distributedSender.SendMessageAsync(request, _communicationConfigurationProvider.GetQueueName(request));
            }
        }
    }
}
