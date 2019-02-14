using System.Collections.Generic;

namespace Recipes.Domain.Entities
{
    public class VendorProductInfo
    {
        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
