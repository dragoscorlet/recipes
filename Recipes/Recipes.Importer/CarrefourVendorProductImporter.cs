using Recipes.Crawlers.Carrefour;
using Recipes.DAL;
using System.Collections.Generic;
using System.Linq;

namespace Recipes.Importer
{
    public class CarrefourVendorProductImporter
    {
        private VendorWriteRepository _repository;

        public CarrefourVendorProductImporter(VendorWriteRepository repository)
        {
            _repository = repository;
        }


        public void Import(IEnumerable<Item> items)
        {
            var products = items.Select(item => item.MapCarrefourProduct());

            _repository.Create(products);
        }
    }
}
