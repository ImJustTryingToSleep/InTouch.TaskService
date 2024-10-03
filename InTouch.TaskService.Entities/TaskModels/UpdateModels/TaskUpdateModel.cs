using InTouch.TaskService.Common.Entities.Enums;

namespace InTouch.TaskService.Common.Entities.TaskModels.UpdateModels;

public class TaskUpdateModel
{
    public Guid ColumnId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TaskTypes Type { get; set; }
    public TaskStatuses Status { get; set; }
    public DateTime EndDate { get; set; }
    public Guid[] Executors { get; set; }
}