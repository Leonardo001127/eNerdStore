using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        public readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomHttpRequestException ex)
            {
                 
            }
        }

        private static void HandleRequestExceptionAsync(HttpContext context, CustomHttpRequestException ex)
        {
            if(ex.statusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //string adicionada para redirecionar para a página desejada antes de receber o unauthorized
                context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
                return;
            }

            context.Response.StatusCode = (int)ex.statusCode;
        }
    }
}
