using PriceNowCompleteV1.DataParsers;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;

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

        //testOnly Patrick
        public async Task SaveProductsToFile(string filePath,List<Product> products)
        {
            //var products = await _productRepository.GetAll(); 

            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(filePath, json);
            Console.WriteLine($"Products saved to {filePath}");
        }

        public async Task<List<Product>> LoadProductsFromFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Product file not found!");
                return new List<Product>();
            }

            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }


        //ensure that the products are in the same category sudo code belong in scraper
        //var categorizedProducts = products.GroupBy(p => p.Category).ToList();
        //foreach(var category in categorizedProducts)
        //{
        //   var productsByCategory = allProducts.Where(p => p.Category == category).ToList();
        //      pass this to processProducts(productsByCategory)
        //}
        public async Task ProcessProducts(List<Product> scrapedProducts)
        {
            var existingProducts = await _productRepository.GetAll();
            int updateCount = 0;
            int newCount = 0;

            if (existingProducts.Count() == 0)//first scrape
            {
                await AddMultipleProducts(scrapedProducts);
                return;
            }

            var category = scrapedProducts.FirstOrDefault()?.Category;
            var existingProductsByCategory = existingProducts.Where(p => p.Category == category).ToList();

            if (existingProductsByCategory.Count() == 0)//first scrape for category
            {
                await AddMultipleProducts(scrapedProducts);
                return;
            }

            foreach (var scrapedProduct in scrapedProducts) 
            {
                var matchFound = false;
                var existingProductsByUnit = existingProductsByCategory.Where(p => DataParser.CheckForCloseComparrisonUnit(p.Unit, scrapedProduct.Unit)).ToList();

                foreach (var existingProduct in existingProductsByUnit)//repo
                {
                    if (DataParser.CheckForCloseComparrison(scrapedProduct, existingProduct))
                    {
                        existingProduct.Prices.Add(scrapedProduct.Prices.First());
                        await UpdateProduct(existingProduct);
                        updateCount++;
                        matchFound = true;
                       // Console.WriteLine($"Updating product in repo update count {updateCount}");
                    }
                    
                }
                if (!matchFound)
                {
                   await _productRepository.AddProduct(scrapedProduct);
                }

            }

        }

        public async Task ProcessProductsV2(List<Product> scrapedProducts)//products are pre-sanitized entering this function
        {
            var existingProducts = await _productRepository.GetAll();

            if (existingProducts.Count() == 0)//first scrape
            {
                await AddMultipleProducts(scrapedProducts);
                return;
            }

            var category = scrapedProducts.FirstOrDefault()?.Category;
            var existingProductsByCategory = existingProducts.Where(p => p.Category == category).ToList();

            if (existingProductsByCategory.Count() == 0)//first scrape for category
            {
                await AddMultipleProducts(scrapedProducts);
                return;
            }

            var existingProductsByUnitDict = existingProductsByCategory.GroupBy(p => p.Unit).ToDictionary(p => p.Key, p => p.ToList());

            foreach (var scrapedProduct in scrapedProducts)
            {
                var closeKey = DataParser.FindClosestKey(scrapedProduct.Unit, existingProductsByUnitDict.Keys.ToList());

                if (closeKey == null)//is this sting.Emtpy patrick?
                {
                    await _productRepository.AddProduct(scrapedProduct);
                    continue;
                }

                var closeExistingProducts = existingProductsByUnitDict[closeKey];

                var matchedProduct = closeExistingProducts.FirstOrDefault(p => DataParser.CheckForCloseComparrison(scrapedProduct, p));//this may be too hacky patrick first or default may be wrong

                if (matchedProduct != null) 
                {

                    var newPrice = scrapedProduct.Prices.First();
                    var existingPrice = matchedProduct.Prices.FirstOrDefault(p => p.MerchantId == newPrice.MerchantId);

                    if (existingPrice != null)
                    {
                        existingPrice.PriceValue = newPrice.PriceValue;
                        existingPrice.ScrapedAt = newPrice.ScrapedAt;
                    }
                    else
                    {
                        matchedProduct.Prices.Add(newPrice);
                    }
                    await _productRepository.Update(matchedProduct);
                    Console.WriteLine($"Updating product in repo {matchedProduct.Name}");
                }
                else
                {
                    await _productRepository.AddProduct(scrapedProduct); 
                }

                //may be quicker to add batches after loop need to add multiple upadtes to Irepository
            }

        }

        public async Task ProcessProductsPartial(List<Product> scrapedProducts)//products are pre-sanitized entering this function
        {
            var existingProducts = await _productRepository.GetAll();
            var count = 0;
            var category = scrapedProducts.FirstOrDefault()?.Category;
            var existingProductsByCategory = existingProducts.Where(p => p.Category == category).ToList();

            var existingProductsByUnitDict = existingProductsByCategory.GroupBy(p => p.Unit).ToDictionary(p => p.Key, p => p.ToList());

            foreach (var scrapedProduct in scrapedProducts)
            {
                var closeKey = DataParser.FindClosestKey(scrapedProduct.Unit, existingProductsByUnitDict.Keys.ToList());

                if (closeKey == string.Empty)
                {
                    continue;
                }

                var closeExistingProducts = existingProductsByUnitDict[closeKey];

                var matchedProduct = closeExistingProducts.FirstOrDefault(p => DataParser.CheckForCloseComparrison(scrapedProduct, p));//this may be too hacky patrick first or default may be wrong

               
                if (matchedProduct != null)
                {

                    var newPrice = scrapedProduct.Prices.First();
                    var existingPrice = matchedProduct.Prices.FirstOrDefault(p => p.MerchantId == newPrice.MerchantId);

                    if (existingPrice != null)
                    {
                        existingPrice.PriceValue = newPrice.PriceValue;
                        existingPrice.ScrapedAt = newPrice.ScrapedAt;
                        count++;
                        //await _productRepository.Update(matchedProduct);
                        Console.WriteLine($"Updating product in repo {matchedProduct.Name} count {count}");
                    }
                   
                   
                }
               

                //may be quicker to add batches after loop need to add multiple upadtes to Irepository
            }
            
        }

        public async Task<IEnumerable<Product>> GetAllProductsWithPriceAndMerchant()
        {
            return await _productRepository.GetAllProductsWithPriceAndMerchant();
        }
    }
}
