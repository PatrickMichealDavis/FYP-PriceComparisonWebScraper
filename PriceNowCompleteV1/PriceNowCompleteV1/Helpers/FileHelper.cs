using PriceNowCompleteV1.Models;
using System.Text.Json;

namespace PriceNowCompleteV1.Helpers
{
    public class FileHelper
    {

        public static async Task<List<Product>> GetJsonProducts(string filepath)
        {
            if (!File.Exists(filepath))
            {
                Console.WriteLine("Product file not found!");
                return new List<Product>();
            }

            string json = await File.ReadAllTextAsync(filepath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        public static async Task SaveProductsToFile(string filePath, List<Product> products)
        {
            //var products = await _productRepository.GetAll(); 

            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(filePath, json);
            Console.WriteLine($"Products saved to {filePath}");
        }
    }
}
