using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities.TaskBoards;
using InTouch.TaskService.Common.Entities.TaskBoards.BoardUpdateModels;
using InTouch.TaskService.Common.Entities.TaskBoards.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.Extensions.Logging;

namespace InTouch.TaskService.BLL.Logic;

public class BoardLogic : IBoardLogic
{
    private readonly IBoardRepository _boardRepository;
    private readonly ILogger<BoardLogic> _logger;
    
    private readonly IColumnLogic _columnLogic;

    public BoardLogic(
        IBoardRepository boardRepository,
        ILogger<BoardLogic> logger, 
        IColumnLogic columnLogic)
    {
        _boardRepository = boardRepository;
        _logger = logger;
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

    public async Task<BoardModel> GetTaskBoardAsync(Guid boardId)
    {
        try
        {
            var taskBoard = await _boardRepository.GetTaskBoardAsync(boardId);
            taskBoard.Columns = _columnLogic.GetAllAsync(boardId);

            return taskBoard;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateAsync(Guid boardId, BoardUpdateModel model)
    {
        try
        {
            await _boardRepository.UpdateBoardAsync(boardId, model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,"Ошибка во время работы UpdateAsync");
            throw;
        }
    }

    public async Task DeleteAsync(Guid boardId)
    {
        try
        {
            await _boardRepository.DeleteBoardAsync(boardId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Ошибка во время работы DeleteAsync");
            throw;
        }
    }
}