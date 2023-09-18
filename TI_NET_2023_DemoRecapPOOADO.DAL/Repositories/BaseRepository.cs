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
    public abstract class BaseRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity> where TEntity : class
    {
        protected readonly string _connectionString;
        protected readonly string _tableName;
        protected readonly string _columnIdName;

        public BaseRepository(string tableName,string columnIdName)
        {
            _connectionString = @"Server=BSTORM\SQLSERVER;Database=TI_NET_2023_Distilery;Trusted_Connection=True";
            _tableName = tableName;
            _columnIdName = columnIdName;
        }

        public void OpenConnection(IDbConnection connection)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            connection.Open();
        }

        protected void GenerateParameter(IDbCommand cmd, string paramName, object? value)
        {
            IDataParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = paramName;
            parameter.Value = value ?? DBNull.Value;
            cmd.Parameters.Add(parameter);
        }

        protected abstract TEntity Convert(IDataRecord record);

        public abstract TEntity Create(TEntity entity);

        public IEnumerable<TEntity> ReadAll()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                using (IDbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {_tableName}";
                    OpenConnection(connection);
                    IDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        yield return Convert(reader);
                    }
                }
            }
        }

        public TEntity ReadOne(TKey id)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                using (IDbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"SELECT * FROM {_tableName} WHERE {_columnIdName} = @id";
                    GenerateParameter(cmd, "@id", id);
                    OpenConnection(connection);
                    IDataReader reader = cmd.ExecuteReader();

                    if (!reader.Read())
                    {
                        throw new KeyNotFoundException();
                    }

                    TEntity entity = Convert(reader);
                    connection.Close();
                    return entity;
                }
            }
        }

        public abstract bool Update(TKey id, TEntity entity);

        public bool Delete(TKey id)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                using (IDbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM {_tableName} " +
                                      $"WHERE {_columnIdName} = @id";
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
