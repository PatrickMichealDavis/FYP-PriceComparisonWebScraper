using HtmlAgilityPack;
using PriceNowCompleteV1.Interfaces;
using PriceNowCompleteV1.Models;
using PuppeteerSharp;

namespace PriceNowCompleteV1.Scrapers
{
    public class GenericScraper : IGenericWebScraper
    {
        public GenericScraper()
        {
        }

        public async Task<decimal> PriceNow(string productUrl)
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

                await page.GoToAsync(productUrl, new NavigationOptions
                {
                    Timeout = 60000 //60Secs
                });

                var htmlContent = await page.GetContentAsync();

                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(htmlContent);

                if (productUrl.Contains("tjomahony",StringComparison.OrdinalIgnoreCase))
                {
                    var firstPrice = await page.EvaluateExpressionAsync<string>(
                          @"document.querySelectorAll('span.price')[0].innerText");

                    if (!string.IsNullOrWhiteSpace(firstPrice))
                    {
                        firstPrice = firstPrice.Replace("€", "").Trim();

                        if (decimal.TryParse(firstPrice, out var result))
                            return result;
                    }
                }
                var price = await page.EvaluateExpressionAsync<string>(
                           @"document.querySelectorAll('span.price')[1].innerText");

                if (!string.IsNullOrWhiteSpace(price))
                {
                    price = price.Replace("€", "").Trim();

                    if (decimal.TryParse(price, out var result))
                        return result;
                }

            }
            catch (Exception ex)
            {
                var Logging = new Logging
                {
                   // MerchantId = merchant.MerchantId,
                    ScrapedAt = DateTime.UtcNow,
                    Status = "failed",
                    ErrorMessage =$"Price now scraper failed at url: {productUrl}" + ex.Message,
                };
            }
            finally
            {
                if (browser != null)
                {
                    await browser.CloseAsync();
                }
            }
            return 0;
        }
    }
}
