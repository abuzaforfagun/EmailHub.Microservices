using AutoMapper;
using Communication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using Gateway.Domain;
using MediatR;

namespace Gateway.API
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
            services.AddAutoMapper();
            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<DomainConfiguration>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway.API", Version = "v1" });
            });

            services.AddMediatR(typeof(DomainConfiguration));

            var servicebusConfiguration = new ServiceBusConfiguration();
            Configuration.GetSection("Servicebus").Bind(servicebusConfiguration);
            services.AddSingleton(servicebusConfiguration);
            services.AddScoped<IDistributedSender, DistributedSender>();
            services.AddScoped<ICommunicationConfigurationProvider, CommunicationConfigurationProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ServiceBusConfiguration busConfig)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway.API v1"));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
