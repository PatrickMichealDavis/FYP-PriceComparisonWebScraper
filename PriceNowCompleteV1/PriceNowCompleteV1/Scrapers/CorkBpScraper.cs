﻿using HtmlAgilityPack;
using PriceNowCompleteV1.DataParsers;
using PriceNowCompleteV1.Helpers;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;
using PuppeteerSharp;

namespace PriceNowCompleteV1.Scrapers
{
    public class CorkBpScraper : BaseScraper
    {
        string corkBPUrl = "https://corkbp.ie";//change to merchant url after testing is done
        private readonly IProductService _productService;
        private readonly ILoggingService _loggingService;

        public CorkBpScraper(IProductService productService, ILoggingService loggingService)
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
                    Headless = false,
                    Args = new[]
                    {
                        "--window-size=1920,1080" // Set browser window size for testing in headless cooment out as needed
                    }
                });

                var page = await browser.NewPageAsync();

                await page.SetViewportAsync(new ViewPortOptions
                {
                    Width = 1920,
                    Height = 1080
                });

                await page.GoToAsync(corkBPUrl, new NavigationOptions
                {
                    Timeout = 60000 //60Secs
                });

                var testTimberLink = await ScraperHelper.GetHrefLink(page, "timber-1.html");//this works for refactor when ready 


                var timberLink = await page.EvaluateFunctionAsync<string>(
                   @"() => {
                        const links = Array.from(document.querySelectorAll('a'));
                        const timberLink = links.find(link => link.href.includes('timber-1.html'));
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

                await Task.Delay(10000);

                await ScraperHelper.DismissModal(page, "#lpclose", 3);

                var roughTimberProducts = await ScraperHelper.ScrapePage(page, merchant, "rough timber");

                string rawProductsFilePath = "corkbpRawProducts.json";
                string sanitizedProductsFilePath = "corkbpSanitizedProducts.json";


                // await _productService.SaveProductsToFile(deckingProductsFilePath, scrapedProductsRaw);
                //await _productService.SaveProductsToFile(rawProductsFilePath, scrapedProductsRaw);
                await _productService.SaveProductsToFile(sanitizedProductsFilePath, roughTimberProducts);

                await _productService.ProcessProductsV2(roughTimberProducts);

                //var deckingProducts = await ScrapeDecking(page, browser, merchant);

                string deckingProductsFilePath = "corkbpDeckingProducts.json";

                //var deckingProducts = await ScraperHelper.ScrapePage(page, merchant, "decking");
                //await _productService.SaveProductsToFile(deckingProductsFilePath, deckingProducts);

               // await _productService.ProcessProductsV2(deckingProducts);

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
                var Logging = new Logging
                {
                    MerchantId = merchant.MerchantId,
                    ScrapedAt = DateTime.UtcNow,
                    Status = "failed",
                    ErrorMessage = merchant.Name + " scraper failed",
                };

                await _loggingService.AddLog(Logging);
            }
            finally
            {
                if (browser != null)
                {
                    await browser.CloseAsync();
                }
            }
        }

        private async Task<List<Product>> ScrapeDecking(IPage page,IBrowser browser, Merchant merchant)
        {
            var deckingPanels = await page.EvaluateFunctionAsync<string>(
                        @"() => {
                    const links = Array.from(document.querySelectorAll('a'));
                    const deckingLink = links.find(link => link.innerText.includes('Decking & Panels'));
                    return deckingLink ? deckingLink.href : null;
                }"
                    );

            if (deckingPanels != null)
            {
                Console.WriteLine("Navigating to decking link...");
                await page.GoToAsync(deckingPanels, new NavigationOptions
                {
                    Timeout = 60000
                });

                await page.WaitForSelectorAsync("a", new WaitForSelectorOptions
                {
                    Timeout = 60000
                });
                Console.WriteLine("Clicked on ' Decking' link.");
            }
            else
            {
                Console.WriteLine("' Decking' link not found.");
                await browser.CloseAsync();
                return new List<Product>();
            }

            var timberDeckingLink = await page.EvaluateFunctionAsync<string>(
                @"() => {
                    const links = Array.from(document.querySelectorAll('a'));
                    const deckingLink = links.find(link => link.innerText.includes('decking-panels'));
                    return deckingLink ? deckingLink.href : null;
                }"
            );


            string deckingProductsFilePath = "corkbpDeckingProducts.json";

            var deckingProducts = await ScraperHelper.ScrapePage(page, merchant, "decking");

            return deckingProducts;
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
            Console.WriteLine("Running partial scrape for CorkBp");
            throw new NotImplementedException();
        }
    }
}
