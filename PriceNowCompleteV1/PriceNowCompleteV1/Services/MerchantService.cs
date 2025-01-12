using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;
using PriceNowCompleteV1.Repositories;

namespace PriceNowCompleteV1.Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantRepository _merchantRepository;

        public MerchantService(IMerchantRepository merchantRepository)
        {
            _merchantRepository = merchantRepository;
        }

        public async Task AddMerchant(Merchant merchant)
        {
            await _merchantRepository.Create(merchant);
        }

        public async Task DeleteMerchant(int id)
        {
            var merchant = _merchantRepository.GetById(id);
            if (merchant == null)
            {
                throw new Exception("Merchant not found");
            }
            await _merchantRepository.Delete(id);
        }

        public async Task<IEnumerable<Merchant>> GetAllMerchants()
        {
            return await _merchantRepository.GetAll();
        }

        public async Task<Merchant> GetMerchantById(int id)
        {
            var merchant = await _merchantRepository.GetById(id);
            if (merchant == null)
            {
                throw new Exception("Merchant not found");
            }
            return merchant;
        }

        public async Task UpdateMerchant(Merchant merchant)
        {
            await _merchantRepository.Update(merchant);
        }

       
    }
}
