using Recipes.DAL.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Recipes.DAL
{
    public class IngredientsReadRepositoryOld
    {
        private string _connectionString;

        public IngredientsReadRepositoryOld(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Ingredient GetIngredientByName(string name)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("@Name", name);

                    cmd.CommandText = @" SELECT TOP 1 Id FROM dbo.Ingredients WHERE Name = @Name";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var id = (int)reader["Id"];

                            return new Ingredient
                            {
                                Id = id,
                                Name = name
                            };
                        }
                    }
                }
            }

            return null;
        }
        
        public IEnumerable<Ingredient> GetAllIngredienst()
        {
            var ingredients = new List<Ingredient>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"SELECT * FROM dbo.Ingredients";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var id = (int)reader["Id"];

                            ingredients.Add(new Ingredient
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"]
                            });
                        }
                    }
                }
            }

            return ingredients;
        }
    }
}
