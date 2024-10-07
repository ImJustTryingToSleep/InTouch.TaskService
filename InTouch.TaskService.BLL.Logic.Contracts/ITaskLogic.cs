using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using InTouch.TaskService.Common.Entities.TaskModels.UpdateModels;

namespace InTouch.TaskService.BLL.Logic.Contracts
{
    public interface ITaskLogic
    {
        Task PostAsync(TaskInputModel model, Guid columnId, Guid associatedWith);
        Task<TaskModel> GetByIdAsync(Guid taskId);
        IAsyncEnumerable<TaskModel> GetAllAsync(Guid columnId);
        Task UpdateAsync(TaskUpdateModel model, Guid taskId);
        Task DeleteAsync(Guid taskId);
        IAsyncEnumerable<TaskModel> GetAllAsync();
    }
}
