using Recipes.DAL.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Recipes.DAL
{
    public class IngredientsReadRepository
    {
        private string _connectionString;

        public IngredientsReadRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IEnumerable<Ingredient> GetIngredientSuggetions(string partialIngredientName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "dbo.GetIngredientsByPartialName";

                    command.Parameters.AddWithValue("@PartialName", partialIngredientName);

                    return ReadIngredients(command.ExecuteReader());
                }
            }
        }

        private IEnumerable<Ingredient> ReadIngredients(SqlDataReader sqlDataReader)
        {
            using (sqlDataReader)
            {
                var ingredients = new List<Ingredient>();

                while (sqlDataReader.Read())
                {
                    ingredients.Add(new Ingredient
                    {
                        Id = (int)sqlDataReader["Id"],
                        Name = (string)sqlDataReader["Name"],
                        IdLanguage= (int)sqlDataReader["IdLanguage"]
                    });
                }

                return ingredients;
            }
        }
    }
}
