namespace PriceNowCompleteV1.Scrapers
{
    public class WebScraperFactory
    {
        public static IWebScraper CreateScraper(string scarperType)
        {
            switch (scarperType)
            {
                case "Chadwicks":
                    return new ChadwicksScraper();
                case "CorkBpScraper":
                    return new CorkBpScraper();
                case "TJOMahonyScraper":
                    return new TJOMahonyScraper();
                default:
                    throw new ArgumentException("Invalid scraper type", nameof(scarperType));
            }
        }
    }
}
