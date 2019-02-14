using F23.StringSimilarity;

namespace Recipes.CleanupTools
{
    public class IngredientsSimilarityComputer
    {
        private JaroWinkler _jaroWinkler;
        private IngredientsTextCleaner _cleaner;
        private string _language;

        public IngredientsSimilarityComputer(IngredientsTextCleaner cleaner, string language)
        {
            _cleaner = cleaner;
            _language = language;
            _jaroWinkler = new JaroWinkler();
        }

        public double ComputeSimilarity(string text1, string text2)
        {
            return 1.0 - _jaroWinkler.Distance(text1, text2);
        }


    }
}
