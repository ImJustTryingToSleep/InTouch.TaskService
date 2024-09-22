using AutoMapper;
using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.Extensions.Logging;

namespace InTouch.TaskService.BLL.Logic;

public class SubTaskLogic : ISubTaskLogic
{
    private readonly ISubTaskRepository _subSubtaskRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<SubTaskLogic> _logger;

    public SubTaskLogic(
        ISubTaskRepository subTaskRepository,
        IMapper mapper,
        ILogger<SubTaskLogic> logger)
    {
        _subSubtaskRepository = subTaskRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task CreateAsync(TaskInputModel model, Guid mainTaskId)
    {
        try
        {
            var task = _mapper.Map<SubTaskModel>(model);
            await _subSubtaskRepository.CreateAsync(task, mainTaskId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Failed to create subtask");
            throw;
        }
    }
}