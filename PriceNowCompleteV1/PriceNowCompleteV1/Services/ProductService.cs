using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddMultipleProducts(List<Product> products)
        {
            await _productRepository.AddMultipleProducts(products);
        }

        public async Task AddProduct(Product product)
        {
           await _productRepository.Create(product);
        }

        public Task DeleteProduct(int id)
        {
           var product = _productRepository.GetById(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return _productRepository.Delete(id);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productRepository.GetAll();
        }

        public Task<Product> GetProductByDescription(string description)
        {
           var product = _productRepository.GetProductByDescription(description);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        public async Task<Product> GetProductByName(string name)
        {
            var product = await _productRepository.GetProductByName(name);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        public async Task<Product> GetProductWithPrices(int productId)
        {
            return await _productRepository.GetProductWithPrices(productId);
        }

        public async Task UpdateProduct(Product product)
        {
             await _productRepository.Update(product);
        }

        
    }
}
