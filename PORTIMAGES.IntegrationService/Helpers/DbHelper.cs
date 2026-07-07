using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace PORTIMAGES.IntegrationService.Helpers
{
    public class DbHelper
    {
        public static async Task<DataTable> ExecuteDataTableAsync(string conStr,string query,CommandType commandType=CommandType.Text, Dictionary<string, object> parameters = null)
        {
            using var con = new SqlConnection(conStr);
            await con.OpenAsync();

            using var cmd = new SqlCommand(query, con);
            cmd.CommandType = commandType;

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                }
            } 

            using var reader = await cmd.ExecuteReaderAsync();
            var dt = new DataTable();
            dt.Load(reader);
            return dt;
        }

        public static async Task<IEnumerable<T>> QueryAsync<T>(string conStr, string query,object param = null,CommandType commandType = CommandType.Text)
        {
            using var con = new SqlConnection(conStr); 
            return await con.QueryAsync<T>(query, param, commandType: commandType);
        }

        public static async Task<int> ExecuteAsync(string conStr, string query,object param = null,CommandType commandType = CommandType.Text)
        {
            using var con = new SqlConnection(conStr);
            return await con.ExecuteAsync(query, param, commandType: commandType);
        }

        public static async Task<T> ExecuteScalarAsync<T>(string conStr, string query,object param = null,CommandType commandType = CommandType.Text)
        {
            using var con = new SqlConnection(conStr); 
            return await con.ExecuteScalarAsync<T>(query, param, commandType: commandType);
        }
    }
}
