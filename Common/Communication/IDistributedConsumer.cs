using System.Threading.Tasks;

namespace Communication
{
    public interface IDistributedConsumer
    {
        void RegisterQueueHandler<T>(string queueName);
        Task CloseQueueAsync();
    }
}
