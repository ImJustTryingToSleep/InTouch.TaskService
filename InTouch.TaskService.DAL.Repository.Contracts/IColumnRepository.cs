using InTouch.TaskService.Common.Entities;
using InTouch.TaskService.Common.Entities.ColumnModels.InputModels;

namespace InTouch.TaskService.DAL.Repository.Contracts;

public interface IColumnRepository
{
    Task CreateColumn(Guid boardId, ColumnInputModel column);
    Task<ColumnModel> GetColumn(Guid columnId);
    IAsyncEnumerable<ColumnModel> GetAllColumns(Guid boardId);
    Task UpdateColumn(Guid columnId, ColumnInputModel column);
    Task DeleteColumn(Guid columnId);
}