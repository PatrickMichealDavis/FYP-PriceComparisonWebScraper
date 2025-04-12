using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PriceNowCompleteV1.Models
{
    public class Price
    {
        [Key]
        public int PriceId { get; set; }
        public int ProductId { get; set; }
        public int MerchantId { get; set; }
        public decimal PriceValue { get; set; }
        public DateTime ScrapedAt { get; set; } = DateTime.UtcNow;
        public string ProductUrl { get; set; }

        [JsonIgnore]
        public Product Product { get; set; }
       
        public Merchant Merchant { get; set; }

        public Price()
        {
        }

        public Price(int priceId, int productId, int merchantId, decimal priceValue, DateTime scrapedAt, string productUrl, Product product, Merchant merchant)
        {
            PriceId = priceId;
            ProductId = productId;
            MerchantId = merchantId;
            PriceValue = priceValue;
            ScrapedAt = scrapedAt;
            ProductUrl = productUrl;
            Product = product;
            Merchant = merchant;
        }
    }
}
