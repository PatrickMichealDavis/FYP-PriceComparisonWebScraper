using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PriceNowCompleteV1.DataParsers;
using PriceNowCompleteV1.Helpers;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;
using PuppeteerSharp;

namespace PriceNowCompleteV1.Scrapers
{
    public class ChadwicksScraper : BaseScraper
    {
        string chadwicksUrl = "https://www.chadwicks.ie";//change to merchant url after testing is done
        private readonly IProductService _productService;
        private readonly ILoggingService _loggingService;
        

        public ChadwicksScraper(IProductService productService, ILoggingService loggingService)
        {
            _productService = productService;
            _loggingService = loggingService;
           
        }

        public override async Task RunFullScrapeByMerchant(Merchant merchant)
        {
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            IBrowser browser = null;

           
            try
            {
                browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = false
                });

                var page = await browser.NewPageAsync();

                await page.GoToAsync(chadwicksUrl, new NavigationOptions
                {
                    Timeout = 60000 //60Secs
                });

                await page.WaitForSelectorAsync("a", new WaitForSelectorOptions
                {
                    Timeout = 60000
                });

                await Task.Delay(9000);//modal time changed to 9 secs this may need changing


                var closeButton = await page.QuerySelectorAsync("#lpclose");//close modal if exists

                if (closeButton != null)
                {
                    Console.WriteLine("Close button found, clicking...");
                    await closeButton.ClickAsync();
                }
                else
                {
                    Console.WriteLine("Close button not found, skipping...");
                }

                var timberLink = await page.EvaluateFunctionAsync<string>(
                   @"() => {
                    const links = Array.from(document.querySelectorAll('a'));
                    const timberLink = links.find(link => link.innerText.includes('Timber'));
                    return timberLink ? timberLink.href : null;
                }"
                );

                if (timberLink != null)
                {
                    Console.WriteLine("Navigating to Timber link...");
                    await page.GoToAsync(timberLink, new NavigationOptions
                    {
                        Timeout = 60000
                    });


                    await page.WaitForSelectorAsync("a", new WaitForSelectorOptions
                    {
                        Timeout = 60000
                    });
                    Console.WriteLine("Clicked on 'Timber' link.");
                }
                else
                {
                    Console.WriteLine("'Timber' link not found.");
                    await browser.CloseAsync();
                    return;
                }

                //var deckingPanels = await page.EvaluateFunctionAsync<string>(
                //    @"() => {
                //    const links = Array.from(document.querySelectorAll('a'));
                //    const deckingLink = links.find(link => link.innerText.includes('Decking & Panels'));
                //    return deckingLink ? deckingLink.href : null;
                //}"
                //);

                //if (deckingPanels != null)
                //{
                //    Console.WriteLine("Navigating to decking link...");
                //    await page.GoToAsync(deckingPanels, new NavigationOptions
                //    {
                //        Timeout = 60000
                //    });

                //    await page.WaitForSelectorAsync("a", new WaitForSelectorOptions
                //    {
                //        Timeout = 60000
                //    });
                //    Console.WriteLine("Clicked on ' Decking' link.");
                //}
                //else
                //{
                //    Console.WriteLine("' Decking' link not found.");
                //    await browser.CloseAsync();
                //    return;
                //}

                //var timberDeckingLink = await page.EvaluateFunctionAsync<string>(
                //    @"() => {
                //    const links = Array.from(document.querySelectorAll('a'));
                //    const deckingLink = links.find(link => link.innerText.includes('Timber Decking'));
                //    return deckingLink ? deckingLink.href : null;
                //}"
                //);

                //if (timberDeckingLink != null)
                //{
                //    Console.WriteLine("Navigating to Timber Decking link...");
                //    await page.GoToAsync(timberDeckingLink, new NavigationOptions
                //    {
                //        Timeout = 60000
                //    });


                //    await page.WaitForSelectorAsync("a", new WaitForSelectorOptions
                //    {
                //        Timeout = 60000
                //    });
                //    Console.WriteLine("Clicked on 'Timber Decking' link.");
                //}
                //else
                //{
                //    Console.WriteLine("'Timber Decking' link not found.");
                //    await browser.CloseAsync();
                //    return;
                //}

                var roughTimber = await page.EvaluateFunctionAsync<string>(
                   @"() => {
                    const links = Array.from(document.querySelectorAll('a'));
                    const roughTimberLink = links.find(link => link.href.includes('rough-timber'));
                    return roughTimberLink ? roughTimberLink.href : null;
                }"
               );

                if (roughTimber != null)
                {
                    Console.WriteLine("Navigating to roughTimber link...");
                    await page.GoToAsync(roughTimber, new NavigationOptions
                    {
                        Timeout = 60000
                    });

                    await page.WaitForSelectorAsync("a", new WaitForSelectorOptions
                    {
                        Timeout = 60000
                    });
                    Console.WriteLine("Clicked on 'roughTimber' link.");
                }
                else
                {
                    Console.WriteLine("'roughTimber' link not found.");
                    await browser.CloseAsync();
                    return;
                }


               var roughTimberProducts = await ScraperHelper.ScrapePage(page, merchant, "rough timber");

                string rawProductsFilePath = "chadwicksRawProducts.json";
                string rawTimberProductsFilePath = "chadwicksTimberRawProducts.json";
                string sanitizedProductsFilePath = "chadwicksSanitizedProducts.json";

                //var scrapedProducts = products.Distinct().ToList();

                //await _productService.SaveProductsToFile(rawTimberProductsFilePath, scrapedProductsRaw);//use this to grab raw rough timber
                //await _productService.SaveProductsToFile(rawProductsFilePath, scrapedProductsRaw);//use this to grab raw for testing sanitizer
                await _productService.SaveProductsToFile(sanitizedProductsFilePath, roughTimberProducts);//use this to grab the sanitized products

                await _productService.ProcessProductsV2(roughTimberProducts);

                await _loggingService.AddLog(new Logging
                {
                    MerchantId = merchant.MerchantId,
                    ScrapedAt = DateTime.UtcNow,
                    Status = "Success",
                    ErrorMessage = $"Scraped successfully for {merchant.Name}"
                });

            }
            catch (Exception ex)
            {
                await _loggingService.AddLog(new Logging
                {
                    MerchantId = merchant.MerchantId,
                    ScrapedAt = DateTime.UtcNow,
                    Status = "failed",
                    ErrorMessage = merchant.Name + " scraper failed: " + ex.Message
                });
            }
            finally
            {
                if (browser != null)
                {
                    await browser.CloseAsync();
                }
            }

            
        }

        public override Task RunFullSuite()
        {
            throw new NotImplementedException();
        }

        public override Task RunFullSuitePartial()
        {
            throw new NotImplementedException();
        }

        public override Task RunPartialScrapeByMerchant(Merchant merchant)
        {
            Console.WriteLine("Running partial scrape for Chadwicks");
            throw new NotImplementedException();
        }
    }
}
