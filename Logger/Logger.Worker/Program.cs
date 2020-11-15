using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Communication;
using Communication.Extensions;
using GenericUnitOfWork;
using Logger.Domain;
using Logger.Domain.Features.AddLog;
using Logger.Repository.Presistance;
using Logger.Repository.Repositories;
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
                    var servicebusConfiguration = new ServiceBusConfiguration();
                    hostContext.Configuration.GetSection("Servicebus").Bind(servicebusConfiguration);
                    services.AddSingleton(servicebusConfiguration);

                    services.AddMediatR(typeof(DomainConfiguration));
                    services.AddAutoMapper();
                    services.AddCommunincationService();
                    services.AddDbContext<LogDbContext>(
                        options => options
                            .UseNpgsql(hostContext.Configuration.GetConnectionString("LogDb"))
                    );
                    services.AddUnitOfWork<LogDbContext>();
                    services.AddTransient<AddLog.IRepository, EmailLogsRepository>();
                    services.AddHostedService<Worker>();
                });
    }
}
