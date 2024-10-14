using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities;
using InTouch.TaskService.Common.Entities.ColumnModels.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.Extensions.Logging;

namespace InTouch.TaskService.BLL.Logic;

public class ColumnLogic : IColumnLogic
{
    private readonly IColumnRepository _columnRepository;
    private readonly ITaskLogic _taskLogic;
    private readonly ILogger<ColumnLogic> _logger;

    public ColumnLogic(
        IColumnRepository columnRepository,
        ILogger<ColumnLogic> logger, 
        ITaskLogic taskLogic)
    {
        _columnRepository = columnRepository;
        _logger = logger;
        _taskLogic = taskLogic;
    }


    /// <summary>
    /// Creating a column
    /// </summary>
    /// <param name="boardId"></param>
    /// <param name="column"></param>
    public async Task CreateAsync(Guid boardId, ColumnInputModel column)
    {
        try
        {
            await _columnRepository.CreateColumn(boardId, column);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating column");
            throw;
        }
    }

    /// <summary>
    /// Getting a column by id
    /// </summary>
    /// <param name="columnId"></param>
    /// <returns></returns>
    public async Task<ColumnModel> GetAsync(Guid columnId)
    {
        try
        {
            var column = await _columnRepository.GetColumn(columnId);
            column.Tasks = _taskLogic.GetAllAsync(column.Id);
        
            return column;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error when getting column in GetAsync");
            throw;
        }
    }
    
    /// <summary>
    /// Getting all column by board id
    /// </summary>
    /// <param name="boardId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async IAsyncEnumerable<ColumnModel> GetAllAsync(Guid boardId)
    {
        var columns = _columnRepository.GetAllColumns(boardId);

        if (columns is null)
        {
            throw new ArgumentNullException("Error when getting columns in GetAllAsync");
        }
        
        // Filling the column with tasks
        await foreach (var column in columns)
        {
            column.Tasks = _taskLogic.GetAllAsync(column.Id);
            yield return column;
        }
    }

    /// <summary>
    /// Updating a column
    /// </summary>
    /// <param name="columnId"></param>
    /// <param name="column"></param>
    public async Task UpdateAsync(Guid columnId, ColumnInputModel column)
    {
        try
        {
            await _columnRepository.UpdateColumn(columnId, column);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка в Update");
            throw;
        }
    }

    /// <summary>
    /// Deleting a column
    /// </summary>
    /// <param name="columnId"></param>
    public async Task DeleteAsync(Guid columnId)
    {
        try
        {
            await _columnRepository.DeleteColumn(columnId);               // Добавить проверку на удаление колонки
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении колонки");
            throw;
        }
    }
}