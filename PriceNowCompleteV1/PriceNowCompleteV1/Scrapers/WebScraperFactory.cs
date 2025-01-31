using PriceNowCompleteV1.Interfaces;

namespace PriceNowCompleteV1.Scrapers
{
    public static class WebScraperFactory
    {
        public static IWebScraper CreateScraper(string scarperType, IProductService productService, ILoggingService loggingService)
        {
            switch (scarperType)
            {
                case "Chadwicks":
                    return new ChadwicksScraper(productService,loggingService);
                case "CorkBP":
                    return new CorkBpScraper(productService,loggingService);
                case "TJOMahony": 
                    return new TJOMahonyScraper(productService,loggingService);
                default:
                    throw new ArgumentException("Invalid scraper type", nameof(scarperType));
            }
        }
    }
}
