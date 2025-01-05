using PriceNowCompleteV1.Data;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Repositories
{
    public class PriceRepository : Repository<Price>, IPriceRepository
    {
        private readonly PriceNowDbContext _context;
        public PriceRepository(PriceNowDbContext context) : base(context)
        {
            _context = context;
        }


    }
   
}
