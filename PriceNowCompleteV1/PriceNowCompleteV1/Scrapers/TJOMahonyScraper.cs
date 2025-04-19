using HtmlAgilityPack;
using Microsoft.CodeAnalysis.Options;
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
                    Headless = true,
                    //Args = new[]
                    //{
                    //    "--window-size=1920,1080" // Set browser window size for testing in headless cooment out as needed
                    //}
                });

                var page = await browser.NewPageAsync();

                //await page.SetViewportAsync(new ViewPortOptions
                //{
                //    Width = 1920,
                //    Height = 1080
                //});

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
                var scrapedProductsRaw = new List<Product>();

                var htmlContent = await page.GetContentAsync();

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                var productItems = htmlDoc.DocumentNode.Descendants("li")
                    .Where(x => x.GetAttributeValue("class", "").Contains("product-item"))
                    .ToList();

                var productLinks = htmlDoc.DocumentNode.Descendants("a")
                .Where(x => x.GetAttributeValue("class", "") == "marker").Select(x => x.GetAttributeValue("href", ""))
                .ToList();

                var repeatedProductLinks = new HashSet<string>();

                foreach (var productLink in productLinks)
                {

                    if (productLink == null || repeatedProductLinks.Contains(productLink))
                    {
                        Console.WriteLine("repeated Link");
                        continue;
                    }
                    await page.GoToAsync(productLink);

                    var allSwatchOptions = await page.QuerySelectorAllAsync("div.swatch-option.text");
                    var lengthLabels = new List<string>();

                    foreach (var swatch in allSwatchOptions)
                    {
                        var label = await swatch.EvaluateFunctionAsync<string>(@"
                                el => el.getAttribute('data-option-label')
                                      || el.getAttribute('aria-label')
                                      || el.textContent.trim()
                        ") ?? "Unknown label";

                        lengthLabels.Add(label);
                    }

                    Console.WriteLine($"found {lengthLabels.Count} swatch labels.");
                    var category = "rough timber";

                    if (lengthLabels.Count() == 1 || lengthLabels.Count()==0)
                    {

                        await page.WaitForSelectorAsync("h1.page-title", new WaitForSelectorOptions
                        {
                            Timeout = 10000,
                            Visible = true
                        });

                        var outOfStock = await page.QuerySelectorAsync("p[style='color:#e6322e;']");
                        if (outOfStock != null)
                        {
                            Console.WriteLine($" Out of stock.");
                            continue;
                        }

                        var singleproductName = await page.EvaluateFunctionAsync<string>(@"
                            () => {
                                const el = document.querySelector('span.base[data-ui-id=""page-title-wrapper""]');
                                return el ? el.textContent.trim() : '';
                        }");

                        var testPrice = await page.WaitForSelectorAsync(
                        "span.price",
                        new WaitForSelectorOptions { Timeout = 5000, Visible = true }
                    );

                        var newPageContent = await page.GetContentAsync();
                        htmlDoc.LoadHtml(newPageContent);

                        var canonicals = htmlDoc.DocumentNode
                           .SelectNodes("//link[@rel='canonical']");

                        var links = new List<string>();

                        foreach (var linkz in canonicals)
                        {
                            var href = linkz.GetAttributeValue("href", null);
                            if (!string.IsNullOrEmpty(href))
                            {
                                links.Add(href);
                            }
                        }

                       

                        var link = canonicals != null && canonicals.Count > 0
                                    ? canonicals[0].GetAttributeValue("href", null)
                                    : "errorLink";

                        string innerPriceText = await testPrice.EvaluateFunctionAsync<string>("el => el.innerText");
                        innerPriceText = innerPriceText.Replace("€", "").Trim();
                        var singlePrice = innerPriceText != null ? Math.Round(decimal.Parse(innerPriceText), 2) : 0;
                        var singleUnit = "test unit";
                        Console.WriteLine($"product:{singleproductName}: {singlePrice}");

                        if (singleproductName != null && singlePrice != 0)
                        {
                            var product = new Product
                            {
                                Name = singleproductName,
                                Description = singleproductName,
                                Unit = singleUnit,
                                Category = category,
                                Prices = new List<Price>
                                    {
                                        new Price
                                        {
                                             PriceValue = singlePrice,
                                             MerchantId = merchant.MerchantId,
                                             ScrapedAt = DateTime.UtcNow,
                                             ProductUrl = link.Trim()
                                        }
                                    }
                            };

                            //scrapedProductsRaw.Add(product); //turn off second 2 lines when turning on this
                            var sanitizedProduct = DataParser.SanitizeProduct(product);
                            products.Add(sanitizedProduct);
                            repeatedProductLinks.Add(productLink);
                        }
                    }
                    else
                    {
                        foreach (var label in lengthLabels)
                        {
                            var escapedLabel = label.Replace("\"", "\\\"");
                            var selector = $"div.swatch-option.text:not(.selected)[data-option-label=\"{escapedLabel}\"]";

                            var swatch = await page.QuerySelectorAsync(selector);
                            var originalUrl = page.Url;
                            if (swatch == null)
                            {
                                continue;
                            }

                            await swatch.EvaluateFunctionAsync("el => el.scrollIntoView({ block: 'center' })");

                            if (lengthLabels.Count() > 1)
                            {
                                await swatch.ClickAsync();
                            }
                            else
                            {
                                Console.WriteLine($"Swatch with label '{label}' not found.");
                                continue;
                            }

                            var currentUrl = page.Url;

                            if (originalUrl != currentUrl)
                            {
                                Console.WriteLine("Full page navigation detected.");
                            }

                            await page.WaitForSelectorAsync("h1.page-title", new WaitForSelectorOptions
                            {
                                Timeout = 10000,
                                Visible = true
                            });

                            var outOfStock = await page.QuerySelectorAsync("p[style='color:#e6322e;']");
                            if (outOfStock != null)
                            {
                                Console.WriteLine($" Out of stock.");
                                break;
                            }

                            var finalPageContent = await page.GetContentAsync();
                            htmlDoc.LoadHtml(finalPageContent);

                            var canonicalLinks = htmlDoc.DocumentNode
                                .SelectNodes("//link[@rel='canonical']");


                            var secondLink = canonicalLinks != null && canonicalLinks.Count > 1
                                ? canonicalLinks[1].GetAttributeValue("href", null)
                                : "errorLink";

                            Console.WriteLine($"Second canonical link: {secondLink}");

                            var productName = await page.EvaluateFunctionAsync<string>(@"
                            () => {
                                const el = document.querySelector('span.base[data-ui-id=""page-title-wrapper""]');
                                return el ? el.textContent.trim() : '';
                            }");

                            var newPrice = await page.WaitForSelectorAsync(
                                "span.price",
                                new WaitForSelectorOptions { Timeout = 5000, Visible = true }
                            );

                            if (newPrice != null)
                            {
                                string priceText = await newPrice.EvaluateFunctionAsync<string>("el => el.innerText");
                                priceText = priceText.Replace("€", "").Trim();
                                var price = priceText != null ? Math.Round(decimal.Parse(priceText), 2) : 0;
                                var unit = "test unit";
                                Console.WriteLine($"product:{productName}: {price}");

                                if (productName != null && price != 0)
                                {
                                    var product = new Product
                                    {
                                        Name = productName,
                                        Description = productName,
                                        Unit = unit,
                                        Category = category,
                                        Prices = new List<Price>
                                    {
                                        new Price
                                        {
                                             PriceValue = price,
                                             MerchantId = merchant.MerchantId,
                                             ScrapedAt = DateTime.UtcNow,
                                             ProductUrl = secondLink.Trim()
                                        }
                                    }
                                    };

                                    //scrapedProductsRaw.Add(product); //turn off second 2 lines when turning on this
                                    var sanitizedProduct = DataParser.SanitizeProduct(product);
                                    products.Add(sanitizedProduct);
                                    repeatedProductLinks.Add(productLink);

                                }
                            }
                            else
                            {
                                Console.WriteLine($"Could not find updated price for swatch '{label}'.");
                            }
                        }
                    }
                }
                    

                var distinctProducts = products.Distinct().ToList();
                string rawProductsFilePath = "tjomahonyRawProducts.json";
                string sanitizedProductsFilePath = "tjomahonySanitizedProducts.json";

                //await _productService.SaveProductsToFile(rawProductsFilePath, scrapedProductsRaw);
                await _productService.SaveProductsToFile(sanitizedProductsFilePath, distinctProducts);

                await _productService.ProcessProductsV2(distinctProducts);

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
                    ErrorMessage = merchant.Name + " scraper failed" + ex.Message,
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

        public async override Task RunPartialScrapeByMerchant(Merchant merchant)
        {
            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            IBrowser browser = null;

            try
            {
                browser = await Puppeteer.LaunchAsync(new LaunchOptions
                {
                    Headless = true,
                    //Args = new[]
                    //{
                    //    "--window-size=1920,1080" // Set browser window size for testing in headless cooment out as needed
                    //}
                });

                var page = await browser.NewPageAsync();

                //await page.SetViewportAsync(new ViewPortOptions
                //{
                //    Width = 1920,
                //    Height = 1080
                //});

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
                var scrapedProductsRaw = new List<Product>();

                var htmlContent = await page.GetContentAsync();

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                var productItems = htmlDoc.DocumentNode.Descendants("li")
                    .Where(x => x.GetAttributeValue("class", "").Contains("product-item"))
                    .ToList();

                var productLinks = htmlDoc.DocumentNode.Descendants("a")
                .Where(x => x.GetAttributeValue("class", "") == "marker").Select(x => x.GetAttributeValue("href", ""))
                .ToList();

                var repeatedProductLinks = new HashSet<string>();

                foreach (var productLink in productLinks)
                {

                    if (productLink == null || repeatedProductLinks.Contains(productLink))
                    {
                        Console.WriteLine("repeated Link");
                        continue;
                    }
                    await page.GoToAsync(productLink);

                    var allSwatchOptions = await page.QuerySelectorAllAsync("div.swatch-option.text");
                    var lengthLabels = new List<string>();

                    foreach (var swatch in allSwatchOptions)
                    {
                        var label = await swatch.EvaluateFunctionAsync<string>(@"
                                el => el.getAttribute('data-option-label')
                                      || el.getAttribute('aria-label')
                                      || el.textContent.trim()
                        ") ?? "Unknown label";

                        lengthLabels.Add(label);
                    }

                    Console.WriteLine($"found {lengthLabels.Count} swatch labels.");
                    var category = "rough timber";

                    if (lengthLabels.Count() == 1 || lengthLabels.Count() == 0)
                    {

                        await page.WaitForSelectorAsync("h1.page-title", new WaitForSelectorOptions
                        {
                            Timeout = 10000,
                            Visible = true
                        });

                        //await Task.Delay(5000);

                        var outOfStock = await page.QuerySelectorAsync("p[style='color:#e6322e;']");
                        if (outOfStock != null)
                        {
                            Console.WriteLine($" Out of stock.");
                            continue;
                        }

                        var singleproductName = await page.EvaluateFunctionAsync<string>(@"
                            () => {
                                const el = document.querySelector('span.base[data-ui-id=""page-title-wrapper""]');
                                return el ? el.textContent.trim() : '';
                        }");

                        var testPrice = await page.WaitForSelectorAsync(
                        "span.price",
                        new WaitForSelectorOptions { Timeout = 5000, Visible = true }
                    );
                        var newPageContent = await page.GetContentAsync();
                        htmlDoc.LoadHtml(newPageContent);

                        var canonicals = htmlDoc.DocumentNode
                           .SelectNodes("//link[@rel='canonical']");

                        var links = new List<string>();

                        foreach (var linkz in canonicals)
                        {
                            var href = linkz.GetAttributeValue("href", null);
                            if (!string.IsNullOrEmpty(href))
                            {
                                links.Add(href);
                            }
                        }

                        var link = canonicals != null && canonicals.Count > 0
                                   ? canonicals[0].GetAttributeValue("href", null)
                                   : "errorLink";///////////////!!!!changed here from line 454

                        string innerPriceText = await testPrice.EvaluateFunctionAsync<string>("el => el.innerText");
                        innerPriceText = innerPriceText.Replace("€", "").Trim();
                        var singlePrice = innerPriceText != null ? Math.Round(decimal.Parse(innerPriceText), 2) : 0;
                        var singleUnit = "test unit";
                        Console.WriteLine($"product:{singleproductName}: {singlePrice}");

                        if (singleproductName != null && singlePrice != 0)
                        {
                            var product = new Product
                            {
                                Name = singleproductName,
                                Description = singleproductName,
                                Unit = singleUnit,
                                Category = category,
                                Prices = new List<Price>
                                    {
                                        new Price
                                        {
                                             PriceValue = singlePrice,
                                             MerchantId = merchant.MerchantId,
                                             ScrapedAt = DateTime.UtcNow,
                                             ProductUrl = link.Trim()
                                        }
                                    }
                            };

                            //scrapedProductsRaw.Add(product); //turn off second 2 lines when turning on this
                            var sanitizedProduct = DataParser.SanitizeProduct(product);
                            products.Add(sanitizedProduct);
                            repeatedProductLinks.Add(productLink);
                        }
                    }
                    else
                    {
                        foreach (var label in lengthLabels)
                        {
                            var escapedLabel = label.Replace("\"", "\\\"");
                            var selector = $"div.swatch-option.text:not(.selected)[data-option-label=\"{escapedLabel}\"]";

                            var swatch = await page.QuerySelectorAsync(selector);
                           
                            if (swatch == null)
                            {
                                continue;
                            }

                            await swatch.EvaluateFunctionAsync("el => el.scrollIntoView({ block: 'center' })");

                            if (lengthLabels.Count() > 1)
                            {
                                await swatch.ClickAsync();
                            }
                            else
                            {
                                Console.WriteLine($"Swatch with label '{label}' not found.");
                                continue;
                            }

                            await page.WaitForSelectorAsync("h1.page-title", new WaitForSelectorOptions
                            {
                                Timeout = 10000,
                                Visible = true
                            });

                            var outOfStock = await page.QuerySelectorAsync("p[style='color:#e6322e;']");
                            if (outOfStock != null)
                            {
                                Console.WriteLine($" Out of stock.");
                                break;
                            }

                            var finalPageContent = await page.GetContentAsync();
                            htmlDoc.LoadHtml(finalPageContent);

                            var canonicalLinks = htmlDoc.DocumentNode
                                .SelectNodes("//link[@rel='canonical']");

                            var secondLink = canonicalLinks != null && canonicalLinks.Count > 1
                                ? canonicalLinks[1].GetAttributeValue("href", null)
                                : "errorLink";

                            Console.WriteLine($"Second canonical link: {secondLink}");

                            var productName = await page.EvaluateFunctionAsync<string>(@"
                            () => {
                                const el = document.querySelector('span.base[data-ui-id=""page-title-wrapper""]');
                                return el ? el.textContent.trim() : '';
                            }");

                            var newPrice = await page.WaitForSelectorAsync(
                                "span.price",
                                new WaitForSelectorOptions { Timeout = 5000, Visible = true }
                            );

                            if (newPrice != null)
                            {
                                string priceText = await newPrice.EvaluateFunctionAsync<string>("el => el.innerText");
                                priceText = priceText.Replace("€", "").Trim();
                                var price = priceText != null ? Math.Round(decimal.Parse(priceText), 2) : 0;
                                var unit = "test unit";
                                Console.WriteLine($"product:{productName}: {price}");

                                if (productName != null && price != 0)
                                {
                                    var product = new Product
                                    {
                                        Name = productName,
                                        Description = productName,
                                        Unit = unit,
                                        Category = category,
                                        Prices = new List<Price>
                                                {
                                                    new Price
                                                    {
                                                         PriceValue = price,
                                                         MerchantId = merchant.MerchantId,
                                                         ScrapedAt = DateTime.UtcNow,
                                                         ProductUrl = secondLink.Trim()
                                                    }
                                                }
                                    };

                                    //scrapedProductsRaw.Add(product); //turn off second 2 lines when turning on this
                                    var sanitizedProduct = DataParser.SanitizeProduct(product);
                                    products.Add(sanitizedProduct);
                                    repeatedProductLinks.Add(productLink);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Could not find updated price for swatch '{label}'.");
                            }
                        }
                    }
                }


                var distinctProducts = products.Distinct().ToList();
                string rawProductsFilePath = "tjomahonyRawProducts.json";
                string sanitizedProductsFilePath = "tjomahonySanitizedProducts.json";

                //await _productService.SaveProductsToFile(rawProductsFilePath, scrapedProductsRaw);
                await _productService.SaveProductsToFile(sanitizedProductsFilePath, distinctProducts);

                await _productService.ProcessProductsPartial(distinctProducts);

                await _loggingService.AddLog(new Logging
                {
                    MerchantId = merchant.MerchantId,
                    ScrapedAt = DateTime.UtcNow,
                    Status = "Success",
                    ErrorMessage = $"Partially scraped successfully for {merchant.Name}"
                });

            }
            catch (Exception ex)
            {
                var Logging = new Logging
                {
                    MerchantId = merchant.MerchantId,
                    ScrapedAt = DateTime.UtcNow,
                    Status = "failed",
                    ErrorMessage = merchant.Name + " scraper failed" + ex.Message,
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


    }

}
