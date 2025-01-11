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

        public async Task AddMultipleProducts(SortedSet<Product> products)
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
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
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
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FindAsync(id); 
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
        }
    }
   
}
