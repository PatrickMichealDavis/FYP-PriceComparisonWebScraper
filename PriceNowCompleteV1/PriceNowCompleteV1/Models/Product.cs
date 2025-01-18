using System.ComponentModel.DataAnnotations;

namespace PriceNowCompleteV1.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Price> Prices { get; set; }

        public Product()
        {
        }

        public Product(int productId, string name, string description, string category, string unit, DateTime createdAt, DateTime updatedAt, ICollection<Price> prices)
        {
            ProductId = productId;
            Name = name;
            Description = description;
            Category = category;
            Unit = unit;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Prices = prices;
        }

        public override bool Equals(object obj)
        {
            if (obj is Product other)
            {
                return Name == other.Name && Unit == other.Unit;
            }
            return false;
        }
    }
}
