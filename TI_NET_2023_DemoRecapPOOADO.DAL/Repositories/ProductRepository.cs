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
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository()
        {
            _connectionString = @"Server=BSTORM\SQLSERVER;Database=TI_NET_2023_Distilery;Trusted_Connection=True";
        }
        private void GenerateParameter(IDbCommand cmd, string paramName, object? value)
        {
            IDataParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = paramName;
            parameter.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(parameter);
        }

        private Product Convert(IDataRecord record)
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

        public Product Create(Product product)
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

                    connection.Open();

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

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Product ReadOne(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
