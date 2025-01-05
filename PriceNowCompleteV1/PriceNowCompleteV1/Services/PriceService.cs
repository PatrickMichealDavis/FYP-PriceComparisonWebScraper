using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Services
{
    public class PriceService : IPriceService
    {
        private readonly IPriceRepository _priceRepository;

        public PriceService(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public Task AddPrice(Price price)
        {
            throw new NotImplementedException();
        }

        public Task DeletePrice(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Price>> GetAllPrices()
        {
            throw new NotImplementedException();
        }

        public Task<Price> GetPriceById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePrice(Price price)
        {
            throw new NotImplementedException();
        }

        
    }
}
