using Recipes.Domain;
using System.Linq;
using System.Web.Mvc;

namespace Recipes.Presentation.Controllers
{
    public class RecipesController : Controller
    {
        private readonly RecipeProvider _provider;

        public RecipesController()
        {
            _provider = new RecipeProvider();
        }


        // GET: Recipes
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRecipes(string ingredients, int pageNumber, bool includeExtra)
        {
            var ids = ingredients.Split(',').Where(i => !string.IsNullOrEmpty(i)).Select(i => int.Parse(i));

            return View(_provider.GetListingRecipes(ids, pageNumber, includeExtra));
        }

        public ActionResult GetRecipe(int idRecipe)
        {
            return View(_provider.GetDetailRecipe(idRecipe));
        }


    }
}