using InTouch.Authorization.Authz;
using InTouch.Authorization.Permissions;
using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities;
using InTouch.TaskService.Common.Entities.ColumnModels.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InTouch.TaskService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    // [HasPermission([PermissionEnum.user, PermissionEnum.admin, PermissionEnum.sysadmin])]
    public class ColumnController : ControllerBase
    {
        private readonly IColumnLogic _columnLogic;

        public ColumnController(IColumnLogic columnLogic)
        {
            _columnLogic = columnLogic;
        }

        [HttpPost]
        [Route("create")]
        public async Task CreateAsync(Guid boardId, [FromBody] ColumnInputModel model)
        {
            await _columnLogic.CreateAsync(boardId, model);
        }
        
        [HttpGet]
        [Route("get")]
        public async Task<ColumnModel> GetAsync(Guid columnId)
        {
            return await _columnLogic.GetAsync(columnId);
        }
        
        
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

        [HttpPut]
        [Route("update")]
        public async Task UpdateAsync(Guid columnId, [FromBody] ColumnInputModel model)
        {
            await _columnLogic.UpdateAsync(columnId, model);
        }
        [HttpPost]
        [Route("delete")]
        public async Task DeleteAsync(Guid columnId)
        {
            await _columnLogic.DeleteAsync(columnId);
        }
    }
}
