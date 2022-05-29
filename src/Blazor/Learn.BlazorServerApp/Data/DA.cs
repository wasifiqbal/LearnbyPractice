using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Learn.BlazorServerApp.Data
{
    public class DA : IDA
    {
        private readonly IConfiguration _config;
        public string ConnectionStringName { get; set; } = "Default";
        public DA(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string connectionstring = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionstring))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);
                return data.ToList();
            }
        }
        public async Task<T> Find<T, U>(string sql, U parameters)
        {
            string connectionstring = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionstring))
            {
                var data = await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
                return data;
            }
        }

        public async Task SaveData<T>(string sql, T parameters)
        {
            string connectionstring = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionstring))
            {
                var data = await connection.ExecuteAsync(sql, parameters);
            }
        }

        public async Task Delete<T>(string sql, T parameters)
        {
            string connectionstring = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionstring))
            {
                await connection.ExecuteScalarAsync(sql, parameters);
            }
        }
    }
}
