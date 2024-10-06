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
        ITaskRepository taskRepository, 
        ILogger<ColumnLogic> logger, 
        ISubTaskRepository subTaskRepository, 
        ISubTaskLogic subTaskLogic, 
        ITaskLogic taskLogic)
    {
        _columnRepository = columnRepository;
        _logger = logger;
        _taskLogic = taskLogic;
    }


    public async Task CreateAsync(Guid boardId, ColumnInputModel column)
    {
        try
        {
            await _columnRepository.CreateColumn(boardId, column);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании колонки");
            throw;
        }
    }

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
            _logger.LogError(ex, "Ошибка при получении колонки в GetAsync");
            throw;
        }
    }

    public async IAsyncEnumerable<ColumnModel> GetAllAsync(Guid boardId)
    {
        var columns = _columnRepository.GetAllColumns(boardId);

        if (columns is null)
        {
            throw new ArgumentNullException("Ошибка при получении колонок в GetAllAsync");
        }
        
        await foreach (var column in columns)
        {
            column.Tasks = _taskLogic.GetAllAsync(column.Id);
            yield return column;
        }
    }

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


    public async Task DeleteAsync(Guid columnId)
    {
        try
        {
            await _columnRepository.DeleteColumn(columnId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении колонки");
            throw;
        }
    }
}