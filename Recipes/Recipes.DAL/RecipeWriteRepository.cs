using Recipes.DAL.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Recipes.DAL
{
    public  class RecipeWriteRepository
    {
        private string _connectionString;


        public RecipeWriteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(IEnumerable<Recipe> recipes)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (var recipe in recipes)
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        var recipeId = InsertRecipe(connection, recipe);
                        InsertImages(connection, recipe.Images, recipeId);
                        InsertRecipeIngredients(connection, recipe.Ingredients, recipeId);
                    }
                }
            }
        }

        private int InsertRecipe(SqlConnection conn, Recipe recipe)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Parameters.AddWithValue("@Name", recipe.Name);
                cmd.Parameters.AddWithValue("@Servings", recipe.Servings);
                cmd.Parameters.AddWithValue("@Description", recipe.Description);
                cmd.Parameters.AddWithValue("@PrepTimeMin", recipe.PreparationTimeMinutes);
                cmd.Parameters.AddWithValue("@Url", recipe.Url);
                cmd.CommandText = @"INSERT INTO dbo.Recipes(Name, Servings, Description, PrepTimeMin, Url) 
                                            OUTPUT INSERTED.ID
                                            VALUES(@Name, @Servings, @Description, @PrepTimeMin, @Url)";

                return (int)cmd.ExecuteScalar();
            }
        }

        private void InsertImages(SqlConnection conn, IEnumerable<string> images, int recipeId)
        {
            foreach(var image in images)
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("@Url", image);
                    cmd.Parameters.AddWithValue("@IdRecipe", recipeId);
                    cmd.CommandText = @"INSERT INTO dbo.Recipe_Images(IdRecipe, Url) VALUES(@IdRecipe, @Url)";

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void InsertRecipeIngredients(SqlConnection conn, IEnumerable<RecipeIngredient> ingredients, int recipeId)
        {
            foreach (var ingredient in ingredients)
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("@IdRecipe", recipeId);
                    cmd.Parameters.AddWithValue("@IdIngredient", ingredient.Ingredient.Id);
                    cmd.Parameters.AddWithValue("@QuantityInGrams", ingredient.QuantityInGrams);
                    cmd.Parameters.AddWithValue("@VolumeInMl", ingredient.VolumeInMl);

                    cmd.CommandText = @"INSERT INTO dbo.Recipe_Ingredients(IdRecipe, IdIngredient, QuantityInGrams, VolumeInMl) 
                                            VALUES(@IdRecipe, @IdIngredient, @QuantityInGrams, @VolumeInMl)";

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
