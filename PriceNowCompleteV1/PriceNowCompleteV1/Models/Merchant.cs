using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PriceNowCompleteV1.Models
{
    public class Merchant
    {
        [Key]
        public int MerchantId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ContactEmail { get; set; }

        [JsonIgnore]//may be wrong
        public ICollection<Price> Prices { get; set; }

        [JsonIgnore]//maybe wrong
        public ICollection<Logging> Loggings { get; set; }

        public Merchant()
        {
        }

        public Merchant(int merchantId, string name, string url, string contactEmail, ICollection<Price> prices, ICollection<Logging> loggings)
        {
            MerchantId = merchantId;
            Name = name;
            Url = url;
            ContactEmail = contactEmail;
            Prices = prices;
            Loggings = loggings;
        }
    }
}
