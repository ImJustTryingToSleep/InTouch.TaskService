using InTouch.TaskService.Common.Entities.TaskBoards;
using InTouch.TaskService.Common.Entities.TaskBoards.BoardUpdateModels;
using InTouch.TaskService.Common.Entities.TaskBoards.InputModels;

namespace InTouch.TaskService.BLL.Logic.Contracts;

public interface IBoardLogic
{
    Task CreateAsync(BoardInputModel model);
    Task<BoardModel> GetTaskBoardAsync(Guid id);
    Task UpdateAsync(Guid boardId, BoardUpdateModel model);
    Task DeleteAsync(Guid boardId);
}