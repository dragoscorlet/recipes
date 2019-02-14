using Recipes.DAL.Entities;
using Recipes.Domain;
using System.Collections.Generic;
using System.Web.Http;

namespace Recipes.API.Controllers
{
    public class IngredientsSuggestController : ApiController
    {
        private readonly RecipeProvider _provider;

        public IngredientsSuggestController()
        {
            _provider = new RecipeProvider();
        }

        [HttpGet]
        public IEnumerable<Ingredient> SuggestIngredients(string name)
        {
            return _provider.SuggestIngredients(name);
        }
    }
}
