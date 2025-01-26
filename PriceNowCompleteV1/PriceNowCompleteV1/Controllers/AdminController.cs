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

        [HttpGet("runFullSuite")]
        public void RunFullSuite()//this will create all scrapers in time
        {
            var merchant = new Merchant
            {
                MerchantId = 6,
                Name = "CorkBP",
                Url = "https://www.chadwicks.ie",
                ContactEmail = "support@chadwicks.ie",
                Prices = new List<Price>(),
                Loggings = new List<Logging>()
            };

            try
            {
                IWebScraper scraper = WebScraperFactory.CreateScraper(merchant.Name);
                scraper.RunFullScrape(merchant);
            }
            catch (Exception e)
            {
                _loggingService.AddLog(new Logging
                {
                    MerchantId = merchant.MerchantId,
                    ScrapedAt = DateTime.UtcNow,
                    Status = "Run full suite failed",
                    ErrorMessage = e.Message
                });
            }
        }

        [HttpGet("runScraperByMerchant")]
        public async void RunScraperByMerchant(int merchantId)
        {
            var merchant = await _merchantService.GetMerchantById(merchantId);

            try
            {
                IWebScraper scraper = WebScraperFactory.CreateScraper(merchant.Name);
                await scraper.RunFullScrape(merchant);
            }
            catch (Exception e)
            {
               await _loggingService.AddLog(new Logging
                     {
                        MerchantId = merchant.MerchantId,
                        ScrapedAt = DateTime.UtcNow,
                        Status = "Scraping initiated",
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


        //    // GET: HomeController
        //    public ActionResult Index()
        //    {
        //        return View();
        //    }

        //    // GET: HomeController/Details/5
        //    public ActionResult Details(int id)
        //    {
        //        return View();
        //    }

        //    // GET: HomeController/Create
        //    public ActionResult Create()
        //    {
        //        return View();
        //    }

        //    // POST: HomeController/Create
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Create(IFormCollection collection)
        //    {
        //        try
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: HomeController/Edit/5
        //    public ActionResult Edit(int id)
        //    {
        //        return View();
        //    }

        //    // POST: HomeController/Edit/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Edit(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: HomeController/Delete/5
        //    public ActionResult Delete(int id)
        //    {
        //        return View();
        //    }

        //    // POST: HomeController/Delete/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Delete(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
    }
}
