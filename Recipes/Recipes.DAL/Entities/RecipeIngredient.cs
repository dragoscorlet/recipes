namespace Recipes.DAL.Entities
{
    public class RecipeIngredient
    {
        public int IdRecipe { get; set; }
        public Ingredient Ingredient {get; set;}
        public int? QuantityInGrams { get; set; }
        public int? VolumeInMl {get; set;}

    }
}
