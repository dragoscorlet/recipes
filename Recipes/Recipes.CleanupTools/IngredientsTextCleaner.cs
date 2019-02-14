using Recipes.DAL.Schema;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Recipes.CleanupTools
{
    public class IngredientsTextCleaner
    {
        private const string SPACE = " ";

        private IEnumerable<string> _stopWords;
        private IEnumerable<string> _brands;
        private INoiseWordsRepository _stopWordsRepository;
        private IRecipeReadRepository _recipeRepository;

        public IngredientsTextCleaner(INoiseWordsRepository noiseRepository, IRecipeReadRepository recipeRepository)
        {
            _stopWordsRepository = noiseRepository;
            _recipeRepository = recipeRepository;
        }

        public string Clean(string text, string language, bool filterBrands = true)
        {
            if (_stopWords == null)
                _stopWords = _stopWordsRepository.GetWords(language);

            if (_brands == null)
                _brands = _recipeRepository.GetAllBrands();

            var cleanText = FilterStopWords(text);

            if (filterBrands)
            {
                cleanText = FilterBrands(cleanText);
            }

            return cleanText;
        }
        
        private string FilterBrands(string text)
        {
            var tokens = text.Split();
            var cleandTokens = tokens.Where(token => !_brands.Any(brand => token.Equals(brand)));

            return string.Join(SPACE, cleandTokens);
        }

        private string FilterStopWords(string text)
        {
            var tokens = text.Split();
            var cleanTokens = tokens.Where(token => !_stopWords.Any(stopWord => Regex.IsMatch(token, stopWord)));

            return string.Join(SPACE, cleanTokens);
        } 
    }
}
