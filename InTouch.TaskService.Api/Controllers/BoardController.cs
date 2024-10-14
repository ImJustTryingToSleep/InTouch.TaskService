using InTouch.Authorization.Authz;
using InTouch.Authorization.Permissions;
using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities.TaskBoards;
using InTouch.TaskService.Common.Entities.TaskBoards.BoardUpdateModels;
using InTouch.TaskService.Common.Entities.TaskBoards.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace InTouch.TaskService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    // [HasPermission([PermissionEnum.user, PermissionEnum.admin, PermissionEnum.sysadmin])]
    public class BoardController : ControllerBase
    {
        private readonly IBoardLogic _boardLogic;

        public BoardController(IBoardLogic boardLogic)
        {
            _boardLogic = boardLogic;
        }
        
        [HttpPost]
        [Route("createBoard")]
        public async Task PostAsync([FromBody] BoardInputModel model)
        {
            await _boardLogic.CreateAsync(model);
        }
        
        [HttpGet]
        [Route("getBoard")]
        public async Task<BoardModel> GetAsync(Guid id)
        {
            return await _boardLogic.GetTaskBoardAsync(id);
        }
        
        [HttpPut]
        [Route("updateBoard")]
        public async Task UpdateAsync(Guid boardId, [FromBody] BoardUpdateModel model)
        {
            await _boardLogic.UpdateAsync(boardId, model);
        }
        
        [HttpDelete]
        [Route("deleteBoard")]
        public async Task DeleteAsync(Guid boardId)
        {
            await _boardLogic.DeleteAsync(boardId);
        }
        
        
    }
}
