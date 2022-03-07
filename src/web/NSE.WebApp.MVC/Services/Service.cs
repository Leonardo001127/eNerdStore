using NSE.WebApp.MVC.Extensions;
using System.Net.Http;

namespace NSE.WebApp.MVC.Services
{
    public abstract class Service
    { 
        public bool TratarErrorsResponse(HttpResponseMessage response)
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
