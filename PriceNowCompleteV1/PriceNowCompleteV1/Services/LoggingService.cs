using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;
using PriceNowCompleteV1.Repositories;

namespace PriceNowCompleteV1.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingRepository _loggingRepository;

        public LoggingService(ILoggingRepository loggingRepository)
        {
            _loggingRepository = loggingRepository;
        }

        public async Task AddLog(Logging log)
        {
            await _loggingRepository.AddLog(log);
        }

        public async Task DeleteLog(int id)
        {
            var log = await _loggingRepository.GetById(id);
            if (log == null)
            {
                throw new Exception("log not found");
            }
            await _loggingRepository.Delete(id);
        }

        public async Task<IEnumerable<Logging>> GetAllLogs()
        {
           return await _loggingRepository.GetAll();
        }

        public async Task<Logging> GetLogById(int id)
        {
            var log = await _loggingRepository.GetById(id);
            if (log == null)
            {
                throw new Exception("log not found");
            }
            return log;
        }

        public async Task UpdateLog(Logging log)
        {
            await _loggingRepository.Update(log);
        }
    }
}
