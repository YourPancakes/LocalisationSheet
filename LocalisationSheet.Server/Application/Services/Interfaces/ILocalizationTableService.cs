using LocalisationSheet.Server.Application.DTOs;

namespace LocalisationSheet.Server.Application.Services.Interfaces
{
    public interface ILocalizationTableService
    {
        Task<IEnumerable<TranslationTableDto>> GetTableAsync(int page, int pageSize, string? filterKey, string? filterLanguage);
    }
}
