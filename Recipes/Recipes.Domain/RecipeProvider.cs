using Recipes.DAL;
using Recipes.DAL.Entities;
using Recipes.Domain.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Recipes.Domain
{
    public class RecipeProvider
    {
        private readonly IngredientsReadRepository _ingredientsRepository;
        private readonly VendorProductReadRepository _vendorProductsrepository;
        private readonly ReadRecipeRepository _reciperepository;

        public RecipeProvider()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Recipes"].ConnectionString;

            _ingredientsRepository = new IngredientsReadRepository(connectionString);
            _vendorProductsrepository = new VendorProductReadRepository(connectionString);
            _reciperepository = new ReadRecipeRepository(connectionString);
        }

        public IEnumerable<Ingredient> SuggestIngredients(string partialName)
        {
           return  _ingredientsRepository.GetIngredientSuggestions(partialName);
        }

        public IEnumerable<ListingRecipe> GetListingRecipes(IEnumerable<int> ingredients)
        {
           return _reciperepository.GetRecipes(ingredients).Select(ing => new ListingRecipe
            {
                Id = ing.Id,
                Image = ing.Images.FirstOrDefault(),
                Name = ing.Name,
                PrepTime = ing.PreparationTimeMinutes,
                Servings = ing.Servings
            });
        }

        public DetailRecipe GetDetailRecipe(int idRecipe)
        {
            var recipe = _reciperepository.GetRecipeById(idRecipe);

            return new DetailRecipe
            {
                Id = recipe.Id,
                Description = recipe.Description,
                Images = recipe.Images,
                Name = recipe.Name,
                Servings = recipe.Servings,
                Url = recipe.Url,
                PreparationTimeMinutes = recipe.PreparationTimeMinutes,
                Ingredients = recipe.Ingredients.Select(ing => new RecipeIngredientInfo
                {
                    QuantityInGrams = ing.QuantityInGrams,
                    VolumeInMl = ing.VolumeInMl,
                    Ingredient = new IngredientInfo
                    {
                        Id = ing.Ingredient.Id,
                        Name = ing.Ingredient.Name
                    }
                })
            };
        }

        private IEnumerable<VendorProductInfo> GetVendorProducts(IEnumerable<int> ingredients)
        {
           return _vendorProductsrepository.GetVendorProducts(ingredients)
                .GroupBy(ing => ing.Brand)
                .Select(group => new VendorProductInfo
                {
                    Name = group.Key,
                    Products = group.Select(dbIng => new Product
                    {
                        Brand = dbIng.Brand,
                        Currency = dbIng.Currency,
                        Id = dbIng.Id,
                        ImageUrl = dbIng.ImageUrl,
                        InStock = dbIng.InStock,
                        Price = dbIng.Price,
                        Title = dbIng.Title,
                        Url = dbIng.Url
                    })
                });
        }
    }
}
