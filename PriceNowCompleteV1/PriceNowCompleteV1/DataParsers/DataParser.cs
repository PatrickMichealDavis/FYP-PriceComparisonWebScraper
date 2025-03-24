using PriceNowCompleteV1.Models;
using FuzzySharp;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Text.Json;

namespace PriceNowCompleteV1.DataParsers
{
    public class DataParser
    {
        public static Product SanitizeProduct(Product product)
        {
            var productNameAndUnit = SplitProductNameAndUnit(product.Name);
            product.Name = productNameAndUnit.Item1.ToLower().Replace(")","").Replace("(","").Replace(".","").Trim();
            product.Unit = productNameAndUnit.Item2.ToLower().Trim();
            product.Description = product.Description.ToLower().Trim();//may not need this unless used in comparisson

            //need to decide about cleaning description

            return product;
        }

        private static Tuple<string, string> SplitProductNameAndUnit(string fullName)
        {
            fullName = Clean(fullName);
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
            unit = Regex.Replace(unit, @"\b[a-zA-Z]+\d+\b", "").Trim();

            return new Tuple<string, string>(productName, unit);
        }

        private static bool IsUnitPart(string part)
        {
            return part.Any(char.IsDigit) || part.Contains("x", StringComparison.OrdinalIgnoreCase) ||
                  part.EndsWith("m", StringComparison.OrdinalIgnoreCase) ||
                  part.EndsWith("mm", StringComparison.OrdinalIgnoreCase) ||
                  part.EndsWith("ft", StringComparison.OrdinalIgnoreCase);
        }

        private static string Clean(string word)
        {
            word = Regex.Replace(word, @"\s*\(.*?\)\s*", " ").Replace("&amp;","").Replace("+","");
            word = Regex.Replace(word, @"\s*-\s*", " ");
            word.Replace(";", "");
            return word;
        }

        public static bool CheckForCloseComparrison(Product newProduct, Product existingProduct)
        {
            int nameRatio = Fuzz.Ratio(newProduct.Name.ToLower(), existingProduct.Name.ToLower());
            int nameSortedRatio = Fuzz.TokenSortRatio(newProduct.Name.ToLower(), existingProduct.Name.ToLower());

            int unitRatio = Fuzz.Ratio(newProduct.Unit.ToLower(), existingProduct.Unit.ToLower());
            int unitSortedRatio = Fuzz.TokenSortRatio(newProduct.Unit.ToLower(), existingProduct.Unit.ToLower());

            int finalNameScore = (nameRatio + nameSortedRatio) / 2;
            int finalUnitScore = (unitRatio + unitSortedRatio) / 2;

            int finalScore = (finalNameScore + finalUnitScore) / 2;
           // Console.WriteLine($"Final Similarity Score: {finalScore}%");
                       

            if (finalNameScore >= 80 && finalUnitScore > 97)
            {
                if (existingProduct.Name.Contains("treated", StringComparison.OrdinalIgnoreCase) && !newProduct.Name.Contains("treated", StringComparison.OrdinalIgnoreCase) 
                    || !existingProduct.Name.Contains("treated", StringComparison.OrdinalIgnoreCase) && newProduct.Name.Contains("treated", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                Console.WriteLine($"Product scraped: {newProduct.Name} Product repo: {existingProduct.Name} ");
                return true;
            }

            return false;
        }
        
        public static bool CheckForCloseComparrisonUnit(string newProduct, string existingProduct)
        {
            int unitRatio = Fuzz.Ratio(newProduct, existingProduct);
            int unitSortedRatio = Fuzz.TokenSortRatio(newProduct, existingProduct);

            int finalUnitScore = (unitRatio + unitSortedRatio) / 2;

            int testOfRefactor = GetRatioScore(newProduct, existingProduct);//if this works change later when testing should work

            if (finalUnitScore >= 80)
            {
                return true;
            }
           
            return false;
        }

        private static int GetRatioScore(string word1, string word2)
        {
            int ratio = Fuzz.Ratio(word1, word2);
            int tokenSortRatio = Fuzz.TokenSortRatio(word1, word2);

            return (ratio + tokenSortRatio) / 2;
        }

        public static string Standardize(string word)
        {
            var words = word.Replace("by", "x", StringComparison.OrdinalIgnoreCase)
                            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                            .Select(w => w.ToLower()).OrderBy(w => w);

            word = string.Join(" ", words);

            return word;
        }

        public static string FindClosestKey(string unit, List<string> keys)
        {
            var closestKey = "";
            int highestScore = 0;

            foreach (var key in keys)
            {
                int score = Fuzz.TokenSortRatio(key, unit);

                if (score > highestScore)
                {
                    highestScore = score;
                    closestKey = key;
                }
            }

            return closestKey;
        }

        //public static async Task<List<Product>> GetJsonProducts(string filepath)
        //{
        //    if (!File.Exists(filepath))
        //    {
        //        Console.WriteLine("Product file not found!");
        //        return new List<Product>();
        //    }

        //    string json = await File.ReadAllTextAsync(filepath);
        //    return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        //}
    }
}
