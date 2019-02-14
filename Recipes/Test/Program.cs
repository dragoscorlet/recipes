using Newtonsoft.Json;
using Recipes.CleanupTools;
using Recipes.Crawlers.Carrefour;
using Recipes.Crawlers.Models;
using Recipes.DAL;
using Recipes.DAL.Entities;
using Recipes.Importer;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Test
{
    class Program
    {   
        class Widget
        {
            public IEnumerable<int> Property;

            public void AddToProerty()
            {   
                Property = Enumerable.Range(0, 10).Where( a => a == 2);
            }
        }

        static void Main(string[] args)
        {
            var widget = new Widget();
            widget.AddToProerty();

            var serialized = JsonConvert.SerializeObject(widget);
        }

        private static void Map()
        {
            var connString = ConfigurationManager.ConnectionStrings["Recipes"].ConnectionString;
            var ingredientsRepo = new IngredientsReadRepositoryOld(connString);
            var reciperepo = new RecipeReadRepositoryOld(connString);

            var repo = new NoiseWordsRepository(connString);

            var cleaner = new IngredientsTextCleaner(repo, reciperepo);

            var vendorRepo = new VendorWriteRepository(connString);

            var vendorProducts = reciperepo.GetAllVendorProducts();
            var ingredients = ingredientsRepo.GetAllIngredienst();
            var mappings = new List<VendorProductIngredient>();
            var similarityComputer = new IngredientsSimilarityComputer(cleaner, "RO");

            var cleandProducts = vendorProducts.Select(p =>
            {
                p.Title = cleaner.Clean(p.Title.ToLowerInvariant(), "RO");
                return p;
            }).Select( p => new Ingredient { Name = p.Title, IdLanguage = 1 }).ToList();

            var cleanedIngredients = ingredients.Select(i =>
            {
                i.Name = cleaner.Clean(i.Name.ToLowerInvariant(), "RO", false);
                return i;
            }).ToList();

            foreach (var cleanedProduct in cleandProducts)
            {
                var tempMappings = cleanedIngredients.Select(ing => new VendorProductIngredient
                {
                    IdIngredient = ing.Id,
                    IdProduct = cleanedProduct.Id,
                    MappingConfidence = similarityComputer.ComputeSimilarity(ing.Name, cleanedProduct.Name)
                });

                var mappingList = tempMappings.OrderByDescending(m => m.MappingConfidence).ToList();
                var maxConfidence = mappingList.FirstOrDefault();

                var finalList = mappingList.Where(m => m.MappingConfidence == maxConfidence.MappingConfidence)
                    .Where(m => m.MappingConfidence > 0.76);

                mappings.AddRange(finalList);
            }

            vendorRepo.CreateProductIngredientMappings(mappings);

        }

        private static void ImportVendorProducts()
        {
            var carefourCrawler = new CarrefourCrawler();
            var repo = new VendorWriteRepository(ConfigurationManager.ConnectionStrings["Recipes"].ConnectionString);
            var importer = new CarrefourVendorProductImporter(repo);
            var items = carefourCrawler.CrawlProducts().Result;

            importer.Import(items);
        }

        private static void ImportIngrediants()
        {

            var connString = ConfigurationManager.ConnectionStrings["Recipes"].ConnectionString;
            var repo = new NoiseWordsRepository(connString);
            var reciperepo = new RecipeReadRepositoryOld(connString);
            var cleaner = new IngredientsTextCleaner(repo, reciperepo);

            var files = Directory.GetFiles(@"E:\UnicaRecipes\");
            var ingrepo = new IngredientsWriteRepository(ConfigurationManager.ConnectionStrings["Recipes"].ConnectionString);

            var ingredients = new List<string>();
            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var recipes = JsonConvert.DeserializeObject<List<CrawledRecipe>>(json);
                ingredients.AddRange(recipes.SelectMany(recipe => recipe.Ingredients).Distinct());
            }

            var cleanIngredinets = ingredients.Select(i => cleaner.Clean(i.ToLowerInvariant(), "RO")).ToList();

            var importer = new IngredientsImporter();
            //importer.ImportIngredients(ingredients.Distinct());

        }

        private static void ImportRecipes()
        {
            var files = Directory.GetFiles(@"E:\UnicaRecipes\");
            var repo = new RecipeWriteRepository(ConfigurationManager.ConnectionStrings["Recipes"].ConnectionString);

            var allReciepes = new List<CrawledRecipe>();
            foreach (var file in files)
            {
                var json = File.ReadAllText(file);
                var recipes = JsonConvert.DeserializeObject<List<CrawledRecipe>>(json);
                allReciepes.AddRange(recipes);
            }

            var importer = new RecipesImporter(repo);
            importer.ImportRecipes(allReciepes.Distinct());

        }
    }
}
