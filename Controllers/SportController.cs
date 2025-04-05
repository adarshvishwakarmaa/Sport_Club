using Microsoft.AspNetCore.Mvc;
using My_Sport_Club.Helpers;
using My_Sport_Club.Models.Domains;
using Newtonsoft.Json;

namespace My_Sport_Club.Controllers
{
    public class SportController : Controller
    {
        static Helper api = new Helper();
        Global global = new Global();
        private readonly IConfiguration _configuration;

        public SportController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult> Index()
        {

            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.GetAsync("api/SportApi"))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var result = resp.Content.ReadAsStringAsync().Result;
                            var data = JsonConvert.DeserializeObject<List<Sport>>(result);
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
        public async Task<IActionResult> Create(Sport sports)
        {
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.PostAsJsonAsync("api/SportApi", sports))
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
            Sport sports = new();
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.GetAsync($"api/SportApi/{id}"))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var result = resp.Content.ReadAsStringAsync().Result;
                            sports = JsonConvert.DeserializeObject<Sport>(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return View(sports);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Sport sports)
        {
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.PutAsJsonAsync<Sport>($"api/SportApi/{sports.ID}", sports))
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
            return View(sports);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Sport sports = new();
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.GetAsync($"api/SportApi/{id}"))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var result = resp.Content.ReadAsStringAsync().Result;

                            sports = JsonConvert.DeserializeObject<Sport>(result);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return View(sports);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.DeleteAsync($"api/SportApi/{id}"))
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
        [HttpGet]
        public async Task<IActionResult> GetData(string alldata)
        {
            var alldataArr = alldata.Split("^");
            int pageIndex = Convert.ToInt32(alldataArr[0]);
            int pageSize = Convert.ToInt32(alldataArr[1]);
            string sport = alldataArr[2];
            string TeamA = alldataArr[3];
            string TeamB = alldataArr[4];
            string Location = alldataArr[5];
            string CoachName = alldataArr[6];
            string VenueName = alldataArr[7];
            string EventName = alldataArr[8];
            string Description = alldataArr[9];


            List<Sport> filterStudentData = await GetAllData();
            #region Searching
            if (!string.IsNullOrEmpty(sport))
            {
                filterStudentData = filterStudentData.Where(s => s.sport.Contains(sport, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(TeamA))
            {
                filterStudentData = filterStudentData.Where(s => s.TeamA.Contains(TeamA, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(TeamB))
            {
                filterStudentData = filterStudentData.Where(s => s.TeamB.Contains(TeamB, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Location))
            {
                filterStudentData = filterStudentData.Where(s => s.Location.Contains(Location, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(CoachName))
            {
                filterStudentData = filterStudentData.Where(s => s.CoachName.Contains(CoachName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(VenueName))
            {
                filterStudentData = filterStudentData.Where(s => s.VenueName.Contains(VenueName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(EventName))
            {
                filterStudentData = filterStudentData.Where(s => s.EventName.Contains(EventName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(Description))
            {
                filterStudentData = filterStudentData.Where(s => s.Description.Contains(Description, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            #endregion
            #region Pagination
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            int allRecords = filterStudentData.Count();
            int startPage = pageIndex - 5;
            int endPage = pageIndex + 4;
            int totalPage = (int)Math.Ceiling((decimal)allRecords / (decimal)pageSize);
            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;

            }
            if (endPage > totalPage)
            {
                endPage = totalPage;
                if (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }
            #endregion
            ViewBag.AllRecord = allRecords;
            ViewBag.TotalPage = totalPage;
            ViewBag.CurrentPage = pageIndex;
            ViewBag.EndPage = endPage;
            ViewBag.StartPage = startPage;
            int excludedRecord = (pageIndex - 1) * pageSize;
            filterStudentData = filterStudentData.Skip(excludedRecord).Take(pageSize).ToList();
            ViewBag.ShowRecord = filterStudentData.Count();
            ViewBag.getAllData = filterStudentData.ToList();
            return PartialView("SportPagination");



        }
        [HttpGet]
        public async Task<List<Sport>> GetAllData()
        {
            List<Sport> list = new();
            try
            {
                using (HttpClient client = api.Initial())
                {
                    using (HttpResponseMessage resp = await client.GetAsync("api/SportApi"))
                    {
                        if (resp.IsSuccessStatusCode)
                        {
                            var result = resp.Content.ReadAsStringAsync().Result;
                            list = JsonConvert.DeserializeObject<List<Sport>>(result);
                            return list;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return null;

        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Login");
        }
    }
}



