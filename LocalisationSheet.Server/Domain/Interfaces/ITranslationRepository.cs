using LocalisationSheet.Server.Domain.Entities;

namespace LocalisationSheet.Server.Domain.Interfaces
{
    public interface ITranslationRepository
    {
        Task<IEnumerable<Translation>> GetPagedAsync(
            int pageNumber, int pageSize,
            string? filterKey = null,
            string? filterLanguage = null,
            CancellationToken cancellationToken = default);

        Task<Translation?> GetAsync(Guid keyId, Guid languageId, CancellationToken cancellationToken = default);

        Task AddOrUpdateAsync(Translation translation, CancellationToken cancellationToken = default);

        Task RemoveAsync(Translation translation, CancellationToken cancellationToken = default);
    }
}