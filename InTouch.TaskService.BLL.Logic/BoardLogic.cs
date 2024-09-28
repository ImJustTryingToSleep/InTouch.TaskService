using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities.TaskBoards;
using InTouch.TaskService.Common.Entities.TaskBoards.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.Extensions.Logging;

namespace InTouch.TaskService.BLL.Logic;

public class BoardLogic : IBoardLogic
{
    private readonly IBoardRepository _boardRepository;
    private readonly IColumnRepository _columnRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<BoardLogic> _logger;
    
    private readonly IColumnLogic _columnLogic;

    public BoardLogic(
        IBoardRepository boardRepository,
        IColumnRepository columnRepository,
        ILogger<BoardLogic> logger, 
        ITaskRepository taskRepository, IColumnLogic columnLogic)
    {
        _boardRepository = boardRepository;
        _columnRepository = columnRepository;
        _logger = logger;
        _taskRepository = taskRepository;
        _columnLogic = columnLogic;
    }

    public async Task CreateAsync(BoardInputModel model)
    {
        try
        {
            await _boardRepository.CreateBoard(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании доски");
            throw;
        }
    }

    public async Task<BoardModel> GetTaskBoardAsync(Guid id)
    {
        try
        {
            var taskBoard = await _boardRepository.GetTaskBoardAsync(id);

            taskBoard.Columns = _columnLogic.GetAllAsync(id);

            return taskBoard;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}