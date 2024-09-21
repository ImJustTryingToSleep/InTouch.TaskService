using InTouch.TaskService.Common.Entities.Enums;

namespace InTouch.TaskService.Common.Entities.TaskModels.Db
{
    public class TaskModel
    {
        public Guid TaskId { get; set; }                // Поменять в БД и тут
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public DateTime EndDate { get; set; }

        public Statuses Status { get; set; }

        public string Author { get; set; }
        public string Executor { get; set; }
        public List<string> Team { get; set; }



    }
}
