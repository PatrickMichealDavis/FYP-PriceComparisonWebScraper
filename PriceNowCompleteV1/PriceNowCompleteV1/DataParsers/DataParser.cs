using PriceNowCompleteV1.Models;

namespace PriceNowCompleteV1.DataParsers
{
    public class DataParser
    {

        public static Product SanitizeProduct(Product product)
        {
            var productNameAndUnit = SplitProductNameAndUnit(product.Name);
            product.Name = productNameAndUnit.Item1;
            product.Unit = productNameAndUnit.Item2;

            return product;
        }

        private static Tuple<string, string> SplitProductNameAndUnit(string fullName)
        {
            var splitNameAndUnit = fullName.Split(" ");

            List<string> productNameParts = new List<string>();
            List<string> productUnitParts = new List<string>();

            foreach (var part in splitNameAndUnit)
            {
                if (IsUnitPart(part))
                {
                    productUnitParts.Add(part);
                }
                else
                {
                    productNameParts.Add(part);
                }
            }
            string productName = string.Join(" ", productNameParts);
            string unit = string.Join(" ", productUnitParts);

            return new Tuple<string, string>(productName, unit);

            
        }

        private static bool IsUnitPart(string part)
        {
            return part.Any(char.IsDigit) || part.Contains("x", StringComparison.OrdinalIgnoreCase) ||
                  part.EndsWith("m", StringComparison.OrdinalIgnoreCase) ||
                  part.EndsWith("mm", StringComparison.OrdinalIgnoreCase) ||
                  part.EndsWith("ft", StringComparison.OrdinalIgnoreCase);
        }
    }
}
