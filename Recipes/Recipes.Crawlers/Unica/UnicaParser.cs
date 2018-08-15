using HtmlAgilityPack;
using Recipes.Crawlers.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Recipes.Crawlers.Unica
{
    public class UnicaParser
    {
        public IEnumerable<string> GetCategoriesUrls(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);

            return document.DocumentNode.SelectNodes("//ul[@class='nav']//a")
                .Select(node => node.GetAttributeValue("href", string.Empty))
                .Where(url => !string.IsNullOrEmpty(url))
                .Distinct();
        }

        public CrawledRecipe GetRecipe(string html, string url)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);

            try
            {
                return new CrawledRecipe
                {
                    Url = url,
                    Name = document.DocumentNode.SelectSingleNode("//h1[@itemprop='name']").InnerText,
                    Description = document.DocumentNode.SelectSingleNode("//div[@itemprop='recipeInstructions']").InnerText,
                    ImageUrls = document.DocumentNode.SelectNodes("//img[@itemprop='image']")
                        .Select(node => node.GetAttributeValue("src", string.Empty))
                        .Where(src => !string.IsNullOrEmpty(src)),
                    Ingredients = document.DocumentNode.SelectNodes("//div[@class='ingredients-wrapper']//ul/li")
                        .Select(node => node.InnerText)
                        .Where(src => !string.IsNullOrEmpty(src)),
                    Category = document.DocumentNode.SelectSingleNode("//span[@typeof='v:Breadcrumb'][2]").InnerText,
                    SubCategory = document.DocumentNode.SelectSingleNode("//span[@typeof='v:Breadcrumb'][3]").InnerText
                };
            }catch
            {
                return null;
            }
        }

        public IEnumerable<string> GetRecipeUrls(string html)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);

            return document.DocumentNode.SelectNodes("//div[@class='row']//div[@class='cta']/a")
                .Select(node => node.GetAttributeValue("href", string.Empty))
                .Where(url => !string.IsNullOrEmpty(url))
                .Distinct();
        }

        public IEnumerable<string> GetAllPagesUrls(string html, string initialUrl)
        {
            var document = new HtmlDocument();
            document.LoadHtml(html);

            var totalNumberofPages = GetTotalNumberofPages(document.DocumentNode.SelectSingleNode("//a[@class='last']"));

            if (totalNumberofPages == -1)
                return new List<string>();

            return Enumerable.Range(1, totalNumberofPages)
                .Select(index => initialUrl + "/page/" + index +  "/");
        }

        private int GetTotalNumberofPages(HtmlNode node)
        {
            if (node == null)
                return -1;

            var href = node.GetAttributeValue("href", string.Empty);

            if (string.IsNullOrEmpty(href))
                return -1;

            var result = Regex.Match(href, @"\d+").Value;

            return int.Parse(result);
        }
    }
}
