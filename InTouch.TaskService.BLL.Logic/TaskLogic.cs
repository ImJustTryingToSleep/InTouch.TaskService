using AutoMapper;
using InTouch.Notification.Entities;
using InTouch.Notification.Notification;
using InTouch.SettingService.HubRegistration.Repository;
using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities;
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
        private readonly INotificationLogic _notification;
        private readonly ISettingsRepository _settingsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskLogic> _logger;

        public TaskLogic(
            ITaskRepository taskRepository,
            INotificationLogic notification,
            ISettingsRepository settingsRepository,
            IMapper mapper,
            ILogger<TaskLogic> logger)
        {
            _taskRepository = taskRepository;
            _notification = notification;
            _settingsRepository = settingsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task PostAsync(TaskInputModel model, Guid columnId, Guid associatedWith)
        {
            try
            {
                var task = _mapper.Map<TaskModel>(model);
                task.AssociatedWith = associatedWith;
                
                var taskid = await _taskRepository.PostAsync(task, columnId);
                _logger.LogInformation($"Posting task {taskid}");
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

                task.RelatedTasks = _taskRepository.GetRelatedTasks(taskId);

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
                yield return task;
            }
        }

        public async IAsyncEnumerable<TaskModel> GetAllAsync(Guid columnId)
        {
            var tasks = _taskRepository.GetByColumnAsync(columnId);

            await foreach (var task in tasks)
            {
                yield return task;
            }
        }
        
        public async Task UpdateAsync(TaskUpdateModel model, Guid taskId)
        {
            try
            {
                var task = await _taskRepository.GetAsync(taskId);
                
                if (task.Executors is null)
                {
                    await JoinJobMail(model.Executors, model.Name);
                }
                else
                {
                    var newUsers = model.Executors.Except(task.Executors);
                    await JoinJobMail(newUsers, model.Name);
                }
                
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
                _logger.LogInformation($"Задача {taskId} была удалена");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении");
                throw;
            }
        }
        
        private async Task JoinJobMail(IEnumerable<Guid> executors, string taskName)
        {
            var options = await _settingsRepository.GetAsync<TaskServiceSettings>();
            foreach (var user in executors)
            {
                var emailMsg = new NotificationServiceMessage
                {
                    EmailFrom = options.ConnectionStrings.EmailFrom, 
                    EmailTo = "valentin.lushnikov.98@mail.ru",                                     // Тут надо получить майлы пользователей из юзерсервиса(из бд, gRPC?)
                    MessageBody = $"{DateTime.Now} Вы приступили к задаче {taskName}"
                };
        
                await _notification.SendAsync(emailMsg, options.Kafka.Topic);
            }
        }
    }
}
