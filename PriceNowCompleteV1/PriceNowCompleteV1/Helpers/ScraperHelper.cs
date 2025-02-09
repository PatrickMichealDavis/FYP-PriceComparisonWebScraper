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
    }
}
