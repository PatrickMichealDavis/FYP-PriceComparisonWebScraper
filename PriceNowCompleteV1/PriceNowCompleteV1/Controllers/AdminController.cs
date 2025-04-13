using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceNowCompleteV1.DataParsers;
using PriceNowCompleteV1.DTOs;
using PriceNowCompleteV1.Helpers;
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
        private readonly IGenericWebScraper _scraper;

        public AdminController(
           IProductService productService,
           IPriceService priceService,
           IMerchantService merchantService,
           ILoggingService loggingService,
           IGenericWebScraper scraper)
        {
            _productService = productService;
            _priceService = priceService;
            _merchantService = merchantService;
            _loggingService = loggingService;
            _scraper = scraper;
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
            var merchants = await _merchantService.GetAllMerchants();

            if(logs == null|| merchants ==null)
            {
                return NotFound();
            }

            foreach (var log in logs)
            {
                log.Merchant = merchants.FirstOrDefault(m => m.MerchantId == log.MerchantId);
            }
            return Ok(logs);
        }

        [HttpPost("compare")]
        public async Task<ActionResult<IEnumerable<Product>>> Compare([FromBody] int[] productList)
        {
            var products = await _productService.GetAllProductsWithPriceAndMerchant();

            var productsToCompare = products.Where(p => productList.Contains(p.ProductId)).ToList();

            if (productsToCompare.Count == 0)
            {
                return NotFound("No products found");
            }

            var productsWithPrices = productsToCompare.Where(p => p.Prices.Count > 1).ToList();//change here to 2 and see if any products did match https://tjomahony.ie/4-8m-50mm-x-44mm-rough-timber-16-2-x-2-030504448.html

            return Ok(productsToCompare);
        }

        [HttpPost("priceNow")]
        public async Task<IActionResult> PriceNow([FromBody] ProductDTO product)
        {
            if (product == null)
            {
                return BadRequest("Product cannot be null");
            }

            foreach (var price in product.Prices)
            {
                try
                {
                    price.PriceValue = await _scraper.PriceNow(price.ProductUrl);
                    price.ScrapedAt = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Price now failed for merchant {price.MerchantId} → {ex.Message}");
                   
                }

            }
            return Ok(product);
        }

        [HttpGet("runFullSuite")]
        public async Task<IActionResult> RunFullSuite()//this will create all scrapers in time
        {
            var merchantId = 2;
            var merchant = await _merchantService.GetMerchantById(merchantId);

            try
            {
                IWebScraper scraper = WebScraperFactory.CreateScraper(merchant.Name, _productService, _loggingService);
                await scraper.RunFullScrapeByMerchant(merchant);
                return Ok("Scraping initiated");//too quick?
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
            return StatusCode(500, $"Error while Scraping for:{merchant.Name}");

        }

        [HttpGet("runScraperByMerchant")]
        public async Task<IActionResult> RunScraperByMerchant(int merchantId, bool isPartial)
        {
            var merchant = await _merchantService.GetMerchantById(merchantId);
            if (merchant == null)
            {
                return NotFound($"No Merchant with id:{merchantId}");
            }

            var maxAttempts = 2;

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
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
                    return Ok($"Scraping initiated after {attempt} attempt");
                }
                catch (Exception e)
                {
                    await _loggingService.AddLog(new Logging
                    {
                        MerchantId = merchant.MerchantId,
                        ScrapedAt = DateTime.UtcNow,
                        Status = $"RunScraperByMerchant: {merchant.Name} Failed after {attempt} attempts",
                        ErrorMessage = e.Message
                    });

                    if (attempt == maxAttempts)
                    {
                        return StatusCode(500, $"Error while Scraping for:{merchant.Name} after {maxAttempts} attempts");
                    }
                }
               
            }
            return StatusCode(500, $"Error while Scraping for:{merchant.Name}");
        }

        [HttpGet("testAddProduct")]
        public async Task<IActionResult> TestAddProduct()
        {
           

            Price price = new Price
            {
                PriceValue = 2,
                MerchantId = 2,
                ProductUrl = "https://tjomahony.ie/4-8m-50mm-x-44mm-rough-timber-16-2-x-2-030504448.html",
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



            await _productService.AddProduct(testObject);
            return Ok("Test product added successfully.");
        }

        [HttpGet("testFuzzy")]
        public async Task<IActionResult> TestFuzzyComparison()
        {
            string chadwicksSanitizedProductsFilePath = "chadwicksSanitizedProducts.json";
            string corkbpSanitizedProductsFilePath = "corkbpSanitizedProducts.json";
            string tjomahonySanitizedProductsFilePath = "tjomahonySanitizedProducts.json";
            string chadwicksRawProductsFilePath = "chadwicksRawProducts.json";
            var resultsFilePath = "results.json";

            var products = await _productService.LoadProductsFromFile(corkbpSanitizedProductsFilePath);
            //  var products = await _productService.LoadProductsFromFile(tjomahonySanitizedProductsFilePath);
            //var products = await _productService.LoadProductsFromFile(chadwicksRawProductsFilePath);

           
            await _productService.ProcessProductsV2(products);

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
