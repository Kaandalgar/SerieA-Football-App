using Microsoft.AspNetCore.Mvc;
using SerieA.WebUI.ViewModels;
using System.Net.Http.Json;

namespace SerieA.WebUI.Controllers
{
    public class FixturesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FixturesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int week = 1)
        {
            var client = _httpClientFactory.CreateClient();

            var matches = await client
                .GetFromJsonAsync<List<MatchViewModel>>(
                    $"https://localhost:7126/api/Matches/week/{week}"
                );

            ViewBag.Week = week;

            return View(matches ?? new List<MatchViewModel>());
        }
    }
}