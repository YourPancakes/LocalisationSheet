using LocalisationSheet.Server.Application.DTOs;
using LocalisationSheet.Server.Application.Services.Interfaces;
using LocalisationSheet.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LocalisationSheet.Server.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/translations")]
    public class TranslationsController : ControllerBase
    {
        private readonly ITranslationService _translationService;
        private readonly ILocalizationTableService _localizationTableService;

        public TranslationsController(ITranslationService translationService, ILocalizationTableService localizationTableService)
        {
            _translationService = translationService;
            _localizationTableService = localizationTableService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TranslationTableDto>>> Get(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] string? filterKey = null,
            [FromQuery] string? filterLanguage = null)
        {
            var result = await _localizationTableService.GetTableAsync(page, pageSize, filterKey, filterLanguage);
            return Ok(result);
        }

        [HttpPut("{keyId:guid}/{languageId:guid}")]
        public async Task<IActionResult> Upsert(Guid keyId, Guid languageId, [FromBody] UpsertTranslationDto dto)
        {
            if (dto.KeyId != keyId || dto.LanguageId != languageId)
                return BadRequest();

            await _translationService.UpsertAsync(dto);
            return NoContent();
        }

        [HttpDelete("{keyId:guid}/{languageId:guid}")]
        public async Task<IActionResult> Delete(Guid keyId, Guid languageId)
        {
            await _translationService.DeleteAsync(keyId, languageId);
            return NoContent();
        }
    }
}