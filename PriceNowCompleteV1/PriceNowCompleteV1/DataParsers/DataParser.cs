using PriceNowCompleteV1.Models;
using FuzzySharp;
using System.Text.RegularExpressions;

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
            fullName = Regex.Replace(fullName, @"\s*\(.*?\)\s*", " ").Replace("-",""); //use this to clean already dirty db if you dont wipe
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

        public static bool CheckForCloseComparrison(Product newProduct, Product existingProduct)
        {
           
            int ratio = Fuzz.Ratio(newProduct.Description.ToLower(), existingProduct.Description.ToLower());
            int sortedRatio = Fuzz.TokenSortRatio(newProduct.Description.ToLower(), existingProduct.Description.ToLower());

            int finalScore = (ratio + sortedRatio) / 2;
            Console.WriteLine($"Final Similarity Score: {finalScore}%");

            if (finalScore >= 90)
            {
                //newProduct.Description = existingProduct.Description;
                return true;
            }
           
            return false;
        }

        public static string StandardizeDescription(string description)
        {
            description = Regex.Replace(description, @"\s*\(.*?\)\s*", " ");
            var words = description
                .Replace("by", "x", StringComparison.OrdinalIgnoreCase).Replace("-","")
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
