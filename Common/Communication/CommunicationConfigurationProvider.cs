using System.Linq;

namespace Communication
{
    public class CommunicationConfigurationProvider : ICommunicationConfigurationProvider
    {
        private readonly ServiceBusConfiguration _config;

        public CommunicationConfigurationProvider(ServiceBusConfiguration config)
        {
            _config = config;
        }

        public string GetQueueName<T>(T item) =>
            _config.Queues?
                .SingleOrDefault(s => s.Contractor == item?.GetType().FullName)?.Name;
    }
}
