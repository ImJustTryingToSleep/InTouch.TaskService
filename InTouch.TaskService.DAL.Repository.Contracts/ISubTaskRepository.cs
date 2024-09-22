using InTouch.TaskService.Common.Entities.TaskModels.Db;

namespace InTouch.TaskService.DAL.Repository.Contracts;

public interface ISubTaskRepository
{
    Task CreateAsync(SubTaskModel model, Guid mainTaskId);
    IAsyncEnumerable<SubTaskModel> GetSubTasksAsync(Guid mainTaskId);
}