using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;
using PriceNowCompleteV1.Scrapers;

namespace PriceNowCompleteV1.Controllers
{
    public class AdminController : Controller
    {

        private readonly IProductService _productService;
        private readonly IPriceService _priceService;
        private readonly IMerchantService _merchantService;
        private readonly ILoggingService _loggingService;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet]
        public void RunFullSuite()//this will create all scrapers in time
        {
            var merchant = new Merchant
            {
                Name = "Chadwicks",
                Url = "https://www.chadwicks.ie",
                ContactEmail = "support@chadwicks.ie",
                Prices = new List<Price>(),
                Loggings = new List<Logging>()
            };

            try
            {
                IWebScraper scraper = WebScraperFactory.CreateScraper("Chadwicks");
                scraper.RunFullScrape(merchant);
            }
            catch (Exception e)
            {
                _loggingService.AddLog(new Logging
                {
                    MerchantId = merchant.MerchantId,
                    ScrapedAt = DateTime.UtcNow,
                    Status = "Scraping initiated",
                    ErrorMessage = e.Message
                });
            }
        }


        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
