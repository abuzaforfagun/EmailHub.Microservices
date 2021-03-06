﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Communication;
using EmailProcessor.Contracts;
using Logger.Contracts;
using MediatR;

namespace Gateway.Domain.Features.SendEmail
{
    public partial class SendEmail
    {
        public class CommandHandler : AsyncRequestHandler<SendEmailCommand>
        {
            private readonly IDistributedSender _distributedSender;
            private readonly ICommunicationConfigurationProvider _communicationConfigurationProvider;
            private readonly IMapper _mapper;

            public CommandHandler(IDistributedSender distributedSender, ICommunicationConfigurationProvider communicationConfigurationProvider, IMapper mapper)
            {
                _distributedSender = distributedSender;
                _communicationConfigurationProvider = communicationConfigurationProvider;
                _mapper = mapper;
            }
            protected override async Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
            {
                await _distributedSender.SendMessageAsync(request, _communicationConfigurationProvider.GetQueueName(request));

                var log = _mapper.Map<AddLogCommand>(request);
                await _distributedSender.SendMessageAsync(log, _communicationConfigurationProvider.GetQueueName(log));
            }
        }
    }
}
