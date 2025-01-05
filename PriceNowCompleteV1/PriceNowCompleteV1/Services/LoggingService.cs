using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingRepository _loggingRepository;

        public LoggingService(ILoggingRepository loggingRepository)
        {
            _loggingRepository = loggingRepository;
        }

        public Task AddLog(Logging log)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLog(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Logging>> GetAllLoggings()
        {
            return await _loggingRepository.GetAll();
        }

        public Task<IEnumerable<Logging>> GetAllLogs()
        {
            throw new NotImplementedException();
        }

        public Task<Logging> GetLogById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLog(Logging log)
        {
            throw new NotImplementedException();
        }

       
    }
}
