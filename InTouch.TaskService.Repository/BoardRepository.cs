using InTouch.TaskService.Common.Entities.TaskBoards;
using InTouch.TaskService.Common.Entities.TaskBoards.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InTouch.TaskService.DAL.Repository;

public class BoardRepository : BaseRepository, IBoardRepository
{
    private readonly ILogger<BoardRepository> _logger;
    private readonly IConfiguration _configuration;
    
    public BoardRepository(
        ILogger<BoardRepository> logger, 
        IConfiguration configuration) : base(logger, configuration)
    {
        _logger = logger;
        _configuration = configuration;
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

    public async Task<BoardModel> GetTaskBoardAsync(Guid id)
    {
        var sql = "SELECT * FROM public.get_board(@_id)";
        var param = new
        {
            _id = id
        };
        
        return await QuerySingleAsync<BoardModel>(sql, param);
    }
}