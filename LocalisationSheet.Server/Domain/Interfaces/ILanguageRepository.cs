using LocalisationSheet.Server.Domain.Entities;

namespace LocalisationSheet.Server.Domain.Interfaces
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Language?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default);

        Task AddAsync(Language language, CancellationToken cancellationToken = default);

        Task RemoveAsync(Language language, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(string code, CancellationToken cancellationToken = default);

        Task UpdateAsync(Language language, CancellationToken cancellationToken = default);
    }
}