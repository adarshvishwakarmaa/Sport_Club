using Microsoft.AspNetCore.Mvc;
using My_Sport_Club.Helpers;
using My_Sport_Club.Models.Domains;
using Newtonsoft.Json;

namespace My_Sport_Club.Controllers
{
    public class MemberShipController : Controller
    {
        static Helper api = new Helper();
        Global global = new Global();
        private readonly IConfiguration _configuration;

        public MemberShipController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {

            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.GetAsync("api/MemberApi"))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var result = resp.Content.ReadAsStringAsync().Result;
                            var data = JsonConvert.DeserializeObject<List<MemberShip>>(result);
                            return View(data.ToList());
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MemberShip player)
        {
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.PostAsJsonAsync("api/MemberApi", player))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            MemberShip player = new();
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.GetAsync($"api/MemberApi/{id}"))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var result = resp.Content.ReadAsStringAsync().Result;
                            player = JsonConvert.DeserializeObject<MemberShip>(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return View(player);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(MemberShip player)
        {
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.PutAsJsonAsync<MemberShip>($"api/MemberApi/{player.Id}", player))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return View(player);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            MemberShip player = new();
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.GetAsync($"api/MemberApi/{id}"))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var result = resp.Content.ReadAsStringAsync().Result;

                            player = JsonConvert.DeserializeObject<MemberShip>(result);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return View(player);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.DeleteAsync($"api/MemberApi/{id}"))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return BadRequest();
        }
    }
}
