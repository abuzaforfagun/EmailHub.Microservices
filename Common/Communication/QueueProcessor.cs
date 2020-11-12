using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Communication
{
    public class QueueProcessor : IQueueProcessor
    {
        private readonly IMediator _mediator;

        public QueueProcessor(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Process<T>(T payload)
        {
            await _mediator.Send(payload, new CancellationToken());
        }
    }
}
