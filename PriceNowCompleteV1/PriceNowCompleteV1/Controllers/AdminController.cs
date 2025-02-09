using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceNowCompleteV1.DataParsers;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;
using PriceNowCompleteV1.Scrapers;

namespace PriceNowCompleteV1.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : Controller
    {

        private readonly IProductService _productService;
        private readonly IPriceService _priceService;
        private readonly IMerchantService _merchantService;
        private readonly ILoggingService _loggingService;

        public AdminController(
           IProductService productService,
           IPriceService priceService,
           IMerchantService merchantService,
           ILoggingService loggingService)
        {
            _productService = productService;
            _priceService = priceService;
            _merchantService = merchantService;
            _loggingService = loggingService;
           
        }

        [HttpGet("getProducts")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("getAllMerchants")]
        public async Task<ActionResult<IEnumerable<Merchant>>> GetMerchants()
        {
            var merchants = await _merchantService.GetAllMerchants();
            return Ok(merchants);
        }

        [HttpGet("getAllLogs")]
        public async Task<ActionResult<IEnumerable<Logging>>> GetLogs()
        {
            var logs = await _loggingService.GetAllLogs();
            return Ok(logs);
        }

        [HttpPost("compare")]
        public async Task<ActionResult<IEnumerable<Product>>> Compare([FromBody] int[] productList)
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("runFullSuite")]
        public async Task RunFullSuite()//this will create all scrapers in time
        {
            var merchantId = 6;
            var merchant = await _merchantService.GetMerchantById(merchantId);

            try
            {
                IWebScraper scraper = WebScraperFactory.CreateScraper(merchant.Name,_productService,_loggingService);
                await scraper.RunFullScrapeByMerchant(merchant);
            }
            catch (Exception e)
            {
                await _loggingService.AddLog(new Logging
                {
                    MerchantId = merchant.MerchantId,
                    ScrapedAt = DateTime.UtcNow,
                    Status = "Run full suite failed",
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpGet("runScraperByMerchant")]
        public async void RunScraperByMerchant(int merchantId,bool isPartial)
        {
            var merchant = await _merchantService.GetMerchantById(merchantId);

            try
            {
                if (isPartial) 
                {
                    IWebScraper scraper = WebScraperFactory.CreateScraper(merchant.Name, _productService, _loggingService);
                    await scraper.RunPartialScrapeByMerchant(merchant);
                }
                else
                {
                    IWebScraper scraper = WebScraperFactory.CreateScraper(merchant.Name, _productService, _loggingService);
                    await scraper.RunFullScrapeByMerchant(merchant);

                }
            }
            catch (Exception e)
            {
               await _loggingService.AddLog(new Logging
                     {
                        MerchantId = merchant.MerchantId,
                        ScrapedAt = DateTime.UtcNow,
                        Status = "RunScraperByMerchant:" + merchant.Name + " Failed",
                        ErrorMessage = e.Message
               });
            }
        }

        [HttpGet("testAddProduct")]
        public async Task<IActionResult> TestAddProduct()
        {
            var merchant = new Merchant
            {
                Name = "CorkBP",
                Url = "https://www.corkbp.ie",
                ContactEmail = "N/A",
                Prices = new List<Price>(),
                Loggings = new List<Logging>()
            };

            Price price = new Price
            {
                PriceValue = 2,
                Merchant = merchant
            };

            List<Price> prices = new List<Price> { price };

            Product product = new Product
            {
                Name = "1.8m 75 x 75 Treated Post",
                Description = "Test",
                Category = "Test",
                Unit = "Test",
                Prices = prices
            };

            var testObject = DataParser.SanitizeProduct(product);

            //var Logging = new Logging
            //{
            //    MerchantId = 2,
            //    ScrapedAt = DateTime.UtcNow,
            //    Status = "Scraping initiated",
            //    ErrorMessage = "Test"
            //};

           // await _loggingService.AddLog(Logging);

            

            //await _productService.AddProduct(product);
            return Ok("Test product added successfully.");
        }
        
        [HttpGet("testFuzzy")]
        public async Task<IActionResult> TestFuzzyComparison()
        {
            //string existingProductsFilePath = "products.json";
            string repoProductsMockFilePath = "newproducts.json";
            string outcomeFilepath = "results.json";
            //string rawTimberProductsFilePath = "chadwicksRawProducts.json";
            string tjomaohnyProductsSanitizedFilePath = "tjomahonySanitizedProducts.json";
            string corkbpProductsSanitizedFilePath = "corkbpProducts.json";
            string chadwicksProductsSanitizedFilePath = "chadwicksTimberSanitizedProducts.json";

            var sanitizedProducts = new List<Product>();

            var mockScrapedProducts = await _productService.LoadProductsFromFile(chadwicksProductsSanitizedFilePath);
            var mockReop = await _productService.LoadProductsFromFile(corkbpProductsSanitizedFilePath);

            // var allProducts = await _productRepository.GetAll();
            var category = mockScrapedProducts.FirstOrDefault()?.Category;
            var productsByCategory = mockReop.Where(p => p.Category == category).ToList();
            var updatedProducts = new List<Product>();
            var repoProductsMock = new List<Product>();
            var counter = 1;

            foreach (var scrapedProduct in mockScrapedProducts)
            {
                var productsByUnit = productsByCategory.Where(p => DataParser.CheckForCloseComparrisonUnit(p.Unit, scrapedProduct.Unit)).ToList();

                foreach (var productByUnit in productsByUnit)//repo
                {
                    if (DataParser.CheckForCloseComparrison(scrapedProduct, productByUnit))
                    {
                        var newPrice = scrapedProduct.Prices.First();
                        newPrice.ProductId = counter;
                        productByUnit.Prices.Add(newPrice);
                        productByUnit.ProductId = counter;
                        updatedProducts.Add(productByUnit);
                        scrapedProduct.ProductId = counter;
                        counter++;


                        repoProductsMock.Add(scrapedProduct);
                        Console.WriteLine("Price updated in repo");
                        //update
                    }
                    else
                    {
                        Console.WriteLine("No close match found, adding new product");
                        //add new
                    }
                }

            }
            updatedProducts = updatedProducts.Distinct().ToList();
            repoProductsMock = repoProductsMock.Distinct().ToList();
            await _productService.SaveProductsToFile(outcomeFilepath, updatedProducts);
            await _productService.SaveProductsToFile(repoProductsMockFilePath, repoProductsMock);

            //await _productService.SaveProductsToFile(ProductsSanitizedFilePath, sanitizedProducts);
            //var existingProducts = await _productService.LoadProductsFromFile(existingProductsFilePath);

            return Ok();
        }

        [HttpGet("runFullSuitePartial")]
        public async Task<IActionResult> RunFullSuitePartial()
        {
            Console.WriteLine("Running partial scrape");
            return Ok();
        }



        }
}
