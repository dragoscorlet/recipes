using Recipes.DAL.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Recipes.DAL
{
    public class VendorProductReadRepository
    {
        private string _connectionString;

        public VendorProductReadRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<VendorProduct> GetVendorProducts(IEnumerable<int> ingredients)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[dbo].[GetVendorProductsForRecipe]";

                    command.Parameters.AddWithValue("@Ingredients", CreateDataTable(ingredients));

                    return ReadProducts(command.ExecuteReader());
                }
            }
        }

        private IEnumerable<VendorProduct> ReadProducts(SqlDataReader sqlDataReader)
        {
            using (sqlDataReader)
            {
                var products = new List<VendorProduct>();

                while (sqlDataReader.Read())
                {
                    products.Add(new VendorProduct
                    {
                        Brand = (string)sqlDataReader["Brand"],
                        Title = (string)sqlDataReader["Title"],
                        MassInGrams = (int)sqlDataReader["MassInGrams"],
                        VolumeInMl = (int)sqlDataReader["Volume"],
                        Price = (int)sqlDataReader["Price"],
                        Currency = (string)sqlDataReader["Currency"],
                        InStock = (bool)sqlDataReader["InStock"],
                        SoldByPiece = (bool)sqlDataReader["SolByPiece"],
                        Url = (string)sqlDataReader["Url"],
                        Vendor = (string)sqlDataReader["Vendor"],
                        Id = (int)sqlDataReader["Id"],
                        ImageUrl = (string)sqlDataReader["ImageUrl"]
                    });
                }

                return products;
            }
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

    }
}
