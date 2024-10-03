using InTouch.TaskService.Common.Entities.TaskModels.Db;

namespace InTouch.TaskService.DAL.Repository.Contracts;

public interface ISubTaskRepository
{
    Task CreateAsync(TaskModel model, Guid mainTaskId);
    IAsyncEnumerable<TaskModel> GetSubTasksAsync(Guid mainTaskId);
}