namespace PriceNowCompleteV1.DTOs
{
    public class PriceDTO
    {
        public int PriceId { get; set; }
        public int ProductId { get; set; }
        public int MerchantId { get; set; }
        public decimal PriceValue { get; set; }
        public DateTime ScrapedAt { get; set; }
        public string ProductUrl { get; set; }

        public MerchantDTO Merchant { get; set; }
    }
}
