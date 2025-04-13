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

                var price = await page.EvaluateExpressionAsync<string>(
                            @"document.querySelectorAll('span.price')[1].innerText");

                if (!string.IsNullOrWhiteSpace(price))
                {
                    price = price.Replace("€", "").Trim();

                    if (decimal.TryParse(price, out var result))
                        return result;
                }

            }
            catch
            {

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
