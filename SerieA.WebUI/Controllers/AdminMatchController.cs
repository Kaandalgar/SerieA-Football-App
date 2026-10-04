using Microsoft.AspNetCore.Mvc;
using SerieA.WebUI.ViewModels;
using System.Net.Http.Json;

namespace SerieA.WebUI.Controllers
{
    public class AdminMatchController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiBase = "https://localhost:7126/api";

        public AdminMatchController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var matches = await client.GetFromJsonAsync<List<AdminMatchApiViewModel>>(
                $"{ApiBase}/Matches") ?? new();

            var teams = await client.GetFromJsonAsync<List<TeamSummaryViewModel>>(
                $"{ApiBase}/Teams") ?? new();

            var vm = new AdminMatchPageViewModel
            {
                Teams = teams.OrderBy(x => x.Name).ToList(),
                Matches = matches
                    .OrderBy(x => x.Week)
                    .ThenBy(x => x.MatchDate)
                    .Select(x => new AdminMatchListItemViewModel
                    {
                        Id = x.Id,
                        Week = x.Week,
                        MatchDate = x.MatchDate,
                        HomeScore = x.HomeScore,
                        AwayScore = x.AwayScore,
                        Status = x.Status,
                        Stadium = x.Stadium,
                        HomeTeamId = x.HomeTeamId,
                        AwayTeamId = x.AwayTeamId,
                        HomeTeam = teams.FirstOrDefault(t => t.Id == x.HomeTeamId),
                        AwayTeam = teams.FirstOrDefault(t => t.Id == x.AwayTeamId)
                    }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdminMatchFormViewModel model)
        {
            if (model.HomeTeamId == model.AwayTeamId)
            {
                TempData["Error"] = "Ev sahibi ve deplasman takımı aynı olamaz.";
                return RedirectToAction(nameof(Index));
            }

            NormalizeScore(model);
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync($"{ApiBase}/Matches", model);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = await ReadError(response);
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Maç başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdminMatchFormViewModel model)
        {
            if (model.Id <= 0)
                return RedirectToAction(nameof(Index));

            if (model.HomeTeamId == model.AwayTeamId)
            {
                TempData["Error"] = "Ev sahibi ve deplasman takımı aynı olamaz.";
                return RedirectToAction(nameof(Index));
            }

            NormalizeScore(model);
            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{ApiBase}/Matches/{model.Id}", model);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = await ReadError(response);
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Maç başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{ApiBase}/Matches/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = await ReadError(response);
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Maç silindi.";
            return RedirectToAction(nameof(Index));
        }

        private static void NormalizeScore(AdminMatchFormViewModel model)
        {
            if (model.Status != "Finished" && model.Status != "Live")
            {
                model.HomeScore = null;
                model.AwayScore = null;
            }
        }

        private static async Task<string> ReadError(HttpResponseMessage response)
        {
            var text = await response.Content.ReadAsStringAsync();
            return string.IsNullOrWhiteSpace(text) ? "İşlem sırasında bir hata oluştu." : text;
        }
    }
}
