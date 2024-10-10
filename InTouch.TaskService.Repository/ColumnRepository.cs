using InTouch.SettingService.HubRegistration.Repository;
using InTouch.TaskService.Common.Entities;
using InTouch.TaskService.Common.Entities.ColumnModels.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql.Replication.PgOutput.Messages;

namespace InTouch.TaskService.DAL.Repository;

public class ColumnRepository : BaseRepository, IColumnRepository
{
    private readonly ISettingsRepository _settingsRepository;
    private readonly ILogger<BaseRepository> _logger;
    public ColumnRepository(
        ISettingsRepository settingsRepository,
        ILogger<BaseRepository> logger) : base(logger,settingsRepository)
    {
        _logger = logger;
        _settingsRepository = settingsRepository;
    }


    public async Task CreateColumn(Guid boardId, ColumnInputModel column)
    {
        try
        {
            var sql = "CALL public.post_column(@_name, @_boardid)";
            var param = new
            {
                _name = column.Name,
                _boardid = boardId
            };

            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при добавлении колонки в БД");
            throw;
        }
    }

    public async Task<ColumnModel> GetColumn(Guid columnId)
    {
        try
        {
            var sql = "SELECT *  FROM public.get_column(@_id)";
            var param = new
            {
                _id = columnId
            };

            return await QuerySingleAsync<ColumnModel>(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении колонки из БД");
            throw;
        }
    }

    public async IAsyncEnumerable<ColumnModel> GetAllColumns(Guid boardId)
    {
        
        var sql = "SELECT * FROM public.get_columns(@_id)";
        var param = new
        {
            _id = boardId
        };
        
        var columns = QueryAsync<ColumnModel>(sql, param);

        if (columns is null)
        {
            throw new ArgumentNullException($"В доске с таким Id {boardId} нет колонок");
        }
        
        await foreach (var column in columns)
        {
            yield return column;
        }
    }

    public async Task UpdateColumn(Guid columnId, ColumnInputModel column)
    {
        try
        {
            var sql = "CALL public.update_column(@_name, @_columnId)";
            var param = new
            {
                _name = column.Name,
                _columnid = columnId
            };
        
            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении в БД");
            throw;
        }
    }


    public async Task DeleteColumn(Guid columnId)
    {
        try
        {
            var sql = "CALL public.delete_column(@_id)";
            var param = new
            {
                _id = columnId
            };
            
            await ExecuteAsync(sql, param);
            
            _logger.LogDebug($"Column {columnId} has been deleted.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Delete Column {columnId} failed.");
            throw;
        }
    }
}