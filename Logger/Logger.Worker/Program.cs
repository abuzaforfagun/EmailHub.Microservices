using Communication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Logger.Domain;
using Logger.Repository.Presistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                    services.AddDbContext<LogDbContext>(
                        options => options
                            .UseNpgsql(hostContext.Configuration.GetConnectionString("LogDb"))
                    );
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
