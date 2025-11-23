using System.Net.Http;
using System.Text.Json;
using WorkTimePanelFull.Application.Services;
using WorkTimePanelFull.Application.DTOs;

namespace WorkTimePanelFull.Infrastructure.Services
{
    public class ExternalPausaService : IExternalPausaService
    {
        private readonly IHttpClientFactory _http;

        public ExternalPausaService(IHttpClientFactory http)
        {
            _http = http;
        }

        private HttpClient Client() => _http.CreateClient("javaApi");

        public async Task<List<PausaDto>> GetRecentPausasAsync(int page = 1, int size = 50)
        {
            var client = Client();
            var resp = await client.GetAsync($"/api/pausas?page={page}&size={size}");
            if (!resp.IsSuccessStatusCode) return new List<PausaDto>();
            var s = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<PausaDto>>(s, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<PausaDto>();
        }

        public async Task<List<dynamic>> GetPausasSummaryByDayAsync(int days = 7)
        {
            var client = Client();
            var resp = await client.GetAsync($"/api/pausas/summary/days?days={days}");
            if (!resp.IsSuccessStatusCode) return new List<dynamic>();
            var s = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<dynamic>>(s, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<dynamic>();
        }

        public async Task<List<dynamic>> GetWeeklySummaryAsync()
        {
            var client = Client();
            var resp = await client.GetAsync($"/api/pausas/summary/week");
            if (!resp.IsSuccessStatusCode) return new List<dynamic>();
            var s = await resp.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<dynamic>>(s, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<dynamic>();
        }
    }
}
