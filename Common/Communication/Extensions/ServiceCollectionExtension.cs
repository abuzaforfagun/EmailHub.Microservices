using Microsoft.Extensions.DependencyInjection;

namespace Communication.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddCommunincationService(this IServiceCollection services)
        {
            services.AddSingleton<IDistributedConsumer, DistributedConsumer>();
        }
    }
}
