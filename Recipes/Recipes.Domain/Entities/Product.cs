namespace Recipes.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string ImageUrl { get; set; }
        public bool InStock { get; set; }
        public string Url { get; set; }
        public string Brand { get; set; }
    }
}
