namespace PriceNowCompleteV1.Models
{
    public class Price
    {
        public int PriceId { get; set; }
        public int ProductId { get; set; }
        public int MerchantId { get; set; }
        public decimal PriceValue { get; set; }
        public DateTime ScrapedAt { get; set; } = DateTime.UtcNow;

        public Product Product { get; set; }
        public Merchant Merchant { get; set; }
    }
}
