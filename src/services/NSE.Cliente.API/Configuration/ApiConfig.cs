using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSE.Clientes.API.Data;
using NSE.WebAPI.Core.Identidade;

namespace NSE.Clientes.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ClienteContext>(x =>
            x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddCors(opt =>
            {
                opt.AddPolicy("Total",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            //services.AddControllers();

            return services;
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}