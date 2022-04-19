using Microsoft.AspNetCore.Mvc;
using NSE.Clientes.API.Application.Commands;
using NSE.Core.Mediator;
using NSE.WebAPI.Core.Controllers;
using System.Threading.Tasks;

namespace NSE.Clientes.API.Controllers
{
    [Route("clientes")]
    public class ClienteController : MainController
    {
        private readonly IMediatorHandler mediator;

        public ClienteController(IMediatorHandler mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await mediator.EnviarCommando(new RegistrarClienteCommand("leo", "teste@teste.com", "45873281890", System.Guid.NewGuid())); 

            return CustomResponse(result);
        }
    }
}
