using LocalisationSheet.Server.Application.DTOs;
using LocalisationSheet.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LocalisationSheet.Server.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/languages")]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageDto>>> Get() =>
            Ok(await _languageService.GetAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<LanguageDto>> GetById(Guid id)
        {
            var dto = await _languageService.GetByIdAsync(id);
            return dto is null ? NotFound() : Ok(dto);
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<LanguageDto>>> GetAvailable() =>
            Ok(await _languageService.GetAvailableLanguagesAsync());

        [HttpPost]
        public async Task<ActionResult<LanguageDto>> Create([FromBody] CreateLanguageDto dto)
        {
            var created = await _languageService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id, version = "1.0" }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLanguageDto dto)
        {
            await _languageService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _languageService.DeleteAsync(id);
            return NoContent();
        }
    }
}