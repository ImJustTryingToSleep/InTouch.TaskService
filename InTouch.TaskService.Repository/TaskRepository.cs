using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using InTouch.TaskService.Common.Entities.TaskModels.UpdateModels;
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

        public async Task<Guid> PostAsync(TaskModel model, Guid columnId)
        {
            try
            {
                var sql = "SELECT * FROM public.create_task(@_name, @_description, @_enddate, @_author, @_executors, @_columnid)";
                var param = new
                {
                    _name = model.Name,
                    _description = model.Description,
                    _enddate = model.EndDate,
                    _author = model.Author,
                    _executors = model.Executors,
                    _columnid = columnId
                };

                return await QuerySingleAsync<Guid>(sql, param);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Ошибка при добавлении задачи");
                throw;
            }
        }
        
        public async Task<TaskModel> GetAsync(Guid id)
        {
            try
            {
                var sql = "SELECT * FROM public.get_task(@_id)";
                var param = new
                {
                    _id = id
                };

                return await QuerySingleAsync<TaskModel>(sql, param);
            }
            catch (Exception ex)
            {
                _logger.LogError($"There no Task with this id {id}");
                throw;
            }
        }

        public async IAsyncEnumerable<TaskModel> GetAllAsync()
        {
            var sql = "SELECT * FROM public.get_all_tasks()";
            var tasks =  QueryAsync<TaskModel>(sql);
            
            await foreach (var task in tasks)
            {
                yield return task;
            }
        }

        public async IAsyncEnumerable<TaskModel> GetByColumnAsync(Guid id)
        {
            var sql = "SELECT * FROM public.gettasks_bycolumn(@_id)";
            var param = new
            {
                _id = id
            };
            
            var tasks = QueryAsync<TaskModel>(sql, param);

            await foreach (var task in tasks)
            {
                yield return task;
            }
        }

        public async Task UpdateAsync(TaskUpdateModel model, Guid taskId)
        {
            try
            {
                var sql =
                    "CALL public.update_task(@_columnid, @_name, @_description, @_type, @_status, @_executors, @_enddate, @_taskid)";
                var param = new
                {
                    _columnid = model.ColumnId,
                    _name = model.Name,
                    _description = model.Description,
                    _type = model.Type,
                    _status = model.Status,
                    _executors = model.Executors,
                    _enddate = model.EndDate.ToLocalTime(),
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
                var sql = "CALL public.delete_task(@_id)";
                var param = new
                {
                    _id = taskId
                };
                
                await ExecuteAsync(sql, param);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"There no Task with this id {taskId}");
                throw;
            }
        }

        #region Test

        public async Task<TaskDTO> GetTaskAsync(Guid taskId)
        {
            var sql = "SELECT * FROM public.get_task(@_id)";
            var task =  await QuerySingleAsync<TaskDTO>(sql, new { _id = taskId });
            return task;
        }

        #endregion
       
    }
}