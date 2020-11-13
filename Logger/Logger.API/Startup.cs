using System;
using Communication;
using Communication.Extensions;
using GenericUnitOfWork;
using Logger.Contracts;
using Logger.Domain;
using Logger.Domain.Features.AddLog;
using Logger.Repository.Presistance;
using Logger.Repository.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Logger.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Logger.API", Version = "v1" });
            });

            var servicebusConfiguration = new ServiceBusConfiguration();
            Configuration.GetSection("Servicebus").Bind(servicebusConfiguration);
            services.AddSingleton(servicebusConfiguration);

            services.AddMediatR(typeof(DomainConfiguration));
            services.AddCommunincationService();
            services.AddDbContext<LogDbContext>(
                options => options
                    .UseNpgsql(Configuration.GetConnectionString("LogDb"))
            );
            services.AddUnitOfWork<LogDbContext>();
            services.AddTransient<AddLog.IRepository, EmailLogsRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, ServiceBusConfiguration busConfig)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Logger.API v1"));
            }

            app.UseHttpsRedirection();
            app.RegisterServiceBus(busConfig, serviceProvider, typeof(AddLogCommand));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
