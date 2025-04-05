using Microsoft.AspNetCore.Mvc;
using My_Sport_Club.Helpers;
using My_Sport_Club.Models.Domains;

namespace My_Sport_Club.Controllers
{
    public class RegisterController : Controller
    {
        static Helper api = new Helper();
        Global global = new Global();
        private readonly IConfiguration _configuration;

        public RegisterController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Register() //template choose create
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.PostAsJsonAsync("api/RegisterApi", register))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Register");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return View();
        }
    }
}

