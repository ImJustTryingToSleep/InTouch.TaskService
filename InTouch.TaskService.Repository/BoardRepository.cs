using InTouch.SettingService.HubRegistration.Repository;
using InTouch.TaskService.Common.Entities.TaskBoards;
using InTouch.TaskService.Common.Entities.TaskBoards.BoardUpdateModels;
using InTouch.TaskService.Common.Entities.TaskBoards.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InTouch.TaskService.DAL.Repository;

public class BoardRepository : BaseRepository, IBoardRepository
{
    private readonly ISettingsRepository _settingsRepository;
    private readonly ILogger<BoardRepository> _logger;
    
    public BoardRepository(
        ISettingsRepository settingsRepository,
        ILogger<BoardRepository> logger) : base(logger, settingsRepository)
    {
        _settingsRepository = settingsRepository;
        _logger = logger;
    }

    public async Task CreateBoard(BoardInputModel model)
    {
        try
        {
            var sql = "CALL post_board(@_name, @_author)";
            var param = new
            {
                _name = model.Name,
                _author = model.Author
            };
            
            await ExecuteAsync(sql, param);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при добавлении доски в БД");
            throw;
        }
    }

    public async Task<BoardModel> GetTaskBoardAsync(Guid boardId)
    {
        var sql = "SELECT * FROM public.get_board(@_id)";
        var param = new
        {
            _id = boardId
        };
        
        return await QuerySingleAsync<BoardModel>(sql, param);
    }

    public async Task UpdateBoardAsync(Guid boardId, BoardUpdateModel model)
    {
        try
        {
            var sql = "CALL update_board(@_id, @_name)";
            var param = new
            {
                _id = boardId,
                _name = model.Name
            };
            
            await ExecuteAsync(sql, param);
            _logger.LogDebug($"Board {boardId} was updated");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, "Произошла ошибка при работе UpdateBoardAsync");
            throw;
        }
    }

    public async Task DeleteBoardAsync(Guid boardId)
    {
        try
        {
            var sql = "CALL delete_board(@_id)";
            var param = new
            {
                _id = boardId
            };
             
            await ExecuteAsync(sql, param);
            _logger.LogDebug($"Board {boardId} was deleted");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка во время работы DeleteBoardAsync");
            throw;
        }
    }
    
}