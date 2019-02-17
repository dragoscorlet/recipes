using Recipes.DAL;
using Recipes.DAL.Entities;
using Recipes.Domain.Entities;
using System;
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

        public IEnumerable<ListingRecipe> GetListingRecipes(IEnumerable<int> ingredients, int pageNumber, bool includeExtraIngredients)
        {
           var exactMatchRecipes =_reciperepository.GetRecipes(ingredients, pageNumber).Select(ing => new ListingRecipe
            {
                Id = ing.Id,
                Image = ing.Images.FirstOrDefault(),
                Name = ing.Name,
                PrepTime = ing.PreparationTimeMinutes,
                Servings = ing.Servings
            });

            return exactMatchRecipes;
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

        public IEnumerable<VendorProductInfo> GetVendorProductsExactMatch(IEnumerable<int> ingredients)
        {
            return GetVendorProducts(ingredients, (ids, vendor) => true);
        }

        public IEnumerable<VendorProductInfo> GetVendorProductsExtraMatches(IEnumerable<int> ingredients)
        {
            return GetVendorProducts(ingredients, (ids, vendor) => ids.Count() < vendor.Products.Count());
        }


        private IEnumerable<VendorProductInfo> GetVendorProducts(IEnumerable<int> ingredients, Func<IEnumerable<int>, VendorProductInfo, bool> productFilter)
        {
           return _vendorProductsrepository.GetVendorProducts(ingredients)
                .GroupBy(ing => ing.Vendor)
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
                }).Where(vendor => productFilter(ingredients, vendor));
        }

    }
}
