using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Communication.Extensions
{
    public static class ServiceExtension
    {
        public static void AddCommunincationService(this IServiceCollection services)
        {
            services.AddSingleton<IDistributedConsumer, DistributedConsumer>();
        }

        public static void RegisterServiceBus(this IServiceProvider serviceProvider, ServiceBusConfiguration busConfig, Type queueModel)
        {
            foreach (var queue in busConfig.Queues)
            {
                using var scope = serviceProvider.CreateScope();
                var bus = scope.ServiceProvider.GetRequiredService<IDistributedConsumer>();
                var type = queueModel.Assembly.GetTypes()
                                .Single((t) => t.FullName == queue.Contractor);

                var method = bus.GetType().GetMethod("RegisterQueueHandler");
                var generic = method.MakeGenericMethod(type);
                generic.Invoke(bus, new object[] { busConfig.PrimaryKey, queue.Name });
            }
        }
    }
}
