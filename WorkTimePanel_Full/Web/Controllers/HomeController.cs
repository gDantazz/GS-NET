using Microsoft.AspNetCore.Mvc;
using WorkTimePanelFull.Application.Services;

namespace WorkTimePanelFull.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IExternalPausaService _external;
        public HomeController(IExternalPausaService external) { _external = external; }

        public async Task<IActionResult> Index()
        {
            var pauses = await _external.GetRecentPausasAsync(1,50);
            var byDay = await _external.GetPausasSummaryByDayAsync(7);
            var weekly = await _external.GetWeeklySummaryAsync();
            ViewData["PausasJson"] = System.Text.Json.JsonSerializer.Serialize(pauses);
            ViewData["ByDayJson"] = System.Text.Json.JsonSerializer.Serialize(byDay);
            ViewData["WeeklyJson"] = System.Text.Json.JsonSerializer.Serialize(weekly);
            return View();
        }
    }
}
