using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using AutoMapper;
using InTouch.TaskService.Common.Entities.TaskModels.Db;

namespace InTouch.TaskService.Api.Config
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<TaskInputModel, TaskModel>();
        }
    }
}
