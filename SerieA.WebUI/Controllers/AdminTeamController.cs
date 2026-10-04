using Microsoft.AspNetCore.Mvc;
using SerieA.WebUI.ViewModels;
using System.Net.Http.Json;

namespace SerieA.WebUI.Controllers
{
    public class AdminTeamController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiBase = "https://localhost:7126/api";

        public AdminTeamController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var teams = await client.GetFromJsonAsync<List<TeamSummaryViewModel>>(
                $"{ApiBase}/Teams") ?? new List<TeamSummaryViewModel>();

            return View(new AdminTeamPageViewModel
            {
                Teams = teams.OrderBy(x => x.Name).ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminTeamFormViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                TempData["Error"] = "Takım adı boş bırakılamaz.";
                return RedirectToAction(nameof(Index));
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync($"{ApiBase}/Teams", model);

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode ? "Takım başarıyla eklendi." : await SafeError(response);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminTeamFormViewModel model)
        {
            if (model.Id <= 0)
                return RedirectToAction(nameof(Index));

            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{ApiBase}/Teams/{model.Id}", model);

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode ? "Takım başarıyla güncellendi." : await SafeError(response);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{ApiBase}/Teams/{id}");

            TempData[response.IsSuccessStatusCode ? "Success" : "Error"] =
                response.IsSuccessStatusCode ? "Takım silindi." : await SafeError(response);

            return RedirectToAction(nameof(Index));
        }

        private static async Task<string> SafeError(HttpResponseMessage response)
        {
            var text = await response.Content.ReadAsStringAsync();
            return string.IsNullOrWhiteSpace(text) ? "İşlem sırasında bir hata oluştu." : text;
        }
    }
}
