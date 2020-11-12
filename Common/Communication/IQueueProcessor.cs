using System.Threading.Tasks;

namespace Communication
{
    public interface IQueueProcessor
    {
        Task Process<T>(T payload);
    }
}
