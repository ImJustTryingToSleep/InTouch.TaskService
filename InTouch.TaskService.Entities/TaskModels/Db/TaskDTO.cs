namespace InTouch.TaskService.Common.Entities.TaskModels.Db;

public class TaskDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public Guid Author { get; set; }
    public Guid[] Executors { get; set; }
    public IAsyncEnumerable<TaskModel> SubTasks { get; set; }
}