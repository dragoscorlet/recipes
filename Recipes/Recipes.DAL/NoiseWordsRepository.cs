using Recipes.DAL.Schema;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Recipes.DAL
{
    public class NoiseWordsRepository : INoiseWordsRepository
    {
        private string _connectionString;

        public NoiseWordsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<string> GetWords(string language)
        {
            var words = new List<string>();

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.Parameters.AddWithValue("@Language", language);
                    cmd.CommandText = "dbo.GetNoiseWords";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            words.Add((string)reader["word"]);
                        }
                    }
                }
            }

            return words;
        }
    }
}
