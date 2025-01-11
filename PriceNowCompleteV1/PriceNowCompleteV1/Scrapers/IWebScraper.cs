using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Scrapers
{
    public interface IWebScraper
    {
        Task RunFullSuite();
        Task RunFullSuitePartial();
        Task RunFullScrape(Merchant merchant);
        Task RunPartialScrape(Merchant merchant);

    }
}
