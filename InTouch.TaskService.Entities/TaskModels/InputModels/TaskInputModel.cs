using InTouch.TaskService.Common.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InTouch.TaskService.Common.Entities.TaskModels.InputModels
{
    public class TaskInputModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
    }
}
