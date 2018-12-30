using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AutoPay.Dtos.Batch;
using AutoPay.Dtos.RemoteDbConfig;
using AutoPay.Infrastructure.DataLayer;
using AutoPay.Infrastructure.Managers;
using AutoPay.ViewModels.RemoteDbConfig;

namespace AutoPay.Managers
{
    public class RemoteDbManager : IRemoteDbManager
    {
        private readonly IDapperRepository _remoteDbRepository;

        public RemoteDbManager(IDapperRepository remoteDbRepository)
        {
            _remoteDbRepository = remoteDbRepository;
        }

        public async Task<string> ValdateDatabaseDetailAsync(RemoteDbUpsertVm model)
        {
            var conString = GetConnectionString(model.Server, model.Username, model.Password, model.Database);

            using (var sqlConnection = new SqlConnection(conString))
            {
                try
                {
                    await sqlConnection.OpenAsync();

                    if (await _remoteDbRepository.ExecuteScalar<int>(conString,
                            $"SELECT COUNT(*) FROM sys.procedures WHERE [name] ='{model.UpdateDueDetailSp}'") == 0)
                        throw new Exception($"Stored procedure ({model.UpdateDueDetailSp}) does not exists in the remote database.");

                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                return string.Empty;
            }
        }

        public async Task<IEnumerable<BatchCustomerDto>> GetCurrentChargesAsync(RemoteDbConfigDto dbConfig, string sqlQuery)
        {
            return await _remoteDbRepository.ExecuteReader<BatchCustomerDto>(GetConnectionString(dbConfig), sqlQuery);
        }

        public async Task UpdateCurrentChargesDetailAsync(RemoteDbConfigDto dbConfig)
        {
            await _remoteDbRepository.ExceuteNonQuery(GetConnectionString(dbConfig), dbConfig.UpdateDueDetailSp, true, 1200);
        }

        public async Task<IEnumerable<BatchCustomerDueDetailDto>> GetCurrentChargesDetailAsync(RemoteDbConfigDto dbConfig)
        {
            return await _remoteDbRepository.ExecuteReader<BatchCustomerDueDetailDto>(GetConnectionString(dbConfig), dbConfig.GetDueDetailQuery);
        }

        private static string GetConnectionString(RemoteDbConfigDto dbConfig)
        {
            return GetConnectionString(dbConfig.Server, dbConfig.Username, dbConfig.Password, dbConfig.Database);
        }

        private static string GetConnectionString(string server, string username, string password, string database)
        {
            return $@"Data Source={server};Initial Catalog={database};User Id={username};Password={password};";
        }

    }
}
