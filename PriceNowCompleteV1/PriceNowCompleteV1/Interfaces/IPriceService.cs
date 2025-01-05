using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Interfaces
{
    public interface IPriceService
    {
        Task<IEnumerable<Price>> GetAllPrices();
        Task<Price> GetPriceById(int id);
        Task AddPrice(Price price);
        Task UpdatePrice(Price price);
        Task DeletePrice(int id);
    }
}
