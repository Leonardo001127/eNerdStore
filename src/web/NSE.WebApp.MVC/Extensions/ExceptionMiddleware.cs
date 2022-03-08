using Microsoft.AspNetCore.Http;
using Polly.CircuitBreaker;
using Refit;
using System.Net;
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
                HandleRequestExceptionAsync(context, ex.statusCode);
            }
            catch (ValidationApiException ex)
            {
                HandleRequestExceptionAsync(context, ex.StatusCode);
            }
            catch (ApiException ex)
            {
                HandleRequestExceptionAsync(context, ex.StatusCode);
            }
            catch (BrokenCircuitException)
            {
                HandleCircuitBreakerExceptionAsync(context);
            }
        }

        private static void HandleRequestExceptionAsync(HttpContext context, HttpStatusCode ex)
        {
            if(ex == HttpStatusCode.Unauthorized)
            {
                //string adicionada para redirecionar para a página desejada antes de receber o unauthorized
                context.Response.Redirect($"/Login?ReturnUrl={context.Request.Path}");
                return;
            }

            context.Response.StatusCode = (int)ex;
        }

        private static void HandleCircuitBreakerExceptionAsync(HttpContext context)
        {
            context.Response.Redirect("/sistema-indisponivel");
        }
    }
}
