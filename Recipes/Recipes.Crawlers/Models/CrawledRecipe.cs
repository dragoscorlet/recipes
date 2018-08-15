using System.Collections.Generic;

namespace Recipes.Crawlers.Models
{
    public class CrawledRecipe
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public int PreparationTimeMinutes { get; set; }

        public int Servings { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> ImageUrls { get; set; }

        public IEnumerable<string> Ingredients { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }
    }
}
