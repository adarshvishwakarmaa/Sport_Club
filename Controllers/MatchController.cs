using Microsoft.AspNetCore.Mvc;
using My_Sport_Club.Helpers;
using My_Sport_Club.Models.Domains;
using Newtonsoft.Json;

namespace My_Sport_Club.Controllers
{
    public class MatchController : Controller
    {
        static Helper api = new Helper();
        Global global = new Global();
        private readonly IConfiguration _configuration;

        public MatchController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {

            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.GetAsync("api/MatchApi"))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var result = resp.Content.ReadAsStringAsync().Result;
                            var data = JsonConvert.DeserializeObject<List<Match>>(result);
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
        public async Task<IActionResult> Create(Match player)
        {
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.PostAsJsonAsync("api/MatchApi", player))
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
            Match player = new();
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.GetAsync($"api/MatchApi/{id}"))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var result = resp.Content.ReadAsStringAsync().Result;
                            player = JsonConvert.DeserializeObject<Match>(result);
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
        public async Task<IActionResult> Edit(Match player)
        {
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.PutAsJsonAsync<Match>($"api/MatchApi/{player.ID}", player))
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
            Match player = new();
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.GetAsync($"api/MatchApi/{id}"))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var result = resp.Content.ReadAsStringAsync().Result;

                            player = JsonConvert.DeserializeObject<Match>(result);

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
                    using (HttpResponseMessage resp = await client.DeleteAsync($"api/MatchApi/{id}"))
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
