using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.Scrapers
{
    public abstract class BaseScraper : IWebScraper
    {
        public abstract Task RunFullSuite();

        public abstract Task RunFullSuitePartial();

        public abstract Task RunFullScrapeByMerchant(Merchant merchant);

        public abstract Task RunPartialScrapeByMerchant(Merchant merchant);
                
    }
    
}
