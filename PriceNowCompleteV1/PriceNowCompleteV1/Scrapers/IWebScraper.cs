using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Scrapers
{
    public interface IWebScraper
    {
        Task RunFullSuite();
        Task RunFullSuitePartial();
        Task RunFullScrapeByMerchant(Merchant merchant);
        Task RunPartialScrapeByMerchant(Merchant merchant);
        

    }
}
