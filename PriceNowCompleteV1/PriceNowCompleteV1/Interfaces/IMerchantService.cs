using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Interfaces
{
    public interface IMerchantService
    {
        Task<IEnumerable<Merchant>> GetAllMerchants();
        Task<Merchant> GetMerchantById(int id);
        Task AddMerchant(Merchant merchant);
        Task UpdateMerchant(Merchant merchant);
        Task DeleteMerchant(int id);
    }
}
