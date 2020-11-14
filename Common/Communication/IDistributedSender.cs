using System.Threading.Tasks;

namespace Communication
{
    public interface IDistributedSender
    {
        Task SendMessageAsync(IDistributedCommand payload, string queueName);
    }
}
