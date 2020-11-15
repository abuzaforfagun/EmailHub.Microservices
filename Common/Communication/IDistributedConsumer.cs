using System.Threading.Tasks;
using MediatR;

namespace Communication
{
    public interface IDistributedConsumer
    {
        void RegisterQueueHandler<T>(string primaryKey, string queueName) where T : IRequest;
        Task CloseQueueAsync();
    }
}
