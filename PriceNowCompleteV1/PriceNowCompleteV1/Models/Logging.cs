namespace PriceNowCompleteV1.Models
{
    public class Logging
    {
        public int ScrapeId { get; set; }
        public int MerchantId { get; set; }
        public DateTime ScrapedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }
        public string ErrorMessage { get; set; }

        public Merchant Merchant { get; set; }
    }
}
