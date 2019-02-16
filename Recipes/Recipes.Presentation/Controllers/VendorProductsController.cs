using Recipes.Domain;
using System.Linq;
using System.Web.Mvc;

namespace Recipes.Presentation.Controllers
{
    public class VendorProductsController : Controller
    {
        private readonly RecipeProvider _provider;

        public VendorProductsController()
        {
            _provider = new RecipeProvider();
        }

        public ActionResult GetVendorProductsExactMatch(string ingredients)
        {
            return View(_provider.GetVendorProductsExactMatch(ingredients.Split(',').Select(i => int.Parse(i))));
        }

    }
}