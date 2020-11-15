using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Common.Domain;
using Communication;
using Communication.Extensions;
using EmailProcessor.Domain;
using EmailProcessor.Services;
using MediatR;
using Pepipost;
using SendGrid;

namespace EmailProcessor.API
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmailProcessor.API", Version = "v1" });
            });

            var servicebusConfiguration = new ServiceBusConfiguration();
            Configuration.GetSection("Servicebus").Bind(servicebusConfiguration);
            services.AddSingleton(servicebusConfiguration);
           
            var emailServiceConfiguration = new EmailServiceConfiguration();
            Configuration.GetSection("EmailProcessor").Bind(emailServiceConfiguration);

            services.AddMediatR(typeof(EmailServiceConfiguration));
            services.AddCommunincationService();

            services.AddScoped(s =>
                new SendGridClient(emailServiceConfiguration.SenderGrid));
            services.AddScoped(p => new PepipostClient(emailServiceConfiguration.Pepipost));

            services.AddScoped<IRetryHelper, RetryHelper>();
            services.AddScoped<IEmailProcessor, PepipostEmailProcessor>();
            services.AddScoped<IEmailProcessor, SenderGridEmailProcessor>();
            services.AddScoped<IEmailProcessorFactory, EmailProcessorFactory>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider, ServiceBusConfiguration busConfig)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmailProcessor.API v1"));
            }

            app.UseHttpsRedirection();
            //app.RegisterServiceBus(busConfig, serviceProvider, typeof(SendEmailCommand));
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
