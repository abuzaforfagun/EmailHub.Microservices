using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Communication;
using Logger.Contracts;

namespace Logger.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ServiceBusConfiguration _busConfig;
        private readonly IDistributedConsumer _consumer;

        public Worker(ILogger<Worker> logger, ServiceBusConfiguration busConfig, IDistributedConsumer consumer)
        {
            _logger = logger;
            _busConfig = busConfig;
            _consumer = consumer;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //while (!stoppingToken.IsCancellationRequested)
            //{
                foreach (var queue in _busConfig.Queues)
                {
                    var type = typeof(AddLogCommand).Assembly.GetTypes().Single((t) => t.FullName == queue.Contractor);


                    var method = _consumer.GetType().GetMethod("RegisterQueueHandler");
                    if (method is not null)
                    {
                        var generic = method.MakeGenericMethod(type);
                        generic.Invoke(_consumer, new object[] { queue.Name });
                    }
                }
            //}

            return Task.CompletedTask;
        }
    }
}
