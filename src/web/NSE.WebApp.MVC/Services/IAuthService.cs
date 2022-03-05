using NSE.WebApp.MVC.Models;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Services
{
    public interface IAuthService
    {
        Task<UsuarioRespostaLogin> Login(UsuarioLogin user);

        Task<UsuarioRespostaLogin> Registro(UsuarioRegistro user);
    }
}
