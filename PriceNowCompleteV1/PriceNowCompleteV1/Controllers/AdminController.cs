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

        [HttpGet("runFullSuite")]
        public async Task<IActionResult> RunFullSuite()//this will create all scrapers in time
        {
            var merchantId = 3;
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
                return Ok("Scraping initiated");
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
            return StatusCode(500, $"Error while Scraping for:{merchant.Name}");
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
            string chadwicksSanitizedProductsFilePath = "chadwicksSanitizedProducts.json";
            string corkbpSanitizedProductsFilePath = "corkbpSanitizedProducts.json";
            string tjomahonySanitizedProductsFilePath = "tjomahonySanitizedProducts.json";

            //var products = await _productService.LoadProductsFromFile(corkbpSanitizedProductsFilePath);
            var products = await _productService.LoadProductsFromFile(tjomahonySanitizedProductsFilePath);
           // var products = await _productService.LoadProductsFromFile(chadwicksSanitizedProductsFilePath);

            await _productService.ProcessProducts(products);

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
