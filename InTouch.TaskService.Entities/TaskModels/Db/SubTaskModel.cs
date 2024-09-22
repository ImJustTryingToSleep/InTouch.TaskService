namespace InTouch.TaskService.Common.Entities.TaskModels.Db;

public class SubTaskModel : TaskModel
{
    public Guid BasedOn {get; set;}
}