using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers
{
    public class HomeController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("sistema-indisponivel")]
        public IActionResult SistmaIndsp()
        {
            var model = new ErrorViewModel
            {
                Mensagem = "Olá, infelizmente nosso sistema está temporariamente indisponível, tente voltar mais tarde",
                ErrorCode = 500,
                Title = "Sistema indisponível"
            };
            return View("Error", model);
        }


        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var erro = new ErrorViewModel();

            switch (id)
            {
                case 500:
                    erro.Mensagem = "Ocorreu um erro, tente mais tarde";
                    erro.Title = "Ocorreu um erro!";
                    erro.ErrorCode = id;
                    break;
                case 404:
                    erro.Mensagem = "Opa, a página que está procurando não existe < br/> Em caso de dúvidas entre em contato com nosso suporte";
                    erro.Title = "Página não encontrada";
                    erro.ErrorCode = id;
                    break;
                case 403:
                    erro.Mensagem = "Você não tem permissão para acessar essa página";
                    erro.Title = "Acesso negado";
                    erro.ErrorCode = id;
                    break;
                default:
                    return StatusCode(404);  
            }

            return View("Error", erro);
        }
    }
}
