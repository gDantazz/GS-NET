using WorkTimePanelFull.Application.DTOs;

namespace WorkTimePanelFull.Application.Services
{
    public interface IExternalPausaService
    {
        Task<List<PausaDto>> GetRecentPausasAsync(int page = 1, int size = 50);
        Task<List<dynamic>> GetPausasSummaryByDayAsync(int days = 7);
        Task<List<dynamic>> GetWeeklySummaryAsync();
    }
}
