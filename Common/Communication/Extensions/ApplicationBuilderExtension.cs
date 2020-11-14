using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Communication.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void RegisterServiceBus(this IApplicationBuilder appBuilder, ServiceBusConfiguration busConfig, IServiceProvider serviceProvider, Type queueModel)
        {
            foreach (var queue in busConfig.Queues)
            {
                using var scope = serviceProvider.CreateScope();
                var bus = scope.ServiceProvider.GetRequiredService<IDistributedConsumer>();
                var type = queueModel.Assembly.GetTypes()
                                .Single((t) => t.FullName == queue.Contractor);

                var method = bus.GetType().GetMethod("RegisterQueueHandler");
                var generic = method.MakeGenericMethod(type);
                generic.Invoke(bus, new object[] { queue.Name });
            }
        }
    }
}
