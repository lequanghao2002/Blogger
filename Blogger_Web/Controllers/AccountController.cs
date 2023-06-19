using Blogger_Web.Models.AccountViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Blogger_Web.Controllers
{
    public class AccountController : Controller
    {
        public AccountController() 
        {
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if(ModelState.IsValid)
            {
                
            }
            return View();
        }
    }
}
