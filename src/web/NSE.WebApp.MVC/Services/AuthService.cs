using Microsoft.Extensions.Options;
using NSE.WebApp.MVC.Extensions;
using NSE.WebApp.MVC.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient; 

        public AuthService(HttpClient httpClient, IOptions<AppSettings> options)
        {
            httpClient.BaseAddress = new System.Uri(options.Value.AutenticacaoUrl);
            _httpClient = httpClient;
             
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin user)
        {
            var loginCont = ObterValor(user);             

            var response = await _httpClient.PostAsync("/identidade/autenticar", loginCont);

            if (!TratarErrorsResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjectResponse<ResponseResult>(response)
                };
            } 
            return await DeserializarObjectResponse<UsuarioRespostaLogin>(response);

        }
        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro user)
        {
            var registroCont = ObterValor(user);
             

            var response = await _httpClient.PostAsync("/identidade/nova-conta", registroCont);

            if (!TratarErrorsResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = await DeserializarObjectResponse<ResponseResult>(response)
                };
            }
            return await DeserializarObjectResponse<UsuarioRespostaLogin>(response);
        }

    }
}
