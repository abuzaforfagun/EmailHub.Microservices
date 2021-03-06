﻿using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Communication
{
    public class DistributedConsumer : IDistributedConsumer
    {
        private QueueClient _queueClient;
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        public DistributedConsumer(ILogger<object> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public void RegisterQueueHandler<T>(string primaryKey, string queueName) where T:IRequest
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false,
            };
            _queueClient = new QueueClient(primaryKey, queueName);
            _queueClient.RegisterMessageHandler(ProcessMessagesAsync<T>, messageHandlerOptions);
        }

        public async Task Process<T>(T payload, IServiceProvider serviceProvider)
        {
            var mediator = serviceProvider.GetRequiredService<IMediator>();
            await mediator.Send(payload);
        }

        private async Task ProcessMessagesAsync<T>(Message message, CancellationToken token) where T:IRequest
        {
            using var serviceProviderScope = _serviceProvider.CreateScope();
            var payload = JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(message.Body)) as IRequest;
            await Process(payload, serviceProviderScope.ServiceProvider);
            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
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
