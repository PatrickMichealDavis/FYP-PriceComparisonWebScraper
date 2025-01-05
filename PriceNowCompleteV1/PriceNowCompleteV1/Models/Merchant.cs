using System.ComponentModel.DataAnnotations;

namespace PriceNowCompleteV1.Models
{
    public class Merchant
    {
        [Key]
        public int MerchantId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ContactEmail { get; set; }

        public ICollection<Price> Prices { get; set; }
        public ICollection<Logging> Loggings { get; set; }
    }
}
