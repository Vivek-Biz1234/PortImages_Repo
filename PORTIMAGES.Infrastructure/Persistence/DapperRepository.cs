using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Dapper;
using System.Data.Common;

namespace PORTIMAGES.Infrastructure.Persistence
{
    public class DapperRepository : IDapperRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        public DapperRepository(IConfiguration config)
        {
            this._config = config;
            this._connectionString = _config.GetConnectionString("Connection_PortImagesDB");
        }
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        //For multiple rows
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object paramters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<T>(sql, paramters, commandType: commandType);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure,int _commandTimeOut=60)
        {
            using var connection = CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters, commandType: commandType,commandTimeout: _commandTimeOut);
        }

        public async Task<int> ExecuteAsync(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(sql, parameters, commandType: commandType);
        }
        public async Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteScalarAsync<T>(sql, parameters, commandType: commandType);
        }

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql,object parameters = null,CommandType commandType = CommandType.StoredProcedure)
        {
            var connection = (SqlConnection)CreateConnection();
            await connection.OpenAsync();
            return await connection.QueryMultipleAsync(sql, parameters, commandType: commandType);
        }

    }
}
