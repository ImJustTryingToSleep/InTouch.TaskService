using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using InTouch.TaskService.Common.Entities.TaskModels.UpdateModels;

namespace InTouch.TaskService.DAL.Repository.Contracts
{
    public interface ITaskRepository
    {
        Task PostAsync(TaskModel model);
        Task<TaskModel> GetAsync(Guid id);
        IAsyncEnumerable<TaskModel> GetAllAsync();
        Task UpdateAsync(TaskUpdateModel model, Guid taskId);
        Task DeleteAsync(Guid taskId);
    }
}
