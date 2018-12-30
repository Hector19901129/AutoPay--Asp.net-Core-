using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoPay.Infrastructure.DataLayer
{
    public interface IDapperRepository
    {
        Task<IEnumerable<T>> ExecuteReader<T>(string connectionString, string sqlQuery);
        Task<T> ExecuteScalar<T>(string connectionString, string sqlQuery);
        Task ExceuteNonQuery(string connectionString, string sqlQuery, bool isStoredProcedure = false, int commandTimeout = 20);
    }
}
