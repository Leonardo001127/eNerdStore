using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using System.Linq;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        public bool ResponseHasErrors(ResponseResult response)
        {
            if(response != null && response.Errors.Mensagens.Any())
            {
                response.Errors.Mensagens.ForEach(x => ModelState.AddModelError(string.Empty, x));

                return true;
            }
            return false; 
        }

    }
}