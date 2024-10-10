using InTouch.SettingService.HubRegistration.Repository;
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
        private readonly ISettingsRepository _settingsRepository;
        private readonly ILogger<TaskRepository> _logger;

        public TaskRepository(
            ISettingsRepository settingsRepository,
            ILogger<TaskRepository> logger) : base(logger, settingsRepository)
        {
            _settingsRepository = settingsRepository;
            _logger = logger;
        }

        public async Task<Guid> PostAsync(TaskModel model, Guid columnId)
        {
            try
            {
                var sql = "SELECT * FROM public.create_task(@_name, @_description, @_enddate, @_author, @_executors, @_associatedwith, @_columnid)";
                var param = new
                {
                    _name = model.Name,
                    _description = model.Description,
                    _enddate = model.EndDate,
                    _author = model.Author,
                    _executors = model.Executors,
                    _columnid = columnId,
                    _associatedwith = model.AssociatedWith,
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

        public async IAsyncEnumerable<TaskModel> GetRelatedTasks(Guid taskId)
        {
            var sql = "SELECT * FROM public.get_related_tasks(@_id)";
            var param = new
            {
                _id = taskId
            };
            var relatedTasks = QueryAsync<TaskModel>(sql, param);

            await foreach (var task in relatedTasks)
            {
                yield return task;
            }
        }

        public async IAsyncEnumerable<TaskModel> GetAllAsync()
        {
            var sql = "SELECT * FROM public.get_all_tasks()";
            var tasks =  QueryAsync<TaskModel>(sql);
            
            await foreach (var task in tasks)
            {
                task.RelatedTasks = GetRelatedTasks(task.Id);
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
                task.RelatedTasks = GetRelatedTasks(task.Id);
                yield return task;
            }
        }

        public async Task UpdateAsync(TaskUpdateModel model, Guid taskId)
        {
            try
            {
                var sql =
                    "CALL public.update_task(@_columnid, @_name, @_description, @_executors, @_enddate, @_taskid)";
                var param = new
                {
                    _columnid = model.ColumnId,
                    _name = model.Name,
                    _description = model.Description,
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
    }
}