using AutoMapper;
using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.Extensions.Logging;
using InTouch.TaskService.Common.Entities.TaskModels.UpdateModels;

namespace InTouch.TaskService.BLL.Logic
{
    public class TaskLogic : ITaskLogic
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly ISubTaskLogic _subTaskLogic;
        // private readonly INotificationLogic _notification;
        // private readonly ISettingsRepository _settingsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskLogic> _logger;

        public TaskLogic(
            ITaskRepository taskRepository,
            ISubTaskRepository subTaskRepository,
            // INotificationLogic notification,
            // ISettingsRepository settingsRepository,
            IMapper mapper,
            ILogger<TaskLogic> logger, ISubTaskLogic subTaskLogic)
        {
            _taskRepository = taskRepository;
            _subTaskRepository = subTaskRepository;
            // _notification = notification;
            // _settingsRepository = settingsRepository;
            _mapper = mapper;
            _logger = logger;
            _subTaskLogic = subTaskLogic;
        }

        public async Task PostAsync(TaskInputModel model, Guid columnId)
        {
            try
            {
                var task = _mapper.Map<TaskModel>(model);
                var taskid = await _taskRepository.PostAsync(task, columnId);
                _logger.LogDebug($"Posting task {taskid}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка во время выполнения PostAsync");
                throw;
            }
        }

        public async Task<TaskModel> GetByIdAsync(Guid taskId)
        {
            try
            {
                var task = await _taskRepository.GetAsync(taskId);

                task.SubTasks = _subTaskRepository.GetSubTasksAsync(taskId);

                return task;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Нет вопроса с таким Id");
                throw;
            }
        }

        public async IAsyncEnumerable<TaskModel> GetAllAsync()
        {
            var tasks = _taskRepository.GetAllAsync();
            await foreach (var task in tasks)
            {
                task.SubTasks = _subTaskLogic.GetSubTasksByIdAsync(task.Id);
                yield return task;
            }
        }

        public async IAsyncEnumerable<TaskModel> GetAllAsync(Guid columnId)
        {
            var tasks = _taskRepository.GetByColumnAsync(columnId);

            await foreach (var task in tasks)
            {
                task.SubTasks = _subTaskLogic.GetSubTasksByIdAsync(task.Id);
                yield return task;
            }
        }
        
        public async Task UpdateAsync(TaskUpdateModel model, Guid taskId)
        {
            try
            {
                var task = await _taskRepository.GetAsync(taskId);
                
                // if (!task.Executors.SequenceEqual(model.Executors))
                // {
                //     var newUsers = model.Executors.Except(task.Executors);
                //     
                //     //var options = await _settingsRepository.GetAsync<NotificationServiceSettings>();
                //     
                //     foreach (var user in newUsers)
                //     {
                //         var emailMsg = new NotificationServiceMessage
                //         {
                //             EmailFrom = "yandex1.ru", 
                //             EmailTo = "valentin.lushnikov.98@mail.ru",
                //             MessageBody = $"{DateTime.Now} Вы приступили к задаче {task.Name}"
                //         };
                //         
                //         await _notification.SendAsync(emailMsg);
                //     }
                // }
                    
                await _taskRepository.UpdateAsync(model, taskId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ошибка при обновлении задачи");
                throw;
            }
        }

        public async Task DeleteAsync(Guid taskId)
        {
            try
            {
                await _taskRepository.DeleteAsync(taskId);
                _logger.LogDebug($"Задача {taskId} была удалена");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении");
                throw;
            }
        }

        public async Task<TaskDTO> GetTaskAsync(Guid taskId)
        {
           return await _taskRepository.GetTaskAsync(taskId);
        }
    }
}
