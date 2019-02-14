using Recipes.DAL;
using Recipes.DAL.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Recipes.Importer
{
    public class IngredientsImporter
    {
        private IngredientsWriteRepository _repo;

        public IngredientsImporter()
        {
            _repo = new IngredientsWriteRepository(ConfigurationManager.ConnectionStrings["Recipes"].ConnectionString);
        }

        public void ImportIngredients(IEnumerable<Ingredient> ingredients)
        {
            var cleanIngredientsNames = ingredients.Select(ing =>  new Ingredient { Name = ing.Name.GetCleanIngredientName(), IdLanguage = 1 }).Distinct();

            _repo.CreateIngredients(cleanIngredientsNames);
        }
    }
}
