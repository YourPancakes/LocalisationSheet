using LocalisationSheet.Server.Domain.Entities;

namespace LocalisationSheet.Server.Domain.Interfaces
{
    public interface IKeyRepository
    {
        Task<IEnumerable<Key>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<Key?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<Key>> GetPagedWithTranslationsAsync(int page, int pageSize, string? filterKey = null, CancellationToken cancellationToken = default);

        Task AddAsync(Key key, CancellationToken cancellationToken = default);

        Task RemoveAsync(Key key, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);

        Task UpdateAsync(Key key, CancellationToken cancellationToken = default);

    }
}