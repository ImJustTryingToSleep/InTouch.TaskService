﻿using AutoMapper;
using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using InTouch.TaskService.DAL.Repository;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InTouch.TaskService.Common.Entities.TaskModels.UpdateModels;

namespace InTouch.TaskService.BLL.Logic
{
    public class TaskLogic : ITaskLogic
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ISubTaskRepository _subTaskRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TaskLogic> _logger;

        public TaskLogic(
            ITaskRepository taskRepository,
            ISubTaskRepository subTaskRepository,
            IMapper mapper,
            ILogger<TaskLogic> logger)
        {
            _taskRepository = taskRepository;
            _subTaskRepository = subTaskRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task PostAsync(TaskInputModel model)
        {
            try
            {
                var task = _mapper.Map<TaskModel>(model);
                await _taskRepository.PostAsync(task);
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
                task.SubTasks = _subTaskRepository.GetSubTasksAsync(task.Id);
                yield return task;
            }
        }

        public async Task UpdateAsync(TaskUpdateModel model, Guid taskId)
        {
            try
            {
                var task = await _taskRepository.GetAsync(taskId);

                if (model.EndDate > task.EndDate)
                {
                    throw new ArgumentException("Дата окончания подзадачи не может быть больше основной задачи");
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
                _logger.LogDebug($"Задача {taskId} была удалена");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при удалении");
                throw;
            }
        }
    }
}
