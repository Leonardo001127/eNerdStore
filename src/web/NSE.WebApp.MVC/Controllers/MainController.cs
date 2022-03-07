using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;
using System.Linq;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    { 
        public bool ResponseHasErrors(ResponseResult response)
            => response != null && response.Errors.Messages.Any();
        
    }
}
