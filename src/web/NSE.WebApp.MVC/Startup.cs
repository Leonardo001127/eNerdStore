using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using NSE.WebApp.MVC.Configuration;

namespace NSE.WebApp.MVC
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostingEnvironment host)
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
            services.AddIdentityConfiguration();
            services.AddMvcConfiguration(Configuration);

            services.RegisterServices(); 
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMvcConfiguration(env);

        }
    }
}
