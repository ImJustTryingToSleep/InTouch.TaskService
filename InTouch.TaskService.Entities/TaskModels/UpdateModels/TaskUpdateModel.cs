namespace InTouch.TaskService.Common.Entities.TaskModels.UpdateModels;

public class TaskUpdateModel
{
    public Guid ColumnId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime EndDate { get; set; }
    public Guid[] Executors { get; set; }
}