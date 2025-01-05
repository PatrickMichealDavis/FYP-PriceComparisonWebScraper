using Microsoft.EntityFrameworkCore;
using PriceNowCompleteV1.Data;
using PriceNowCompleteV1.Interfaces;

namespace PriceNowCompleteV1.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly PriceNowDbContext _context;

        public Repository(PriceNowDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Create(T entity)
        {
           await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var foundEnity = await GetById(id);

            if(foundEnity != null)
            {
                _context.Set<T>().Remove(foundEnity);
                await _context.SaveChangesAsync();
            }
        }
    }
    
}
