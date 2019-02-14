using Recipes.DAL.Entities;
using Recipes.DAL.Schema;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Recipes.DAL
{
    public class RecipeReadRepositoryOld : IRecipeReadRepository
    {
        private string _connectionString;

        public RecipeReadRepositoryOld(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<string> GetAllBrands()
        {

            var words = new List<string>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select brand from dbo.v_Brands";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            words.Add(((string)reader["brand"]).ToLowerInvariant());
                        }
                    }
                }
            }

            return words;
        }

        public IEnumerable<VendorProduct> GetAllVendorProducts()
        {

            var products = new List<VendorProduct>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select
                              Id, Title, MassInGrams, VolumeInMl, Price, Currency, ImageUrl, InStock, SoldByPiece, Url, Brand, Vendor, IdLanguage 
                            from dbo.Vendor_Products
                            ";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new VendorProduct
                            {
                                Id = (int)reader["Id"],
                                Title = (string)reader["Title"],
                                MassInGrams = (int)reader["MassInGrams"],
                                VolumeInMl = (int)reader["VolumeInMl"],
                                Price = (decimal)reader["Price"],
                                Currency = (string)reader["Currency"],
                                ImageUrl = (string)reader["ImageUrl"],
                                InStock = (bool)reader["InStock"],
                                SoldByPiece = (bool)reader["SoldByPiece"],
                                Url = (string)reader["Url"],
                                Brand = (string)reader["Brand"],
                                Vendor = (string)reader["Vendor"],
                                IdLanguage = (int)reader["IdLanguage"]
                            });
                        }
                    }
                }
            }

            return products;
        }

    }
}
