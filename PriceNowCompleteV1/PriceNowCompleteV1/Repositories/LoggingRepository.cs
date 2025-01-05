using PriceNowCompleteV1.Data;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Repositories
{
    public class LoggingRepository : Repository<Logging>, ILoggingRepository
    {
        private readonly PriceNowDbContext _context;
        public LoggingRepository(PriceNowDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
