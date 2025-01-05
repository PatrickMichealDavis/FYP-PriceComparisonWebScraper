using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public Task AddMerchant(Merchant merchant)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMerchant(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Merchant>> GetAllMerchants()
        {
            throw new NotImplementedException();
        }

        public Task<Merchant> GetMerchantById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMerchant(Merchant merchant)
        {
            throw new NotImplementedException();
        }

       
    }
}
