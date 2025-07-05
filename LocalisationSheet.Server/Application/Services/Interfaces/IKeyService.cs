using LocalisationSheet.Server.Application.DTOs;

namespace LocalisationSheet.Server.Services.Interfaces
{
    public interface IKeyService
    {
        Task<IEnumerable<KeyDto>> GetAllAsync();

        Task<KeyDto?> GetByIdAsync(Guid id);

        Task<KeyDto> CreateAsync(CreateKeyDto dto);

        Task UpdateAsync(Guid id, UpdateKeyDto dto);

        Task DeleteAsync(Guid id);
    }
}