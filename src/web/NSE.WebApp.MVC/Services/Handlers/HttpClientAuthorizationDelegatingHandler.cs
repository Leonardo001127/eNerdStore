using NSE.WebApp.MVC.Extensions;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services.Handlers
{
    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IUser user;

        public HttpClientAuthorizationDelegatingHandler(IUser user)
        {
            this.user = user;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authHeader = user.ObterHttpContext().Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authHeader))
            {
                request.Headers.Add("Authorization", new List<string>() { authHeader });
            }
            var token = user.ObterUserToken();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
