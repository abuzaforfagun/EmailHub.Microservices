using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Communication;
using Communication.Extensions;
using Logger.Contracts;
using Logger.Repository.Presistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Logger.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ServiceBusConfiguration _busConfig;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(IServiceProvider serviceProvider, ServiceBusConfiguration busConfig, IServiceScopeFactory serviceScopeFactory)
        {
            _serviceProvider = serviceProvider;
            _busConfig = busConfig;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<LogDbContext>();
            await dbContext.Database.MigrateAsync(cancellationToken: stoppingToken);

            await Task.Run(() => _serviceProvider.RegisterServiceBus(_busConfig, typeof(AddLogCommand)), stoppingToken);
        }
    }
}
