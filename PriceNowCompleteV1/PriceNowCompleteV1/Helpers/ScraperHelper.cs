using HtmlAgilityPack;
using PriceNowCompleteV1.DataParsers;
using PriceNowCompleteV1.Models;
using PuppeteerSharp;

namespace PriceNowCompleteV1.Helpers
{
    public static class ScraperHelper
    {
        public static async Task<string> GetHrefLink(IPage page, string keyword)
        {
            return await page.EvaluateFunctionAsync<string>(
                    @"(keyword) => {
                        const links = Array.from(document.querySelectorAll('a'));
                        const matchedLink = links.find(link => link.href.includes(keyword));
                        return matchedLink ? matchedLink.href : null;
                    }",
                    keyword
                );
        }

        public  static async Task<List<Product>> ScrapePage(IPage page, Merchant merchant,string category)
        {
            var products = new List<Product>();
            bool hasMoreProducts = true;
            var repeatedProductLinks = new HashSet<string>();
            var scrapedProductsRaw = new List<Product>();

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

                    if (productHref == null || repeatedProductLinks.Contains(productHref))
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
                            Category = category,
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
            return products.Distinct().ToList();
        }

    }
}
