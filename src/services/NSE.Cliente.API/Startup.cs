using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSE.Clientes.API.Configuration;
using NSE.WebAPI.Core.Identidade;

namespace NSE.Clientes.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostEnvironment host)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(host.ContentRootPath)
                .AddJsonFile("appSettings.json", true, true)
                .AddJsonFile($"appSettings.{host.EnvironmentName}json", true, true)
                .AddEnvironmentVariables();

            if (host.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();

            }

            Configuration = builder.Build();


        }




        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiConfiguration(Configuration);

            services.AddJwtConfiguration(Configuration);

            services.AddSwaggerConfiguration();

            services.AddMediatR(typeof(Startup)); 

            services.RegisterServices();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerConfiguration();

            app.UseApiConfiguration(env);
        }
    }
}
