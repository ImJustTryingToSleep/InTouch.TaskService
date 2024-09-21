using InTouch.TaskService.Common.Entities.TaskModels.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTouch.TaskService.Common.Entities.TaskBoards
{
    public class TaskBoardModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<TaskModel> ToDoTasks { get; set; }         //
        public List<TaskModel> InProgressTasks { get; set; }   // Другая модель
        public List<TaskModel> DoneTasks { get; set; }         //

        public List<string> Users { get; set; }
    }
}
