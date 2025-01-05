using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Data
{
    public class PriceNowDbContext : DbContext
    {
        public PriceNowDbContext(DbContextOptions<PriceNowDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Logging> Loggings { get; set; }

        //adapt here patrick for foreign keys before migration 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Price>()
            .HasOne(p => p.Product)
            .WithMany(p => p.Prices)
            .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Price>()
                .HasOne(p => p.Merchant)
                .WithMany(m => m.Prices)
                .HasForeignKey(p => p.MerchantId);

            modelBuilder.Entity<Logging>()
                .HasOne(l => l.Merchant)
                .WithMany(m => m.Loggings)
                .HasForeignKey(l => l.MerchantId);
        }
    }
   
}
