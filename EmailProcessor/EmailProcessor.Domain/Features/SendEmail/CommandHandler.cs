using System;
using System.Threading;
using System.Threading.Tasks;
using EmailProcessor.Contracts;
using MediatR;

namespace EmailProcessor.Domain.Features.SendEmail
{
    public class CommandHandler : AsyncRequestHandler<SendEmailCommand>
    {
        protected override Task Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
