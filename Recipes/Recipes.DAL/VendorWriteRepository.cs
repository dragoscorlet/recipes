using Recipes.DAL.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Recipes.DAL
{
    public class VendorWriteRepository
    {
        private string _connectionString;


        public VendorWriteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateProductIngredientMappings(IEnumerable<VendorProductIngredient> products)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (var product in products)
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        InsertProductIngredientMapping(connection, product);
                    }
                }
            }

        }

        public void Create(IEnumerable<VendorProduct> products)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (var product in products)
                {
                    using (var cmd = connection.CreateCommand())
                    {
                        InsertProduct(connection, product);
                    }
                }
            }

        }

        private void InsertProductIngredientMapping(SqlConnection conn, VendorProductIngredient product)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Parameters.AddWithValue("@IdIngredient", product.IdIngredient);
                cmd.Parameters.AddWithValue("@IdProduct", product.IdProduct);
                cmd.Parameters.AddWithValue("@MappingConfidence", product.MappingConfidence);
                cmd.CommandText = @"INSERT INTO [dbo].[Vendor_IngredientProductMapping]
                (IdIngredient, IdProduct, MappingConfidence)
                VALUES(@IdINgredient, @IdProduct, @MappingConfidence)";

                cmd.ExecuteNonQuery();
            }

        }

        private void InsertProduct(SqlConnection conn, VendorProduct product)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.Parameters.AddWithValue("@Title", product.Title);
                cmd.Parameters.AddWithValue("@MassInGrams", product.MassInGrams);
                cmd.Parameters.AddWithValue("@VolumeInMl", product.VolumeInMl);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Currency", product.Currency);
                cmd.Parameters.AddWithValue("@ImageUrl", product.ImageUrl);
                cmd.Parameters.AddWithValue("@InStock", product.InStock);
                cmd.Parameters.AddWithValue("@SoldByPiece", product.SoldByPiece);
                cmd.Parameters.AddWithValue("@Url", product.Url);
                cmd.Parameters.AddWithValue("@Brand", product.Brand?? string.Empty);
                cmd.Parameters.AddWithValue("@Vendor", product.Vendor ?? string.Empty);

                cmd.CommandText = @"INSERT INTO dbo.Vendor_Products
                (Title,MassInGrams,VolumeInMl ,Price,Currency,ImageUrl,InStock,SoldByPiece,[Url],Brand,Vendor)
                VALUES(@Title,@MassInGrams,@VolumeInMl ,@Price,@Currency,@ImageUrl,@InStock,@SoldByPiece,@Url,@Brand,@Vendor)";

                cmd.ExecuteNonQuery();
            }
        }
    }
}
