using Recipes.Domain;
using Recipes.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public IEnumerable<ListingRecipe> GetRecipes(string ingredients,int pageNumber, bool includeExtra)
        {   

            return _provider.GetListingRecipes(ingredients.Split(',').Select(i => int.Parse(i)), pageNumber, includeExtra);
        }

        [HttpGet]
        public DetailRecipe GetRecipe(int idRecipe)
        {
            return _provider.GetDetailRecipe(idRecipe);
        }

    }
}
