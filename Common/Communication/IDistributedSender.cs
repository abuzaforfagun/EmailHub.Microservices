using System.Threading.Tasks;
using Common.Domain;

namespace Communication
{
    public interface IDistributedSender
    {
        Task SendMessageAsync(IDistributedCommand payload, string queueName);
    }
}
