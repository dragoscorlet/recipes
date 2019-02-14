namespace Recipes.DAL.Entities
{
    public class VendorProduct
    {   
        public int Id { get; set; }
        public int IdLanguage { get; set; }
        public string Title { get; set; }
        public double? MassInGrams { get; set; }
        public double? VolumeInMl { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string ImageUrl { get; set; }
        public bool InStock { get; set; }
        public bool SoldByPiece { get; set; }
        public string Url { get; set; }
        public string Brand { get; set; }
        public string Vendor { get; set; }

    }
}
