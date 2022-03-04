using NSE.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public interface IAuthService
    {
        Task<string> Login(UsuarioLogin user);

        Task<string> Registro(UsuarioRegistro user);
    }
}
