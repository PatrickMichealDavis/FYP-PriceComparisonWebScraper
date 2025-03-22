using PriceNowCompleteV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriceNowTest
{
    public class TestDataGenerator
    {
        public static List<Product> GenerateProducts(int productCount,string category)
        {
            var random = new Random(0);
            var products = new List<Product>();

            for (int i = 0; i < productCount; i++)
            {
                var name = $"product {i} ";
                var unit = $"{random.Next(1, 10)} x {random.Next(1, 10)} x {random.Next(1, 20)} ";

                var price = new Price
                {
                    MerchantId = random.Next(1, 5),
                    PriceValue = (decimal)random.NextDouble() * 100,
                    ScrapedAt = DateTime.UtcNow
                };

                var product = new Product
                {
                    ProductId = i,
                    Name = name,
                    Category=category,
                    Unit = unit,
                    Description = $"{name} {unit}",
                    Prices = new List<Price>() { price }

                };
                products.Add(product);
            }

            return products;

        }
    }
}

