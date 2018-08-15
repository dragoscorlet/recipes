using Recipes.Crawlers.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System;
using Newtonsoft.Json;

namespace Recipes.Crawlers.Unica
{
    public class UnicaCrawler
    {
        public async Task CrawlRecipes()
        {
            var parser = new UnicaParser();
            var httpClient = new HttpClient();

            var html = await httpClient.GetStringAsync("https://retete.unica.ro/");
            var categoriesUrls = parser.GetCategoriesUrls(html);
            var allRecipesUrls = new List<string>();

            foreach (var category in categoriesUrls)
           {
                var categoryHtml = await TryGetStringAsync(category);

                if (string.IsNullOrEmpty(categoryHtml))
                    continue;

                var allpages = parser.GetAllPagesUrls(categoryHtml, category);

                var recipesUrls = await GetPagesRecipesUrls(allpages);
                allRecipesUrls.AddRange(recipesUrls);
           }

            await GetRecipes(allRecipesUrls.Distinct());
        }

        private async  Task<IEnumerable<string>> GetPagesRecipesUrls(IEnumerable<string> urls)
        {
            var parser = new UnicaParser();
            var allRecipesUrls = new List<string>();

            foreach (var url in urls)
            {
                var recipeDetailsPage =  await TryGetStringAsync(url);

                if (string.IsNullOrEmpty(recipeDetailsPage))
                    continue;

                var recipesUrls = parser.GetRecipeUrls(recipeDetailsPage);
                allRecipesUrls.AddRange(recipesUrls);
            }

            return allRecipesUrls;
        }

        private async Task GetRecipes(IEnumerable<string> urls)
        {
            var parser = new UnicaParser();
            var allRecipesUrls = new List<CrawledRecipe>();

            int i = 0;
            foreach (var url in urls)
            {
                if(i == 200)
                {
                    i = 0;
                    var json = JsonConvert.SerializeObject(allRecipesUrls);
                    File.WriteAllText(@"E:\UnicaRecipes\" + Guid.NewGuid().ToString() + ".json", json);
                    allRecipesUrls.Clear();
                }
                else
                {
                    i++;
                }
                var recipeDetailsPage = await TryGetStringAsync(url);
                if (string.IsNullOrEmpty(recipeDetailsPage))
                    continue;

                var recipesUrls = parser.GetRecipe(recipeDetailsPage,url);
                if(recipesUrls != null)
                    allRecipesUrls.Add(recipesUrls);
            }
        }

        private async Task<string> TryGetStringAsync(string url)
        {
            HttpClient httpClient = new HttpClient();
            try
            {
                var html = await httpClient.GetStringAsync(url);

                return html;
            }catch
            {
                return string.Empty;
            }
        }
    }
}
