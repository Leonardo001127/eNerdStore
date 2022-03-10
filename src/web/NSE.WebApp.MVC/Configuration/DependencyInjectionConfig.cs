using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Services;
using NSE.WebApp.MVC.Services.Handlers;

namespace NSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration config)
        {
            //tem que ser transiente porque deve ser instanciado a cada novo request feito
            services.AddTransient<HttpClientAuthorizationDelegatingHandler>();



            services.AddHttpClient<IAuthService, AuthService>();

            services.AddHttpClient("Refit", opt =>
            {
                opt.BaseAddress = new System.Uri(config.GetSection("CatalogoUrl").Value);
            })
                .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                .AddTypedClient(Refit.RestService.For<ICatalogoServiceRefit>);


            //services.AddHttpClient<ICatalogoService, CatalogoService>()
            //    //uso transiente para o request ser manipulado
            //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUser, NetUser>();
        }
    }
}
