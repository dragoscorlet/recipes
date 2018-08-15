namespace Recipes.Crawlers.Carrefour.Models
{
    public class VendorProduct
    {
        public string Title { get; set; }
        public double? MassInGrams { get; set; }
        public double? VolumeInMl { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public string Currency { get; set; }
        public string ImageUrl { get; set; }
        public bool InStock { get; set; }
        public bool SoldByPiece { get; set; }
        public string Url { get; set; }
        public string Brand { get; set; }
        public string Vendor { get; set; }
    }
}
