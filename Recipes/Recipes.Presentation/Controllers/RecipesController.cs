using Recipes.Domain;
using Recipes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        public ActionResult GetRecipes(string ingredients, bool includeExtra)
        {
            return View(_provider.GetListingRecipes(ingredients.Split(',').Select(i => int.Parse(i)), includeExtra));
        }

        public ActionResult GetRecipe(int idRecipe)
        {
            return View(_provider.GetDetailRecipe(idRecipe));
        }


    }
}