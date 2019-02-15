using Recipes.Domain;
using Recipes.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Recipes.API.Controllers
{
    public class VendorProductsController : ApiController
    {
        private readonly RecipeProvider _provider;

        public VendorProductsController()
        {
            _provider = new RecipeProvider();
        }

        [HttpGet]
        public IEnumerable<VendorProductInfo> GetVendorProductsExactMatch(string ingredients)
        {
            return _provider.GetVendorProductsExactMatch(ingredients.Split(',').Select(i => int.Parse(i)));
        }

        [HttpGet]
        public IEnumerable<VendorProductInfo> GetVendorProductsExtraMatches(string ingredients)
        {
            return _provider.GetVendorProductsExtraMatches(ingredients.Split(',').Select(i => int.Parse(i)));
        }

    }
}
