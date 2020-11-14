using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using Communication;
using Communication.Extensions;
using EmailProcessor.Contracts;
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

            services.AddMediatR(typeof(DomainConfiguration));
            services.AddCommunincationService();

            services.AddScoped(s =>
                new SendGridClient("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"));
            services.AddScoped(p => new PepipostClient("84bc7a98af555bed1db40e3b66e4f5b2"));

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
            app.RegisterServiceBus(busConfig, serviceProvider, typeof(SendEmailCommand));
            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
