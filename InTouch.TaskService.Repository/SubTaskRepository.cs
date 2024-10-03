using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InTouch.TaskService.DAL.Repository;

public class SubTaskRepository : BaseRepository, ISubTaskRepository
{
    private readonly ILogger<SubTaskRepository> _logger;

    public SubTaskRepository(
        ILogger<SubTaskRepository> logger, 
        IConfiguration configuration) : base(logger, configuration)
    {
        _logger = logger;
    }

    public async Task CreateAsync(TaskModel model, Guid mainTaskId)
    {
        try
        {
            var sql = "CALL public.post_subtask(@_name, @_description, @_enddate, @_author, @_executors, @_basedon)";
            var param = new
            {
                _name = model.Name,
                _description = model.Description,
                _enddate = model.EndDate,
                _author = model.Author,
                _executors = model.Executors,
                _basedon = mainTaskId
            };
            
            await ExecuteAsync(sql, param);

            _logger.LogInformation("Task was created");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Ошибка при создании подзадачи");
            throw;
        }
    }

    public async IAsyncEnumerable<TaskModel> GetSubTasksAsync(Guid mainTaskId)
    {
        var sql = "SELECT * FROM public.getall_subtasks(@_mainTaskId)";
        var param = new
        {
            _mainTaskId = mainTaskId
        };
        var tasks =  QueryAsync<TaskModel>(sql, param);
            
        await foreach (var task in tasks)
        {
            yield return task;
        }
    }
}