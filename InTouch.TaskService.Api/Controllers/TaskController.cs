using System.ComponentModel.DataAnnotations;
using InTouch.Authorization.Authz;
using InTouch.Authorization.Permissions;
using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities.TaskModels.Db;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using InTouch.TaskService.Common.Entities.TaskModels.UpdateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InTouch.TaskService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    // [HasPermission([PermissionEnum.user, PermissionEnum.admin, PermissionEnum.sysadmin])]
    public class TaskController : ControllerBase
    {
        private readonly ITaskLogic _taskLogic;

        public TaskController(ITaskLogic taskLogic)
        {
            _taskLogic = taskLogic;
        }
        
        [HttpPost]
        [Route("createTask")]
        public async Task PostAsync([FromBody] TaskInputModel model, Guid columnId, Guid associatedWith)
        {
            await _taskLogic.PostAsync(model, columnId, associatedWith);
        }
        
        [HttpGet("getTask")]
        public async Task<TaskModel> GetAsync([Required] Guid taskId)
        {
            return await _taskLogic.GetByIdAsync(taskId);
        }
        
        [HttpGet("getAll")]
        public async IAsyncEnumerable<TaskModel> GetAsync()
        {
            await foreach (var task in _taskLogic.GetAllAsync())
            {
                yield return task;
            }
        }

        [HttpPut]
        [Route("updateTask")]
        public async Task Put(Guid taskId, [FromBody] TaskUpdateModel model)
        {
            await _taskLogic.UpdateAsync(model, taskId);
        }

        
        [HttpDelete]
        [Route("deleteTask")]
        public async Task Delete(Guid taskId)
        {
            await _taskLogic.DeleteAsync(taskId);
        }
    }
}
