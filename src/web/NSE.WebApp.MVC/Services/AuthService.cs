using NSE.WebApp.MVC.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class AuthService : Service, IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UsuarioRespostaLogin> Login(UsuarioLogin user)
        {
            var loginCont = new StringContent(
                JsonSerializer.Serialize(user),
                System.Text.Encoding.UTF8,
                "application/json"
                );

            var opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var response = await _httpClient.PostAsync("https://localhost:44396/api/identidade/autenticar", loginCont);


            if (!TratarErrorsResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), opt)
                };
            }


            return JsonSerializer.Deserialize<UsuarioRespostaLogin>(await response.Content.ReadAsStringAsync(), opt);
        }
        public async Task<UsuarioRespostaLogin> Registro(UsuarioRegistro user)
        {
            var registroCont = new StringContent(
                JsonSerializer.Serialize(user),
                System.Text.Encoding.UTF8, 
                "applicaton/json"
                );

            var opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var response = await _httpClient.PostAsync("https://localhost:44396/api/identidade/nova-conta", registroCont);

            if (!TratarErrorsResponse(response))
            {
                return new UsuarioRespostaLogin
                {
                    ResponseResult = JsonSerializer.Deserialize<ResponseResult>(await response.Content.ReadAsStringAsync(), opt)
                };
            }
            return JsonSerializer.Deserialize<UsuarioRespostaLogin>(await response.Content.ReadAsStringAsync(), opt);
        }

    }
}
