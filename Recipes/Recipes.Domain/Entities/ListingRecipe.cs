namespace Recipes.Domain.Entities
{
    public class ListingRecipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int PrepTime { get; set; }
        public int Servings { get; set; }
    }
}
