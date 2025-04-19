using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using PriceNowCompleteV1.Data;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private readonly PriceNowDbContext _context;
        public ProductRepository(PriceNowDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddMultipleProducts(List<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();
        }

        public async  Task AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task Create(Product product)
        {
            try
            {
                Console.WriteLine($"Adding product: {product.Name}");
                await _context.Products.AddAsync(product);
                var result = await _context.SaveChangesAsync();
                Console.WriteLine($"SaveChangesAsync result: {result}");
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"Error in AddProduct: {ex.ObjectName} - {ex.Message}");
                throw;
            }
        }

        public async Task Delete(int id)
        {
           var product = _context.Products.Find(id);
            if(product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.Include(p=>p.Prices).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsWithPriceAndMerchant()
        {
            return await _context.Products.Include(p => p.Prices).ThenInclude(pr => pr.Merchant).ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product; 
        }

        public async Task<Product> GetProductByDescription(string description)
        {
            return await _context.Products
                .Include(p => p.Prices)
                .FirstOrDefaultAsync(p => p.Description == description);
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _context.Products
                .Include(p => p.Prices)
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Product> GetProductWithPrices(int productId)
        {
            return await _context.Products
                .Include(p => p.Prices)
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task Update(Product entity)
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
            Console.WriteLine("made it past update");
        }

        public async Task UpdateMultipleProducts(List<Product> products)
        {
             _context.Products.UpdateRange(products);
            await _context.SaveChangesAsync();
        }
    }
   
}
