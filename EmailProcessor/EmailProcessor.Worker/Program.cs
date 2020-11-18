using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Common.Domain;
using Communication;
using Communication.Extensions;
using EmailProcessor.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Pepipost;
using SendGrid;

namespace EmailProcessor.Worker
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
                    var servicebusConfiguration = new ServiceBusConfiguration();
                    hostContext.Configuration.GetSection("Servicebus").Bind(servicebusConfiguration);
                    services.AddSingleton(servicebusConfiguration);
           
                    var emailServiceConfiguration = new EmailServiceConfiguration();
                    hostContext.Configuration.GetSection("EmailProcessor").Bind(emailServiceConfiguration);
                    services.AddScoped(provider  => emailServiceConfiguration);

                    services.AddMediatR(typeof(EmailServiceConfiguration));
                    services.AddCommunincationService();
                    
                    services.AddScoped(s =>
                        new SendGridClient(emailServiceConfiguration.SenderGrid.AppKey));
                    services.AddScoped(p => new PepipostClient(emailServiceConfiguration.Pepipost.AppKey));

                    services.AddScoped<IRetryHelper, RetryHelper>();
                    services.AddScoped<IEmailProcessor, PepipostEmailProcessor>();
                    services.AddScoped<IEmailProcessor, SenderGridEmailProcessor>();
                    services.AddScoped<IEmailProcessorFactory, EmailProcessorFactory>();

                    services.AddHostedService<Worker>();

                });
    }
}
