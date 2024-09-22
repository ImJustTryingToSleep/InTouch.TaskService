using InTouch.TaskService.Common.Entities.TaskModels.InputModels;

namespace InTouch.TaskService.BLL.Logic.Contracts;

public interface ISubTaskLogic
{
    Task CreateAsync(TaskInputModel model, Guid mainTaskId);
}