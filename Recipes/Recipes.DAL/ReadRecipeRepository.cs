﻿using Recipes.DAL.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Recipes.DAL
{
    public class ReadRecipeRepository
    {
        public string _connectionString;

        public ReadRecipeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Recipe GetRecipeById(int idRecipe)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[GetRecipe]";

                    command.Parameters.AddWithValue("@RecipeId", idRecipe);

                    return ReadRecipe(command.ExecuteReader());
                }
            }

        }

        public IEnumerable<Recipe> GetRecipes(IEnumerable<int> ingredients,int pageNumber)
        {
            return GetRecipes(ingredients, pageNumber, "[dbo].[GetRecipes]");
        }

        public IEnumerable<Recipe> GetRecipesWithExtraIngredients(IEnumerable<int> ingredients, int pageNumber)
        {
            return GetRecipes(ingredients,pageNumber, "[dbo].[GetRecipesExtraIngredients]");
        }


        private IEnumerable<Recipe> GetRecipes(IEnumerable<int> ingredients,int pageNumber, string spName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;

                    command.Parameters.AddWithValue("@Ingredients", CreateDataTable(ingredients));
                    command.Parameters.AddWithValue("@StartRowIndex", pageNumber * 20);
                    command.Parameters.AddWithValue("@PageSize", 20);
                    command.Parameters.Add("@TotalCount", SqlDbType.Int).Direction = ParameterDirection.Output;

                    return ReadRecipes(command.ExecuteReader());
                }
            }

        }

        private IEnumerable<Recipe> ReadRecipes(SqlDataReader sqlDataReader)
        {
            var recipes = new List<Recipe>();

            using (sqlDataReader)
            {
                while (sqlDataReader.Read())
                {
                    recipes.Add(new Recipe
                    {
                        Id = (int)sqlDataReader["Id"],
                        Name = (string)sqlDataReader["Name"],
                        Description = (string)sqlDataReader["Description"],
                        Url = (string)sqlDataReader["Url"],
                        PreparationTimeMinutes = (int)sqlDataReader["PrepTimeMin"],
                        Servings = (int)sqlDataReader["Servings"],
                        Images = new List<string> { (string)sqlDataReader["Image"] }
                    });
                }
            }

            return recipes;
        }

        private static DataTable CreateDataTable(IEnumerable<int> ingredients)
        {   

            DataTable table = new DataTable();
            table.Columns.Add("IngredientId", typeof(int));
            foreach (long id in ingredients)
            {
                table.Rows.Add(id);
            }
            return table;
        }

        private Recipe ReadRecipe(SqlDataReader sqlDataReader)
        {
            using (sqlDataReader)
            {
                var recipe = new Recipe();
                var images = new List<string>();
                var ingredients = new List<RecipeIngredient>();

                while (sqlDataReader.Read())
                {
                    recipe.Id = (int)sqlDataReader["Id"];
                    recipe.Name = (string)sqlDataReader["Name"];
                    recipe.Servings = (int)sqlDataReader["Servings"];
                    recipe.Url = (string)sqlDataReader["Url"];
                    recipe.Description = (string)sqlDataReader["Description"];
                    recipe.PreparationTimeMinutes = (int)sqlDataReader["PrepTimeMin"];

                    if (recipe.Id > 0)
                    {
                        images.Add((string)sqlDataReader["ImageUrl"]);
                        ingredients.Add(new RecipeIngredient
                        {   
                            IdRecipe = recipe.Id,
                            QuantityInGrams = (int)sqlDataReader["QuantityInGrams"],
                            VolumeInMl = (int)sqlDataReader["VolumeInMl"],
                            Ingredient = new Ingredient
                            {
                                Id = (int)sqlDataReader["IngredientId"],
                                Name = (string)sqlDataReader["IngredientName"],
                                IdLanguage = (int)sqlDataReader["IdLanguage"]
                            }
                        });
                    }
                }

                recipe.Ingredients = ingredients;
                recipe.Images = images;

                return recipe;
            }
        }
    }
}
