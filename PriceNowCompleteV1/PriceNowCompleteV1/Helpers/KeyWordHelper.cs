namespace PriceNowCompleteV1.Helpers
{
    public static class KeyWordHelper
    {

        // This will we used for scalling as the product base grows to remove unwanted keywords
        public static HashSet<string> KeyWordsForRemoval = new HashSet<string>() { "white deal" };



        public static string CleanKeyWord(string keyword)
        {
            foreach(var key in KeyWordsForRemoval)
            {
                keyword = keyword.Replace(key, "");
            }

            return keyword;
        }

        public static bool ContainsKeyWord(string existing, string newWord)
        {
            if (existing.Contains(newWord) || newWord.Contains(existing))
            {
                return true;
            }

            return false;
        }


    }
}
