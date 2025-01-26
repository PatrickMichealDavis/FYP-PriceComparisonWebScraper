namespace PriceNowCompleteV1.Scrapers
{
    public static class WebScraperFactory
    {

        public static IWebScraper CreateScraper(string scarperType)
        {
            switch (scarperType)
            {
                case "Chadwicks":
                    return new ChadwicksScraper();
                case "CorkBP":
                    return new CorkBpScraper();
                case "TJOMahony": //you changed here patrick
                    return new TJOMahonyScraper();
                default:
                    throw new ArgumentException("Invalid scraper type", nameof(scarperType));
            }
        }
    }
}
