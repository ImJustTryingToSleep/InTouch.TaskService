using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;

namespace InTouch.TaskService.DAL.Repository.Contracts
{
    public interface ITaskRepository
    {
        Task PostAsync(TaskModel model);
        Task<TaskModel> GetAsync(Guid taskId);
        Task UpdateAsync(TaskInputModel model, Guid taskId);
        Task DeleteAsync(Guid taskId);
        IAsyncEnumerable<TaskModel> GetAllAsync();
    }
}
