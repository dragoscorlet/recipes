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

        public IEnumerable<VendorProductInfo> GetVendorProducts(string ingredients)
        {
            return _provider.GetVendorProducts(ingredients.Split(',').Select(i => int.Parse(i)));
        }
    }
}
