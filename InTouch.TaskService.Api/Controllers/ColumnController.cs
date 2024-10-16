using InTouch.Authorization.Authz;
using InTouch.Authorization.Permissions;
using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities;
using InTouch.TaskService.Common.Entities.ColumnModels.InputModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InTouch.TaskService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly IColumnLogic _columnLogic;

        public ColumnController(IColumnLogic columnLogic)
        {
            _columnLogic = columnLogic;
        }

        [Authorize]
        [HasPermission([PermissionEnum.user])]
        [HttpPost]
        [Route("create")]
        public async Task CreateAsync(Guid boardId, [FromBody] ColumnInputModel model)
        {
            await _columnLogic.CreateAsync(boardId, model);
        }
        
        [Authorize]
        [HasPermission([PermissionEnum.user])]
        [HttpGet]
        [Route("get")]
        public async Task<ColumnModel> GetAsync(Guid columnId)
        {
            return await _columnLogic.GetAsync(columnId);
        }
        
        [Authorize]
        [HasPermission([PermissionEnum.user])]
        [HttpGet]
        [Route("getAll")]
        public async IAsyncEnumerable<ColumnModel> GetAllAsync(Guid boardId)
        {
            var columns = _columnLogic.GetAllAsync(boardId);

            await foreach (var column in columns)
            {
                yield return column;
            }
        }

        [Authorize]
        [HasPermission([PermissionEnum.user])]
        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(Guid columnId, [FromBody] ColumnInputModel model)
        {
            await _columnLogic.UpdateAsync(columnId, model);
        }
        
        [Authorize]
        [HasPermission([PermissionEnum.user])]
        [HttpPost]
        [Route("delete")]
        public async Task DeleteAsync(Guid columnId)
        {
            await _columnLogic.DeleteAsync(columnId);
        }
    }
}
