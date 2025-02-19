using HtmlAgilityPack;
using PriceNowCompleteV1.DataParsers;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;
using PuppeteerSharp;

namespace PriceNowCompleteV1.Scrapers
{
    public class TJOMahonyScraper : BaseScraper
    {
        string tJOMahonyUrl = "https://tjomahony.ie";//change to merchant url after testing is done
        private readonly IProductService _productService;
        private readonly ILoggingService _loggingService;

        public TJOMahonyScraper(IProductService productService, ILoggingService loggingService)
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

                await page.GoToAsync(tJOMahonyUrl, new NavigationOptions
                {
                    Timeout = 60000 //60Secs
                });

                await page.WaitForSelectorAsync("#CybotCookiebotDialogBodyButtonDecline");

                await page.ClickAsync("#CybotCookiebotDialogBodyButtonDecline");

                await page.WaitForSelectorAsync("button.mc-closeModal", new WaitForSelectorOptions
                {
                    Visible = true
                    
                });

                await Task.Delay(3000);
                await page.ClickAsync("button.mc-closeModal");

                //await page.WaitForSelectorAsync("a", new WaitForSelectorOptions
                //{
                //    Timeout = 60000
                //});

                var timberLink = await page.EvaluateFunctionAsync<string>(
                   @"() => {
                        const links = Array.from(document.querySelectorAll('a'));
                        const timberLink = links.find(link => link.href.includes('timber-products.html'));
                        return timberLink ? timberLink.href : null;
                    }"
                );

                if (timberLink != null)
                {
                    Console.WriteLine("Navigating to Timber Products link...");
                    await page.GoToAsync(timberLink, new NavigationOptions
                    {
                        Timeout = 60000
                    });


                    await page.WaitForSelectorAsync("a", new WaitForSelectorOptions
                    {
                        Timeout = 60000
                    });
                    Console.WriteLine("Clicked on 'Timber Products' link.");
                }
                else
                {
                    Console.WriteLine("'Timber Products' link not found.");
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
                var scrapedProductsRaw = new List<Product>();


                while (hasMoreProducts)
                {
                    var htmlContent = await page.GetContentAsync();

                    var htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlContent);

                    var productItems = htmlDoc.DocumentNode.Descendants("li")
                        .Where(x => x.GetAttributeValue("class", "").Contains("product-item"))
                        .ToList();



                    foreach (var productItem in productItems)
                    {
                        var productName = productItem.Descendants("div")
                            .FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("product-name"))
                            ?.InnerText.Trim();

                        var priceWrapper = productItem.Descendants("span")
                            .FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("price-wrapper price-including-tax"));

                        var priceText = priceWrapper?.Descendants("span")
                            .FirstOrDefault(x => x.GetAttributeValue("class", "").Contains("price"))
                            ?.InnerText.Trim();

                        var priceData = priceWrapper?.GetAttributeValue("data-price-amount", "");

                        var price = priceData != null ? Math.Round(decimal.Parse(priceData),2) : 0;
                        var unit = "test unit";

                        if (productName != null && price != 0)
                        {
                            var product = new Product
                            {
                                Name = productName,
                                Description = productName,
                                Unit = unit,
                                Category = "rough timber",
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

                            //scrapedProductsRaw.Add(product); //turn off second 2 lines when turning on this
                            var sanitizedProduct = DataParser.SanitizeProduct(product);// remove and send to full standardize
                            products.Add(sanitizedProduct);

                        }
                    }
                   
                    var loadNextButton = await page.QuerySelectorAsync("span[x-text='loadingafterTextButton']");//dont need this code remove later
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

                string rawProductsFilePath = "tjomahonyRawProducts.json";
                string sanitizedProductsFilePath = "tjomahonySanitizedProducts.json";

                //await _productService.SaveProductsToFile(rawProductsFilePath, scrapedProductsRaw);
                await _productService.SaveProductsToFile(sanitizedProductsFilePath, distinctProducts);

                //await _productService.AddMultipleProducts(products);// added new chain here!!!!
            }
            catch (Exception ex)
            {
                var Logging = new Logging
                {
                    MerchantId = merchant.MerchantId,
                    ScrapedAt = DateTime.UtcNow,
                    Status = "failed",
                    ErrorMessage = merchant.Name +" scraper failed",
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
            Console.WriteLine("Running partial scrape for TJOMahony");
            throw new NotImplementedException();
        }

       
    }
   
}
