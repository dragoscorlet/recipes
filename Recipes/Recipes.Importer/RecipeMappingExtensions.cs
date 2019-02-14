using Recipes.CleanupTools;
using Recipes.Crawlers.Models;
using Recipes.DAL;
using Recipes.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;

namespace Recipes.Importer
{
    public static class RecipeMappingExtensions
    {
        private static IEnumerable<Ingredient> _ingredients;

        private static void LoadIngredients()
        {   
            if(_ingredients == null || _ingredients.Count() == 0)
            {
                var connString = ConfigurationManager.ConnectionStrings["Recipes"].ConnectionString;

                var ingredientsRepo = new IngredientsReadRepositoryOld(connString);
                _ingredients = ingredientsRepo.GetAllIngredienst();
            }
        }

        private static Ingredient GetMappedIngredient(string text)
        {
            LoadIngredients();
            var connString = ConfigurationManager.ConnectionStrings["Recipes"].ConnectionString;
            var repo = new NoiseWordsRepository(connString);
            var reciperepo = new RecipeReadRepositoryOld(connString);
            var cleaner = new IngredientsTextCleaner(repo, reciperepo);
            var similarityComputer = new IngredientsSimilarityComputer(cleaner, "RO");

            var cleaned = cleaner.Clean(text.ToLowerInvariant(), "RO");
            cleaned = cleaned.GetCleanIngredientName();

           var ing =  _ingredients.Select(i => Tuple.Create<Ingredient, double>(i, similarityComputer.ComputeSimilarity(cleaned, i.Name)))
                .OrderByDescending(t => t.Item2)
                .FirstOrDefault();

            if (ing == null)
                return null;

            return ing.Item1;
        }

        public static Recipe Map(this CrawledRecipe crawledRecipe)
        {
            return new Recipe
            {
                Name = crawledRecipe.Name,
                Url = crawledRecipe.Url,
                PreparationTimeMinutes = crawledRecipe.PreparationTimeMinutes,
                Servings = crawledRecipe.Servings,
                Description = CleanDescription(crawledRecipe.Description),
                Images = crawledRecipe.ImageUrls,
                Ingredients = MapIngredients(crawledRecipe.Ingredients).ToList()
            };
        }

        private static IEnumerable<RecipeIngredient> MapIngredients(IEnumerable<string> ingredients)
        {
            return ingredients.Select(ing => {

                var dbIngredient = GetIngredientByName(ing);

                if (dbIngredient == null)
                    return null;

                return new RecipeIngredient
                {
                    Ingredient = dbIngredient,
                    QuantityInGrams = GetQuantityIngrams(ing),
                    VolumeInMl = GetVolumeInMl(ing)
                };
            }).Where(ing => ing != null);
        }

        private static int? GetVolumeInMl(string ing)
        {
            var match = Regex.Match(ing, @"(?<capture>\d+) ml ");

            if (match == null || match.Groups == null || string.IsNullOrEmpty(match.Groups["capture"].Value))
                return 0;

            return int.Parse(match.Groups["capture"].Value);
        }

        private static int? GetQuantityIngrams(string ing)
        {
            var match = Regex.Match(ing, @"(?<capture>\d+) g ");

            if (match == null || match.Groups == null || string.IsNullOrEmpty(match.Groups["capture"].Value))
                return 0;

            return int.Parse(match.Groups["capture"].Value);
        }

        private static Ingredient GetIngredientByName(string ing)
        {
            return GetMappedIngredient(ing);
        }

        private static string CleanDescription(string description)
        {
            description = Regex.Replace(description, @"[\s\S]+?Mod De Preparare[\s\S]+?\. ", string.Empty);
            description = description.Replace(@"\n ", string.Empty);
            description = description.Replace(@"\\n\d+ ", string.Empty);

            return description;
            
        }
    }
}
