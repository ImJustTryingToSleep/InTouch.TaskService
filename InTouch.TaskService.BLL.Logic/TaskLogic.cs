using AutoMapper;
using InTouch.Notification.Entities;
using InTouch.Notification.Notification;
using InTouch.SettingService.HubRegistration.Repository;
using InTouch.SettingsServiceTasks;
using InTouch.TaskService.BLL.Logic.Contracts;
using UserServiceClientGrpcApp;
using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.Extensions.Logging;
using InTouch.TaskService.Common.Entities.TaskModels.UpdateModels;
using static UserServiceClientGrpcApp.UserServiceGrpc;

namespace InTouch.TaskService.BLL.Logic
{
    public class TaskLogic : ITaskLogic
    {
        private readonly ITaskRepository _taskRepository;
        private readonly INotificationLogic _notification;
        private readonly ISettingsRepository _settingsRepository;
        private readonly UserServiceGrpcClient _userService;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskLogic> _logger;

        public TaskLogic(
            ITaskRepository taskRepository,
            INotificationLogic notification,
            ISettingsRepository settingsRepository,
            UserServiceGrpcClient userService,
            IMapper mapper,
            ILogger<TaskLogic> logger)
        {
            _taskRepository = taskRepository;
            _notification = notification;
            _settingsRepository = settingsRepository;
            _userService = userService;
            _mapper = mapper;
            _logger = logger;
        }
        
        /// <summary>
        /// Сreating a Task
        /// </summary>
        /// <param name="model"></param>
        /// <param name="columnId"></param>
        /// <param name="associatedWith"></param>
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
        /// <summary>
        /// Getting a task by id
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Getting all tasks
        /// </summary>
        /// <returns></returns>
        public async IAsyncEnumerable<TaskModel> GetAllAsync()
        {
            var tasks = _taskRepository.GetAllAsync();
            await foreach (var task in tasks)
            {
                yield return task;
            }
        }
        
        /// <summary>
        /// Getting all tasks by column id
        /// </summary>
        /// <param name="columnId"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<TaskModel> GetAllAsync(Guid columnId)
        {
            var tasks = _taskRepository.GetByColumnAsync(columnId);

            await foreach (var task in tasks)
            {
                yield return task;
            }
        }
        
        /// <summary>
        /// Updating a task
        /// </summary>
        /// <param name="model"></param>
        /// <param name="taskId"></param>
        public async Task UpdateAsync(TaskUpdateModel model, Guid taskId)
        {
            try
            {
                var task = await _taskRepository.GetAsync(taskId);
                
                if (task.Executors is null)
                {
                    await JoinJobMailAsync(model.Executors, model.Name);
                }
                else
                {
                    var newUsers = model.Executors.Except(task.Executors);
                    await JoinJobMailAsync(newUsers, model.Name);
                }
                
                await _taskRepository.UpdateAsync(model, taskId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ошибка при обновлении задачи");
                throw;
            }
        }

        /// <summary>
        /// Deleting a task
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="userId"></param>
        public async Task DeleteAsync(Guid taskId, Guid userId)
        {
            try
            {
                var task = await _taskRepository.GetAsync(taskId);
                
                if (task.Author != userId)
                {
                    throw new Exception("\nInsufficient rights to delete task");
                }
                
                await _taskRepository.DeleteAsync(taskId);
                _logger.LogInformation($"Task {taskId} was deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting the task");
                throw;
            }
        }
        
        /// <summary>
        /// Sending email to new executors
        /// </summary>
        /// <param name="executors"></param>
        /// <param name="taskName"></param>
        private async Task JoinJobMailAsync(IEnumerable<Guid> executors, string taskName)
        {
            try
            {
                var options = await _settingsRepository.GetAsync<TaskServiceSettings>();
            
                foreach (var user in executors)
                {
                    var userToMail = new UserServiceRequest()
                    {
                        UserId = user.ToString()
                    };
                
                    var emailTo = _userService.Send(userToMail);
                
                    var emailMsg = new NotificationServiceMessage
                    {
                        EmailFrom = options.ConnectionStrings.EmailFrom, 
                        EmailTo = emailTo.Email,                                     
                        MessageBody = $"{DateTime.Now} Вы приступили к задаче {taskName}"
                    };
        
                    await _notification.SendAsync(emailMsg, options.Kafka.Topic);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while running JoinJobMail");
                throw;
            }
        }
    }
}
