using InTouch.TaskService.BLL.Logic.Contracts;
using InTouch.TaskService.Common.Entities.TaskBoards;
using InTouch.TaskService.Common.Entities.TaskBoards.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace InTouch.TaskService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
