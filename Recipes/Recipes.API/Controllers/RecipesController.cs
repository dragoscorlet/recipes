using Recipes.Domain;
using Recipes.Domain.Entities;
using System.Collections.Generic;
using System.Web.Http;

namespace Recipes.API.Controllers
{
    public class RecipesController : ApiController
    {
        private readonly RecipeProvider _provider;

        public RecipesController()
        {
            _provider = new RecipeProvider();
        }

        public IEnumerable<ListingRecipe> GetAllProducts(IEnumerable<int> ingredients)
        {
            return _provider.GetListingRecipes(ingredients);
        }

        public DetailRecipe GetAllProducts(int idRecipe)
        {
            return _provider.GetDetailRecipe(idRecipe);
        }

    }
}
