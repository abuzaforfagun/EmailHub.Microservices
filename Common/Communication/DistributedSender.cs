using System;
using System.Text;
using System.Threading.Tasks;
using Common.Domain;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Management;
using Newtonsoft.Json;

namespace Communication
{
    public class DistributedSender : IDistributedSender
    {
        private readonly ServiceBusConfiguration _config;

        public DistributedSender(ServiceBusConfiguration config)
        {
            _config = config;
        }

        public async Task SendMessageAsync(IDistributedCommand payload, string queueName)
        {
            await InitializeQueue(queueName);

            var queueClient = new QueueClient(_config.PrimaryKey, queueName);

            var data = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(payload));
            Message message = new Message(Encoding.UTF8.GetBytes(data)) {MessageId = Guid.NewGuid().ToString()};
            await queueClient.SendAsync(message);
        }

        private async Task InitializeQueue(string queueName)
        {
            var managementClient = new ManagementClient(_config.PrimaryKey);
            if (!await managementClient.QueueExistsAsync(queueName))
            {
                try
                {
                    var queueDescription = new QueueDescription(queueName)
                    {
                        EnablePartitioning = false,
                        MaxSizeInMB = 1024,
                        RequiresDuplicateDetection = true
                    };

                    await managementClient.CreateQueueAsync(queueDescription);
                }
                catch (MessagingEntityAlreadyExistsException)
                {
                    //hiding this exception as this means another thread created the queue in the meantime, so all is fine
                }
            }
        }
    }
}
