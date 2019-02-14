namespace Recipes.Crawlers.Carrefour
{
    public class Item
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int[] categories { get; set; }
        public decimal price { get; set; }
        public float initialPrice { get; set; }
        public string[] images { get; set; }
        public string published { get; set; }
        public string code { get; set; }
        public float stock { get; set; }
        public int vat { get; set; }
        public float minSellingQuantity { get; set; }
        public float maxSellingQuantity { get; set; }
        public float weightIncrement { get; set; }
        public object[] attributes { get; set; }
        public string packaging { get; set; }
        public string deliveryType { get; set; }
        public string path { get; set; }
        public string brand { get; set; }
        public string vendor { get; set; }
        public int discount { get; set; }
    }
}
