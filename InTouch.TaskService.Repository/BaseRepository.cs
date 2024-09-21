using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace InTouch.TaskService.DAL.Repository
{
    public abstract class BaseRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BaseRepository> _logger;

        public BaseRepository(
            ILogger<BaseRepository> logger,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task ExecuteAsync(string sql, object param = null)
        {
            try
            {
                using var connection = GetConnection();
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
            using var connection = GetConnection();
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
                using var connection = GetConnection();
                return await connection.QueryFirstOrDefaultAsync<T>(sql, param);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка во время работы QuerySingleAsync");
                throw;
            }

        }

        protected NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
