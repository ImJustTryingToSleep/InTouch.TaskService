using InTouch.TaskService.Common.Entities;
using InTouch.TaskService.Common.Entities.ColumnModels.InputModels;

namespace InTouch.TaskService.BLL.Logic.Contracts;

public interface IColumnLogic
{
    Task CreateAsync(Guid boardId, ColumnInputModel column);
    Task<ColumnModel> GetAsync(Guid columnId);
    IAsyncEnumerable<ColumnModel> GetAllAsync(Guid boardId);
    Task UpdateAsync(Guid columnId, ColumnInputModel column);
    Task DeleteAsync(Guid columnId);
}