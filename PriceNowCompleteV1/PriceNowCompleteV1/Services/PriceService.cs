using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;
using PriceNowCompleteV1.Repositories;

namespace PriceNowCompleteV1.Services
{
    public class PriceService : IPriceService
    {
        private readonly IPriceRepository _priceRepository;

        public PriceService(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public async Task AddPrice(Price price)
        {
           await _priceRepository.Create(price);
        }

        public Task DeletePrice(int id)
        {
            var price = _priceRepository.GetById(id);
            if (price == null)
            {
                throw new Exception("Price not found");
            }
            return _priceRepository.Delete(id);
        }

        public async Task<IEnumerable<Price>> GetAllPrices()
        {
            return await _priceRepository.GetAll();
        }

        public async Task<Price> GetPriceById(int id)
        {
            var price = await _priceRepository.GetById(id);
            if (price == null)
            {
                throw new Exception("Price not found");
            }
            return price;
        }

        public async Task UpdatePrice(Price price)
        {
            await _priceRepository.Update(price);
        }

        
    }
}
