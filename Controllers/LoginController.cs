using Microsoft.AspNetCore.Mvc;
using My_Sport_Club.Models.Domains;
using My_Sport_Club.Models;

namespace My_Sport_Club.Controllers
{
    public class LoginController : Controller
    {
        private readonly SportDbContext SportDbContext;

        public LoginController(SportDbContext SportDbContext)
        {
            this.SportDbContext = SportDbContext;
        }

        [HttpGet]
        public IActionResult Login() //template choose create
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var alldata = SportDbContext.Registers.Where(x => x.Username == login.Username && x.Password == login.Password).FirstOrDefault();

            if (alldata != null)
            {
                return RedirectToAction("Index", "Sport");
            }
            else
            {
                ViewData["ValidateMessage"] = "Invalid username or password.";
                return View("Login");
                //return View();
            }
        }
    }
}


