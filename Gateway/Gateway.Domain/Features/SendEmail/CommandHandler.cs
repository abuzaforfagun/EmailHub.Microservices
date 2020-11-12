using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Gateway.Domain.Features.SendEmail
{
    public partial class SendEmail
    {
        public class CommandHandler : AsyncRequestHandler<Command>
        {
            protected override Task Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
