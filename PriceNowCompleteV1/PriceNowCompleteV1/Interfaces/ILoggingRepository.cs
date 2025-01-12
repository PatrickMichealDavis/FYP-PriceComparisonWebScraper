using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Interfaces
{
    public interface ILoggingRepository : IRepository<Logging>
    {
        Task AddLog(Logging log);
    }
}
