using System.Collections.Generic;

namespace Recipes.DAL.Entities
{
    public class Recipe
    {   
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int PreparationTimeMinutes { get; set; }

        public int Servings { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Images { get; set; }

        public IEnumerable<RecipeIngredient> Ingredients { get; set; }
    }
}
