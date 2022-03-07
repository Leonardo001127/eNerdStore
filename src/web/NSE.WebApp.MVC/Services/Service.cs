using NSE.WebApp.MVC.Extensions;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public abstract class Service
    { 
        protected StringContent ObterValor(object dado)
            => new StringContent(
              JsonSerializer.Serialize(dado), System.Text.Encoding.UTF8, "application/json" ); 


        protected async Task<T> DeserializarObjectResponse<T>(HttpResponseMessage response)
        {
            var opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync(), opt);
        }
        protected bool TratarErrorsResponse(HttpResponseMessage response)
        {
            switch ((int)response.StatusCode)
            {
                case 400:
                    return false;
                case 401: 
                case 403:  
                case 404:  
                case 500:
                    throw new CustomHttpRequestException(response.StatusCode);
            }
            response.EnsureSuccessStatusCode();
            return true;    
        }
    }
}
