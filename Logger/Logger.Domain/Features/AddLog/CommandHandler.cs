using System.Threading;
using System.Threading.Tasks;
using Logger.Contracts;
using MediatR;

namespace Logger.Domain.Features.AddLog
{
    public partial class AddLog
    {
        public class CommandHandler : AsyncRequestHandler<AddLogCommand>
        {
            protected override Task Handle(AddLogCommand request, CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }
    }
}
