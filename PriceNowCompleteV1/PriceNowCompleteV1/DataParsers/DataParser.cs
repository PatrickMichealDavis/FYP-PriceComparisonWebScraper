using PriceNowCompleteV1.Models;
using FuzzySharp;

namespace PriceNowCompleteV1.DataParsers
{
    public class DataParser
    {
        public static Product SanitizeProduct(Product product)
        {
            var productNameAndUnit = SplitProductNameAndUnit(product.Name);
            product.Name = productNameAndUnit.Item1.ToLower();
            product.Unit = productNameAndUnit.Item2.ToLower();
            product.Description = StandardizeDescription(product.Description);

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

        public static Product UpdatePriceIfProductExists(Product newProduct, Product existingProduct)
        {
            if (existingProduct == null)//never will hit i dont think
            {
                return newProduct;
            }
            int ratio = Fuzz.Ratio(newProduct.Description.ToLower(), existingProduct.Description.ToLower());
            int sortedRatio = Fuzz.TokenSortRatio(newProduct.Description.ToLower(), existingProduct.Description.ToLower());

            int finalScore = (ratio + sortedRatio) / 2;
            Console.WriteLine($"Final Similarity Score: {finalScore}%");

            //check against book for test cases currently at 106 which failed with score 88 may need to increase further or add another verification
            if (finalScore >= 90)
            {
                existingProduct.Prices.Add(newProduct.Prices.First());
            }
            //else
            //{
            //    return newProduct;//this wont work as you are for eaching need to change to dictionary. 
            //}

            return existingProduct;
        }

        public static string StandardizeDescription(string description)
        {
            var words = description
                .Replace("by", "x", StringComparison.OrdinalIgnoreCase)
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(w => w.ToLower()).OrderBy(w => w);
            description = string.Join(" ", words);
            return description;
        }

        public static Product CleanProductDescription(Product product)
        {
            product.Description = StandardizeDescription(product.Description);
            return product;
        }
    }
}
