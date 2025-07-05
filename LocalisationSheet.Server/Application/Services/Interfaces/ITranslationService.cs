using LocalisationSheet.Server.Application.DTOs;

namespace LocalisationSheet.Server.Services.Interfaces
{
    public interface ITranslationService
    {
        Task<IEnumerable<TranslationTableDto>> GetTranslationsAsync(int page, int pageSize, string? filterKey, string? filterLanguage);

        Task UpsertAsync(UpsertTranslationDto dto);

        Task DeleteAsync(Guid keyId, Guid languageId);
    }
}