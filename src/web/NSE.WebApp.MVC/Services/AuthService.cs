using NSE.WebApp.MVC.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Login(UsuarioLogin user)
        {
            var loginCont = new StringContent(
                JsonSerializer.Serialize(user),
                System.Text.Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PostAsync("https://localhost:44396/api/identidade/autenticar", loginCont);

            var teste = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Registro(UsuarioRegistro user)
        {
            var registroCont = new StringContent(
                JsonSerializer.Serialize(user),
                System.Text.Encoding.UTF8,
                "applicaton/json"
                );

            var response = await _httpClient.PostAsync("https://localhost:44396/api/identidade/nova-conta", registroCont);
            return JsonSerializer.Deserialize<string>(await response.Content.ReadAsStringAsync());
        }

    }
}
