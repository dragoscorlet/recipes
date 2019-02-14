using Recipes.Crawlers.Models;
using Recipes.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Importer
{
    public class RecipesImporter
    {
        private RecipeWriteRepository _repository;

        public RecipesImporter(RecipeWriteRepository repository)
        {
            _repository = repository;
        }

        public void ImportRecipes(IEnumerable<CrawledRecipe> crawledRecipes)
        {
            var recipes = crawledRecipes.Select(recipe => recipe.Map()).ToList();

           _repository.Create(recipes);
        }
    }
}
