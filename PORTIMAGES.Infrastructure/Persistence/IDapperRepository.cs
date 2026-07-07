
using Dapper;
using System.Data;

namespace PORTIMAGES.Infrastructure.Persistence
{
    public interface IDapperRepository
    {
        // Query multiple rows
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure);

        // Query single row (or null if not found)
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure,int _commandTimeOut=60);

        // Execute insert/update/delete (returns number of affected rows)
        Task<int> ExecuteAsync(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure);

        // Execute scalar (return identity value, count, etc.)
        Task<T> ExecuteScalarAsync<T>(string sql, object parameters = null, CommandType commandType = CommandType.StoredProcedure);

        //QueryMultipleAsync (return multiple result set)
        Task<SqlMapper.GridReader> QueryMultipleAsync(string sql,object parameters = null,CommandType commandType = CommandType.StoredProcedure);

    }
}
