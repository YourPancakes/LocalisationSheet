using LocalisationSheet.Server.Application.DTOs;

namespace LocalisationSheet.Server.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<IEnumerable<LanguageDto>> GetAllAsync();

        Task<LanguageDto?> GetByIdAsync(Guid id);

        Task<IEnumerable<AvailableLanguageDto>> GetAvailableLanguagesAsync();

        Task<LanguageDto> CreateAsync(CreateLanguageDto dto);

        Task UpdateAsync(Guid id, UpdateLanguageDto dto);

        Task DeleteAsync(Guid id);
    }
}