using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Recipes.Crawlers.Carrefour
{
    public class CarrefourCrawler
    {
        public async Task<IEnumerable<Item>> CrawlProducts()
        {
            var feedUrls = new List<string>
            {
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=822&page=0&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=824&page=9&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=823&page=0&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=825&page=0&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=1188&page=0&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=826&page=0&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=931&page=0&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=872&page=0&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=822&page=1&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=824&page=1&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=823&page=1&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=825&page=1&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=1188&page=1&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=826&page=1&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=931&page=1&size=600",
                "https://carrefour.ro/supermarket/ajax/catalog/filter?category_id=872&page=1&size=600",

            };

            var httpClient = new HttpClient();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


            var items = new List<Item>();

            foreach (var feedurl in feedUrls)
            {
                var json = await httpClient.GetStringAsync(feedurl);

                var root = JsonConvert.DeserializeObject<Rootobject>(json);
                items.AddRange(root.content.items);
            }

            return items;
        }
    }
}
