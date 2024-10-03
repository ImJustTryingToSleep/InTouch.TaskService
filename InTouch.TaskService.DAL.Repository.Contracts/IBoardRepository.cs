using InTouch.TaskService.Common.Entities.TaskBoards;
using InTouch.TaskService.Common.Entities.TaskBoards.BoardUpdateModels;
using InTouch.TaskService.Common.Entities.TaskBoards.InputModels;

namespace InTouch.TaskService.DAL.Repository.Contracts;

public interface IBoardRepository
{
    Task CreateBoard(BoardInputModel model);
    Task<BoardModel> GetTaskBoardAsync(Guid boardId);
    Task UpdateBoardAsync(Guid boardId, BoardUpdateModel model);
    Task DeleteBoardAsync(Guid boardId);

}