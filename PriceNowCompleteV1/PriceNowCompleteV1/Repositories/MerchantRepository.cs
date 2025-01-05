using Microsoft.EntityFrameworkCore;
using PriceNowCompleteV1.Data;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Repositories
{
    public class MerchantRepository : Repository<Merchant>, IMerchantRepository
    {
        private readonly PriceNowDbContext _context;
        public MerchantRepository(PriceNowDbContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
