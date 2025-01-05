using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Interfaces
{
    public interface ILoggingService
    {
        Task<IEnumerable<Logging>> GetAllLogs();
        Task<Logging> GetLogById(int id);
        Task AddLog(Logging log);
        Task UpdateLog(Logging log);
        Task DeleteLog(int id);
    }
}
