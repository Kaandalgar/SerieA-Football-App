using Microsoft.AspNetCore.Mvc;
using SerieA.WebUI.ViewModels;

namespace SerieA.WebUI.Controllers
{
    public class StandingsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StandingsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var standings = await client
                .GetFromJsonAsync<List<StandingViewModel>>(
                    "https://localhost:7126/api/Standings"
                );

            return View(standings);
        }
    }
}