using Microsoft.AspNetCore.Mvc;
using SerieA.WebUI.ViewModels;
using System.Net.Http.Json;

namespace SerieA.WebUI.Controllers
{
    public class MatchController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MatchController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Index", "Fixtures");
            }

            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync(
                $"https://localhost:7126/api/Matches/{id}"
            );

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var match = await response.Content
                .ReadFromJsonAsync<MatchDetailViewModel>();

            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }
    }
}