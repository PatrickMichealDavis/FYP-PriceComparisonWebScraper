using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
       
        Task AddMultipleProducts(List<Product> products);
        Task<Product> GetProductByName(string name);

        Task<Product> GetProductByDescription(string description);
        Task SaveProductsToFile(string filePath, List<Product> products);
        Task<List<Product>> LoadProductsFromFile(string filePath);

    }
}
