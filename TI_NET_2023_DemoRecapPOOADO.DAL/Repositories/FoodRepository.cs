using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_2023_DemoRecapPOOADO.Domain.Entities;

namespace TI_NET_2023_DemoRecapPOOADO.DAL.Repositories
{
    public class FoodRepository : BaseRepository<int, Food>, IFoodRepository
    {

        public FoodRepository() : base("Food","Id") { }

        protected override Food Convert(IDataRecord record)
        {
            return new Food()
            {
                Id = (int)record["Id"],
                Name = (string)record["Name"],
                Description = (string)record["Description"]
            };
        }

        public override Food Create(Food entity)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                using(IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO Food (Name,Description)" +
                                      $"OUTPUT INSERTED.*" +
                                      $"VALUES (@name,@description)";
                    GenerateParameter(cmd, "@name", entity.Name);
                    GenerateParameter(cmd, "@description", entity.Description);

                    OpenConnection(conn);

                    IDataReader reader = cmd.ExecuteReader();

                    if(!reader.Read())
                    {
                        throw new Exception("Error");
                    }

                    Food food = Convert(reader);

                    conn.Close();

                    return food;
                }
            }
        }

        public override bool Update(int id, Food entity)
        {
            using (IDbConnection conn = new SqlConnection(_connectionString))
            {
                using (IDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"UPDATE Food " +
                                      $"SET Name = @name, " +
                                          $"Description = @description " +
                                      $"WHERE Id = @id";

                    GenerateParameter(cmd, "@name", entity.Name);
                    GenerateParameter(cmd, "@description", entity.Description);
                    GenerateParameter(cmd, "@id", id);

                    OpenConnection(conn);

                    int nbRow = cmd.ExecuteNonQuery();

                    conn.Close();

                    return nbRow == 1;
                }
            }
        }
    }
}
