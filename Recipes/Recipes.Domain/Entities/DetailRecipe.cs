using System.Collections.Generic;
using System.Linq;

namespace Recipes.Domain.Entities
{
    public class DetailRecipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int PreparationTimeMinutes { get; set; }

        public int Servings { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Images { get; set; }

        public IEnumerable<RecipeIngredientInfo> Ingredients { get; set; }

        public string IngredientIds
        {
            get { return string.Join(",", Ingredients.Select(ing => ing.Ingredient.Id)); }
        }
    }
}
