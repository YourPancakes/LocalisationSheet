using LocalisationSheet.Server.Application.DTOs;
using LocalisationSheet.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LocalisationSheet.Server.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/keys")]
    public class KeysController : ControllerBase
    {
        private readonly IKeyService _keyService;

        public KeysController(IKeyService keyService)
        {
            _keyService = keyService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KeyDto>>> Get() =>
            Ok(await _keyService.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<KeyDto>> GetById(Guid id)
        {
            var dto = await _keyService.GetByIdAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<KeyDto>> Create([FromBody] CreateKeyDto dto)
        {
            var created = await _keyService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id, version = "1.0" }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateKeyDto dto)
        {
            await _keyService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _keyService.DeleteAsync(id);
            return NoContent();
        }
    }
}