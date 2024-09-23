using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities.TaskModels.InputModels;
using InTouch.TaskService.DAL.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace InTouch.TaskService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubTaskController : ControllerBase
    {
        private readonly ISubTaskLogic _subTaskLogic;

        public SubTaskController(ISubTaskLogic subTaskLogic)
        {
            _subTaskLogic = subTaskLogic;
        }

        
        [HttpPost]
        [Route("createSubtask")]
        public async Task PostAsync([FromBody] TaskInputModel model, Guid mainTaskId)
        {
            await _subTaskLogic.CreateAsync(model, mainTaskId);
        }
    }
}
