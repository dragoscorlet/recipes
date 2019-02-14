using Recipes.DAL.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Recipes.DAL
{
    public class IngredientsWriteRepository
    {
        private string _connectionString;
            
        public IngredientsWriteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateIngredients(IEnumerable<Ingredient> ingredients)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (var ingredient in ingredients)
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.Parameters.AddWithValue("@Name", ingredient.Name);
                        cmd.Parameters.AddWithValue("@IdLanguage", ingredient.IdLanguage);

                        cmd.CommandText = "INSERT INTO dbo.Ingredients(Name, IdLanguage) VALUES(@Name, @IdLanguage)";
                        cmd.ExecuteNonQuery();
                    }
                }
            }

        }
    }
}
