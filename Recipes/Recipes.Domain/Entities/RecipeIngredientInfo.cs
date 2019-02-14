namespace Recipes.Domain.Entities
{
    public class RecipeIngredientInfo
    {
        public IngredientInfo Ingredient { get; set; }
        public int? QuantityInGrams { get; set; }
        public int? VolumeInMl { get; set; }
    }
}
