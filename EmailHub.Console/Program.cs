using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Communication;
using EmailProcessor.Contracts;
using Gateway.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EmailHub.Console
{
    class Program
    {
        public static IConfigurationRoot configuration;
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IHost host = CreateHostBuilder(args).Build();
            while (true)
            {
                System.Console.Write("Sender Email: ");
                var senderEmail = System.Console.ReadLine();
                System.Console.Write("Sender Name: ");
                var senderName = System.Console.ReadLine();
                System.Console.Write("Reciver Email: ");
                var reciverEmail = System.Console.ReadLine();
                System.Console.Write("Reciver Name: ");
                var reciverName = System.Console.ReadLine();
                System.Console.Write("Subject: ");
                var subject = System.Console.ReadLine();
                System.Console.Write("Body: ");
                var content = System.Console.ReadLine();

                using IServiceScope serviceScope = host.Services.CreateScope();
                IServiceProvider provider = serviceScope.ServiceProvider;
                var mediator = provider.GetRequiredService<IMediator>();
                await mediator.Send(new SendEmailCommand
                {
                    Content = content,
                    ReciverEmail = reciverEmail,
                    ReciverName = reciverName,
                    SenderName = senderName,
                    SenderEmail = senderEmail,
                    Subject = subject
                });

                System.Console.WriteLine("We are processing your request...");
            }
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    var servicebusConfiguration = new ServiceBusConfiguration();
                    configuration.GetSection("Servicebus").Bind(servicebusConfiguration);
                    
                    services.AddAutoMapper();
                    services.AddSingleton(servicebusConfiguration);
                    services.AddScoped<IDistributedSender, DistributedSender>();
                    services.AddScoped<ICommunicationConfigurationProvider, CommunicationConfigurationProvider>();
                    services.AddMediatR(typeof(DomainConfiguration));
                });

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogging();

            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            serviceCollection.AddSingleton(configuration);
        }

    }
}
