using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using PriceNowCompleteV1.DataParsers;
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
                    Headless = true
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
                    return;
                }

                var timberDeckingLink = await page.EvaluateFunctionAsync<string>(
                    @"() => {
                    const links = Array.from(document.querySelectorAll('a'));
                    const deckingLink = links.find(link => link.innerText.includes('Timber Decking'));
                    return deckingLink ? deckingLink.href : null;
                }"
                );

                if (timberDeckingLink != null)
                {
                    Console.WriteLine("Navigating to Timber Decking link...");
                    await page.GoToAsync(timberDeckingLink, new NavigationOptions
                    {
                        Timeout = 60000
                    });


                    await page.WaitForSelectorAsync("a", new WaitForSelectorOptions
                    {
                        Timeout = 60000
                    });
                    Console.WriteLine("Clicked on 'Timber Decking' link.");
                }
                else
                {
                    Console.WriteLine("'Timber Decking' link not found.");
                    await browser.CloseAsync();
                    return;
                }


                var products = new List<Product>();
                bool hasMoreProducts = true;

                while (hasMoreProducts)
                {
                    var htmlContent = await page.GetContentAsync();

                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlContent);

                    var productLinks = htmlDoc.DocumentNode.Descendants("a")
                        .Where(x => x.GetAttributeValue("class", "") == "product-item-link")
                        .ToList();

                    foreach (var productLink in productLinks)
                    {
                        var name = productLink.GetAttributeValue("data-name",null);
                        var priceText = productLink.GetAttributeValue("data-price", null);
                        var price = priceText != null ? decimal.Parse(priceText) : 0;
                        var unit = "test unit";

                        if (name != null && price != 0)//this is temporary
                        {
                            var product = new Product
                            {
                                Name = name,
                                Description = name,
                                Unit = unit,
                                Category = "timber decking",
                                Prices = new List<Price>
                                        {
                                            new Price
                                            {
                                                PriceValue = price,
                                                Merchant = merchant,
                                                ScrapedAt = DateTime.UtcNow
                                            }
                                        }
                            };
                            var sanitizedProduct =DataParser.SanitizeProduct(product);
                            products.Add(sanitizedProduct);

                        }
                    }
                    var distinctProducts = products.Distinct().ToList();
                    //await _productService.AddMultipleProducts(products);// added new chain here!!!!


                    var loadNextButton = await page.QuerySelectorAsync("span[x-text='loadingafterTextButton']");
                    if (loadNextButton != null)
                    {
                        Console.WriteLine("Loading next products...");
                        await loadNextButton.ClickAsync();
                        await page.WaitForSelectorAsync("a", new WaitForSelectorOptions
                        {
                            Timeout = 60000
                        });
                    }
                    else
                    {
                        hasMoreProducts = false;
                    }
                }
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
            throw new NotImplementedException();
        }
    }
}
