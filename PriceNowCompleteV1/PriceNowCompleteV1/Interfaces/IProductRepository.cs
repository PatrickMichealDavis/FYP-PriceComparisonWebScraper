using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductWithPrices(int id);
        Task AddProduct(Product product);

        Task AddMultipleProducts(List<Product> products);
    }
}
