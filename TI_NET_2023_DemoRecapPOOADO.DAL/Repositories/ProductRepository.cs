using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TI_NET_2023_DemoRecapPOOADO.Domain.Entities;
using TI_NET_2023_DemoRecapPOOADO.Domain.enums;

namespace TI_NET_2023_DemoRecapPOOADO.DAL.Repositories
{
    public class ProductRepository : BaseRepository<int,Product>, IProductRepository 
    { 

        public ProductRepository() : base("Product","Id") { }
        

        protected override Product Convert(IDataRecord record)
        {
            return new Product
            {
                Id = (int)record["Id"],
                Name = (string)record["product_name"],
                Description = record["product_description"] == DBNull.Value ? null : (string)record["product_description"],
                Quantity = (int)record["quantity"],
                Category = Enum.Parse<Category>((string)record["Category"]),
                Price = (decimal)record["price"]
            };
        }

        public override Product Create(Product product)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                using (IDbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO PRODUCT (product_name," +
                                                           "product_description," +
                                                           "quantity," +
                                                           "category," +
                                                           "price) " +
                                      "OUTPUT INSERTED.* " +
                                      "VALUES (@name,@description,@quantity,@category,@price)";
                    GenerateParameter(cmd, "@name", product.Name);
                    GenerateParameter(cmd, "@description", product.Description);
                    GenerateParameter(cmd, "@quantity", product.Quantity);
                    GenerateParameter(cmd, "@category", product.Category);
                    GenerateParameter(cmd, "@price", product.Price);

                    OpenConnection(connection);

                    IDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                    {
                        throw new Exception("Error");
                    }

                    Product p = Convert(reader);

                    connection.Close();

                    return p;
                }
            }
        }

        public override bool Update(int id, Product product)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                using (IDbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE PRODUCT " +
                                      "SET product_name = @name, " +
                                          "product_description = @description, " +
                                          "quantity = @quantity" +
                                          "category = @category" +
                                          "price = @price " +
                                      "WHERE Id = @id";
                    GenerateParameter(cmd, "@name", product.Name);
                    GenerateParameter(cmd, "@description", product.Description);
                    GenerateParameter(cmd, "@quantity", product.Quantity);
                    GenerateParameter(cmd, "@category", product.Category);
                    GenerateParameter(cmd, "@price", product.Price);
                    GenerateParameter(cmd, "@id", id);

                    OpenConnection(connection);

                    int nbRow = cmd.ExecuteNonQuery();

                    connection.Close();

                    return nbRow == 1;
                }
            }
        }
    }
}
