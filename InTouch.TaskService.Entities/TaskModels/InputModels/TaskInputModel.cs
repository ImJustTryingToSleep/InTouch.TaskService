namespace InTouch.TaskService.Common.Entities.TaskModels.InputModels
{
    public class TaskInputModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
    }
}
