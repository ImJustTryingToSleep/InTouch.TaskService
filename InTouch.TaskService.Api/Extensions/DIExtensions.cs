using AutoMapper;
using InTouch.TaskService.Api.Config;
using InTouch.TaskService.BLL.Logic;
using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.DAL.Repository;
using InTouch.TaskService.DAL.Repository.Contracts;

namespace InTouch.TaskService.Api.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection ConfigureDALDependecies(this IServiceCollection services)
        {
            services.AddScoped<ITaskRepository, TaskRepository>();
            return services;
        }

        public static IServiceCollection ConfigureBLLDependecies(this IServiceCollection services)
        {
            services.AddScoped<ITaskLogic, TaskLogic>();
            services.AddAutoMapper(typeof(MapperConfig));
            return services;
        }
    }
}
