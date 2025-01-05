using System.ComponentModel.DataAnnotations;

namespace PriceNowCompleteV1.Models
{
    public class Logging
    {
        [Key]
        public int ScrapeId { get; set; }
        public int MerchantId { get; set; }
        public DateTime ScrapedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }
        public string ErrorMessage { get; set; }

        public Merchant Merchant { get; set; }

        public Logging()
        {
        }

        public Logging(int scrapeId, int merchantId, DateTime scrapedAt, string status, string errorMessage, Merchant merchant)
        {
            ScrapeId = scrapeId;
            MerchantId = merchantId;
            ScrapedAt = scrapedAt;
            Status = status;
            ErrorMessage = errorMessage;
            Merchant = merchant;
        }

    }
}
