namespace PriceNowCompleteV1.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PriceDTO> Prices { get; set; }
    }
}
