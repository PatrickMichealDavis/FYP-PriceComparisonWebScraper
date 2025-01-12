using Microsoft.EntityFrameworkCore;
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

        public async Task AddLog(Logging log)
        {
            await _context.Loggings.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task Create(Logging log)
        {
            _context.Loggings.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var log = _context.Loggings.Find(id);
            if (log != null)
            {
                _context.Loggings.Remove(log);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Logging>> GetAll()
        {
            return await _context.Loggings.ToListAsync();
        }

        public async Task<Logging> GetById(int id)
        {
            return await _context.Loggings.FindAsync(id);
        }

        public async Task Update(Logging entity)
        {
            _context.Loggings.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}

