using Recipes.Crawlers.Carrefour;
using Recipes.DAL.Entities;
using System.Linq;
using System.Text.RegularExpressions;

namespace Recipes.Importer
{
    public static  class VendorMappingExtensions
    {
        public static VendorProduct MapCarrefourProduct(this Item item)
        {
            return new VendorProduct
            {
                Brand = item.brand,
                ImageUrl = "https://carrefour.ro/supermarket/media/" +  item.images.FirstOrDefault(),
                Currency = "RON",
                InStock = item.stock > 0,
                Price = item.price,
                Title = item.title,
                Url = "https://carrefour.ro/supermarket/" +  item.path,
                Vendor = item.vendor,
                SoldByPiece = item.packaging.Equals("PIECE"),
                MassInGrams = GetQuantity(item.title),
                VolumeInMl = GetVolume(item.title)
            };
        }

        private static double? GetVolume(string title)
        {
            var match = Regex.Match(title, @"(?<capture>\d+)ml");

            if (match == null || match.Groups == null || string.IsNullOrEmpty(match.Groups["capture"].Value))
                return 0;

            return int.Parse(match.Groups["capture"].Value);

        }

        private static double? GetQuantity(string title)
        {
            var match = Regex.Match(title, @"(?<capture>\d+)g");

            if (match == null || match.Groups == null || string.IsNullOrEmpty(match.Groups["capture"].Value))
                return 0;

            return int.Parse(match.Groups["capture"].Value);
        }
    }
}
