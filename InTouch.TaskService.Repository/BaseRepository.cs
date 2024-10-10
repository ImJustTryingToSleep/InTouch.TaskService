using Dapper;
using InTouch.SettingService.HubRegistration.Repository;
using InTouch.TaskService.Common.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace InTouch.TaskService.DAL.Repository
{
    public abstract class BaseRepository
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly ILogger<BaseRepository> _logger;

        public BaseRepository(
            ILogger<BaseRepository> logger,
            ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
            _logger = logger;
        }

        public async Task ExecuteAsync(string sql, object param = null)
        {
            try
            {
                using var connection = await GetConnection();
                await connection.QueryAsync(sql, param);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при работе ExecuteAsync");
                throw;
            }
        }

        public async IAsyncEnumerable<T> QueryAsync<T>(string sql, object param = null)
        {
            using var connection = await GetConnection();
            var reader = await connection.ExecuteReaderAsync(sql, param);
            var rowParser = reader.GetRowParser<T>();

            while (await reader.ReadAsync())
            {
                yield return rowParser(reader);
            }
        }

        public async Task<T?> QuerySingleAsync<T>(string sql, object param = null)
        {
            try
            {
                using var connection = await GetConnection();
                return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка во время работы QuerySingleAsync");
                throw;
            }

        }

        protected async Task<NpgsqlConnection> GetConnection()
        {
            try
            {
                var connectionString = await _settingsRepository.GetAsync<TaskServiceSettings>();
                return new NpgsqlConnection(connectionString.ConnectionStrings.PostgreSQL);
            }
            catch (Exception ex)
            {
                _logger.LogError("Ошибка при подключении к БД");
                throw;
            }
        }
    }
}
