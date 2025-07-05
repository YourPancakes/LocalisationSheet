using LocalisationSheet.Server.Application.DTOs;
using LocalisationSheet.Server.Application.Services.Interfaces;
using LocalisationSheet.Server.Domain.Entities;
using LocalisationSheet.Server.Domain.Interfaces;

public class LocalizationTableService : ILocalizationTableService
{
    private readonly IKeyRepository _keyRepository;

    public LocalizationTableService(IKeyRepository keyRepository)
    {
        _keyRepository = keyRepository;
    }

    public async Task<IEnumerable<TranslationTableDto>> GetTableAsync(int page, int pageSize, string? filterKey, string? filterLanguage)
    {
        var keys = await _keyRepository.GetPagedWithTranslationsAsync(page, pageSize, filterKey);

        return keys.Select(k => new TranslationTableDto
        {
            KeyName = k.Name,
            Translations = (k.Translations ?? new List<Translation>())
                .Where(t => t.Language != null && (string.IsNullOrWhiteSpace(filterLanguage) || t.Language.Code.ToLower().Contains(filterLanguage.ToLower())))
                .Select(t => new TranslationItemDto
                {
                    Code = t.Language!.Code,
                    Value = t.Value
                }).ToList()
        });
    }
}