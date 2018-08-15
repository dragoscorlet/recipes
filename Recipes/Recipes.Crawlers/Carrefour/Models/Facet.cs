namespace Recipes.Crawlers.Carrefour
{
    public class Facet
    {
        public string name { get; set; }
        public string code { get; set; }
        public bool filterable { get; set; }
        public bool sortable { get; set; }
        public bool rangeable { get; set; }
        public Value[] values { get; set; }
    }
}
