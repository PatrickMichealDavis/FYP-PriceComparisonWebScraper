using HtmlAgilityPack;
using PriceNowCompleteV1.DataParsers;
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

                var products = new List<Product>();
                bool hasMoreProducts = true;
                var repeatedProductLinks = new HashSet<string>();

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
                        var productHref = productLink.GetAttributeValue("href", null);

                        if(productHref==null || repeatedProductLinks.Contains(productHref))
                        {
                            continue;
                        }

                        var productName = productLink.GetAttributeValue("data-name", null);
                        var priceText = productLink.GetAttributeValue("data-price", null);
                        var price = priceText != null ? Math.Round(decimal.Parse(priceText), 2) : 0;
                        var unit = "test unit";

                        if (productName != null && price != 0)
                        {
                            var product = new Product
                            {
                                Name = productName,
                                Description = productName,
                                Unit = unit,
                                Category = "timber",
                                Prices = new List<Price>
                                        {
                                            new Price
                                            {
                                                PriceValue = price,
                                                MerchantId = merchant.MerchantId,
                                                ScrapedAt = DateTime.UtcNow
                                            }
                                        }
                            };

                            var sanitizedProduct = DataParser.SanitizeProduct(product);
                            products.Add(sanitizedProduct);
                            repeatedProductLinks.Add(productHref);

                        }
                    }
                    


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
                var distinctProducts = products.Distinct().ToList();
                await _productService.AddMultipleProducts(products);
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
            Console.WriteLine("Running partial scrape for CorkBp");
            throw new NotImplementedException();
        }
    }
}
