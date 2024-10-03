using AutoMapper;
using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using InTouch.TaskService.Common.Entities.TaskModels.UpdateModels;

namespace InTouch.TaskService.BLL.Logic.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<TaskInputModel, TaskModel>(); 
            //CreateMap<TaskInputModel, SubTaskModel>();
            CreateMap<TaskUpdateModel, TaskModel>();
        }
    }
}
