using System.Text.Json;
using InTouch.Authorization.Authz;
using InTouch.Authorization.Permissions;
using InTouch.SettingService.HubRegistration.Repository;
using InTouch.TaskService.Common.Entities.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InTouch.TaskService.Api.Controllers
{
    [Authorize]
    [HasPermission([PermissionEnum.admin])]
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly ISettingsRepository _settingsRepository;

        public ConfigController(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }
        
        // GET: api/<ConfigController>
        [HttpGet]
        public async Task<ActionResult<TasksServiceSettings>> Get()
        {
            var result = await _settingsRepository.GetAsync<TasksServiceSettings>();

            return Ok(result);
        }

        // POST api/<ConfigController>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TasksServiceSettings settings)
        {
            var collection = JsonSerializer.Serialize(settings);

            await _settingsRepository.CreateAsync<TasksServiceSettings>(collection);

            return Ok();
        }

        // DELETE api/<ConfigController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete()
        {
            await _settingsRepository.DeleteAsync("TasksServiceSettings");

            return Ok();
        }

    }
}
