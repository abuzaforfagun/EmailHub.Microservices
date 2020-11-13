using Communication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Logger.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Logger.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddMediatR(typeof(DomainConfiguration));
                    services.AddSingleton<IQueueProcessor, QueueProcessor>();
                    services.AddSingleton<IDistributedConsumer, DistributedConsumer>();

                    var servicebusConfiguration = new ServiceBusConfiguration();
                    hostContext.Configuration.GetSection("Servicebus").Bind(servicebusConfiguration);
                    services.AddSingleton(servicebusConfiguration);
                });
    }
}
