namespace Recipes.DAL.Entities
{
    public class VendorProductIngredient
    {
        public int IdProduct { get; set; }
        public int IdIngredient { get; set; }
        public double MappingConfidence { get; set; }
    }
}
