using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InTouch.TaskService.DAL.Repository
{
    public class TaskRepository : BaseRepository, ITaskRepository
    {
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(
            ILogger<TaskRepository> logger, 
            IConfiguration configuration) : base(logger, configuration)
        {
            _logger = logger;
        }

        public async Task PostAsync(TaskModel model)
        {
            try
            {
                var sql = "CALL public.post_task(@_name, @_description, @_type, @_status, @_createdat, @_updateddate, @_enddate, @_author, @_executor)";
                var param = new
                {
                    _name = model.Name,
                    _description = model.Description,
                    _type = model.Type,
                    _status = model.Status,
                    _createdat = model.CreatedDate,
                    _updateddate = model.UpdatedDate,
                    _enddate = model.EndDate,
                    _author = model.Author,
                    _executor = model.Executor
                };

                await ExecuteAsync(sql, param);

                _logger.LogInformation("Task was created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Ошибка при добавлении задачи");
                throw;
            }
        }
        
        public async Task<TaskModel> GetAsync(Guid taskId)
        {
            try
            {
                var sql = "SELECT * FROM public.get_task(@_taskid)";
                var param = new
                {
                    _taskid = taskId
                };

                return await QuerySingleAsync<TaskModel>(sql, param);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There no Task with this id {taskId}");
                throw;
            }
        }

        public async IAsyncEnumerable<TaskModel> GetAllAsync()
        {
            var sql = "SELECT * FROM public.getAll_task()";
            var tasks =  QueryAsync<TaskModel>(sql);
            
            await foreach (var task in tasks)
            {
                yield return task;
            }
        }

        public async Task UpdateAsync(TaskInputModel model, Guid taskId)
        {
            try
            {
                var sql =
                    "CALL public.update_task(@_name, @_description, @_type, @_status, @_executor, @_taskid)";
                var param = new
                {
                    _name = model.Name,
                    _description = model.Description,
                    _type = model.Type,
                    _status = model.Status,
                    _executor = model.Executor,
                    _taskid = taskId
                };
            
                await ExecuteAsync(sql, param);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"There no Task with this id {taskId}");
                throw;
            }
        }

        public async Task DeleteAsync(Guid taskId)
        {
            try
            {
                var sql = "CALL public.delete_task(@_taskid)";
                var param = new
                {
                    _taskid = taskId
                };
                
                await ExecuteAsync(sql, param);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"There no Task with this id {taskId}");
                throw;
            }
        }
    }
}