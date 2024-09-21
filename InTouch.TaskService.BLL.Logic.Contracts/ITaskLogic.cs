using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;

namespace InTouch.TaskService.BLL.Logic.Contracts
{
    public interface ITaskLogic
    {
        Task PostAsync(TaskInputModel model);
        Task<TaskModel> GetByIdAsync(Guid taskId);
        Task UpdateAsync(TaskInputModel model, Guid taskId);
        Task DeleteAsync(Guid taskId);
        IAsyncEnumerable<TaskModel> GetAllAsync();
    }
}
