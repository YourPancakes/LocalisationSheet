using LocalisationSheet.Server.Domain.Entities;
using LocalisationSheet.Server.Domain.Interfaces;
using LocalisationSheet.Server.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalisationSheet.Server.Infrastructure.Repository
{
    public class KeyRepository : IKeyRepository
    {
        private readonly LocalizationDbContext _keyDbContext;

        public KeyRepository(LocalizationDbContext keyRepository)
        {
            _keyDbContext = keyRepository;
        }

        public async Task<IEnumerable<Key>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _keyDbContext.Keys.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<Key?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
            => await _keyDbContext.Keys.FindAsync(id, cancellationToken);

        public async Task<IEnumerable<Key>> GetPagedWithTranslationsAsync(int page, int pageSize, string? filterKey = null, CancellationToken cancellationToken = default)
        {
            var query = _keyDbContext.Keys
                .Include(k => k.Translations)
                    .ThenInclude(t => t.Language)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterKey))
                query = query.Where(k => k.Name.Contains(filterKey));

            return await query
                .OrderBy(k => k.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Key key, CancellationToken cancellationToken = default)
        {
            _keyDbContext.Keys.Add(key);
            await _keyDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(Key key, CancellationToken cancellationToken = default)
        {
            _keyDbContext.Keys.Remove(key);
            await _keyDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
           => await _keyDbContext.Keys.AnyAsync(k => k.Name == name, cancellationToken);

        public async Task UpdateAsync(Key key, CancellationToken cancellationToken = default)
        {
            var existingKey = await _keyDbContext.Keys.FindAsync(new object[] { key.Id }, cancellationToken);

            if (existingKey == null)
                throw new KeyNotFoundException($"Key with ID '{key.Id}' not found.");

            _keyDbContext.Entry(existingKey).CurrentValues.SetValues(key);
            await _keyDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}