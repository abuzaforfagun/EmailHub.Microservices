using System.Threading.Tasks;

namespace Communication
{
    public interface IDistributedSender
    {
        Task SendMessageAsync(object payload, string queueName);
    }
}
