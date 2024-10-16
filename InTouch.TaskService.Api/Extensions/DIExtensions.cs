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
            services.AddScoped<IBoardRepository, BoardRepository>();
            services.AddScoped<IColumnRepository, ColumnRepository>();
            return services;
        }

        public static IServiceCollection ConfigureBLLDependecies(this IServiceCollection services)
        {
            services.AddScoped<ITaskLogic, TaskLogic>();
            services.AddScoped<IBoardLogic, BoardLogic>();
            services.AddScoped<IColumnLogic, ColumnLogic>();
            
            return services;
        }
        
        public static IServiceCollection ConfigureHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient();

            return services;
        }

    }
}
