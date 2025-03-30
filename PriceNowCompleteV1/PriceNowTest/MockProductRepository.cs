using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceNowTest
{
    public class MockProductRepository : IProductRepository
    {

        private readonly List<Product> _products;
        public List<Product> _UpdatedProducts { get; set; } = new();
        public List<Product> _newProducts { get; set; } = new();


        public MockProductRepository(List<Product> initialProducts)
        {
            _products = initialProducts ?? new List<Product>();
        }

        public Task AddMultipleProducts(List<Product> products)
        {
            _products.AddRange(products);
             return Task.CompletedTask;
        }

        public Task AddProduct(Product product)
        {
            _products.Add(product);
            _newProducts.Add(product);
           // Console.WriteLine($"Product added {product}");
            return Task.CompletedTask;
        }

        public Task Create(Product entity)
        {
           throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAll()
        {
            return Task.FromResult<IEnumerable<Product>> (_products);
        }

        public Task<IEnumerable<Product>> GetAllProductsWithPriceAndMerchant()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByDescription(string description)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductWithPrices(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Product entity)
        {
            _UpdatedProducts.Add(entity);
           // Console.WriteLine($"Product updated {entity}");
            return Task.CompletedTask;
        }
    }
}
