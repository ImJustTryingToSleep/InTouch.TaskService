using InTouch.TaskService.Common.Entities.TaskModels.Db;

namespace InTouch.TaskService.Common.Entities;

public class ColumnModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IAsyncEnumerable<TaskModel> Tasks { get; set; }
}