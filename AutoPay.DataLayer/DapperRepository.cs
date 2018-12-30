using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoPay.Infrastructure.DataLayer;

namespace AutoPay.DataLayer
{
    public class DapperRepository : IDapperRepository
    {
        public async Task<IEnumerable<T>> ExecuteReader<T>(string connectionString, string sqlQuery)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                var result = await sqlConnection.QueryAsync<T>(sqlQuery);
                return result.ToList();
            }
        }

        public async Task<T> ExecuteScalar<T>(string connectionString, string sqlQuery)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                return await sqlConnection.ExecuteScalarAsync<T>(sqlQuery);
            }
        }

        public async Task ExceuteNonQuery(string connectionString, string sqlQuery, bool isStoredProcedure = false, int commandTimeout = 20)
        {
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                await sqlConnection.ExecuteAsync(sqlQuery,
                    commandType: isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text,
                    commandTimeout: commandTimeout);
            }
        }
    }
}
