using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Communication
{
    public class DistributedConsumer : IDistributedConsumer
    {
        private readonly IQueueProcessor _queueProcessor;
        private QueueClient _queueClient;
        private readonly ILogger _logger;
        private readonly ServiceBusConfiguration _config;

        public DistributedConsumer(IQueueProcessor queueProcessor,
            ILogger<object> logger, ServiceBusConfiguration config)
        {
            _queueProcessor = queueProcessor;
            _logger = logger;
            _config = config;
        }

        public void RegisterQueueHandler<T>(string queueName)
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = true,
            };
            _queueClient = new QueueClient(_config.PrimaryKey, queueName);
            _queueClient.RegisterMessageHandler(ProcessMessagesAsync<T>, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync<T>(Message message, CancellationToken token)
        {
            var payload = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body));
            await _queueProcessor.Process(payload);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogError(exceptionReceivedEventArgs.Exception, "Message handler encountered an exception");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            _logger.LogDebug($"- Endpoint: {context.Endpoint}");
            _logger.LogDebug($"- Entity Path: {context.EntityPath}");
            _logger.LogDebug($"- Executing Action: {context.Action}");

            return Task.CompletedTask;
        }

        public async Task CloseQueueAsync()
        {
            await _queueClient.CloseAsync();
        }
    }
}
