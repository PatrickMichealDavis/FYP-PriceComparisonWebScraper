namespace PriceNowCompleteV1.Helpers
{
    public static class KeyWordHelper
    {

        // This will be used for scalling as the product base grows to remove unwanted keywords at scale a database table would be used
        public static HashSet<string> KeyWordsForRemoval = new HashSet<string>() { "white","deal" };

        
        public static string RemoveKeyWords(string word)
        {
            var words = word.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var filtered = words.Where(w => !KeyWordsForRemoval.Contains(w));
            return string.Join(" ", filtered);
        }

    }
}
