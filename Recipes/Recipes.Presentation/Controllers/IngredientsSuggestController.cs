using Recipes.Domain;
using System.Web.Mvc;

namespace Recipes.Presentation.Controllers
{
    public class IngredientsSuggestController : Controller
    {
        private readonly RecipeProvider _provider;

        public IngredientsSuggestController()
        {
            _provider = new RecipeProvider();
        }

        
        public ActionResult SuggestIngredients(string name)
        {
            return  Json(_provider.SuggestIngredients(name));
        }

    }
}