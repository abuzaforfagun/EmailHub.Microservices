using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Communication;
using Communication.Extensions;
using EmailProcessor.Contracts;

namespace EmailProcessor.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ServiceBusConfiguration _busConfig;

        public Worker(IServiceProvider serviceProvider, ServiceBusConfiguration busConfig)
        {
            _serviceProvider = serviceProvider;
            _busConfig = busConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Run(() => _serviceProvider.RegisterServiceBus(_busConfig, typeof(SendEmailCommand)), stoppingToken);
        }
    }
}
