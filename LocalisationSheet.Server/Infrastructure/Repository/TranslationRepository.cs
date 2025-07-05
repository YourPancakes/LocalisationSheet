using LocalisationSheet.Server.Domain.Entities;
using LocalisationSheet.Server.Domain.Interfaces;
using LocalisationSheet.Server.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalisationSheet.Server.Infrastructure.Repository
{
    public class TranslationRepository : ITranslationRepository
    {
        private readonly LocalizationDbContext _localizationDbContext;

        public TranslationRepository(LocalizationDbContext localizationDbContext)
        {
            _localizationDbContext = localizationDbContext;
        }

        public async Task<IEnumerable<Translation>> GetPagedAsync(
            int pageNumber, int pageSize,
            string? filterKey = null,
            string? filterLanguage = null,
            CancellationToken cancellationToken = default)
        {
            var keysQuery = _localizationDbContext.Keys
                .Include(k => k.Translations)
                    .ThenInclude(t => t.Language)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filterKey))
                keysQuery = keysQuery.Where(k => k.Name.ToLower().Contains(filterKey.ToLower()));

            var keys = await keysQuery
                .OrderBy(k => k.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var result = new List<Translation>();
            foreach (var key in keys)
            {
                if (key.Translations == null || key.Translations.Count == 0)
                {
                    result.Add(new Translation
                    {
                        KeyId = key.Id,
                        Key = key,
                        LanguageId = Guid.Empty,
                        Language = null,
                        Value = null
                    });
                }
                else
                {
                    foreach (var t in key.Translations)
                    {
                        if (t.Language != null && (string.IsNullOrWhiteSpace(filterLanguage) || t.Language.Code.ToLower().Contains(filterLanguage.ToLower())))
                        {
                            result.Add(t);
                        }
                    }
                }
            }

            return result;
        }

        public async Task<Translation?> GetAsync(Guid keyId, Guid languageId, CancellationToken cancellationToken = default)
            => await _localizationDbContext.Translations.FindAsync(keyId, languageId, cancellationToken);

        public async Task AddOrUpdateAsync(Translation translation, CancellationToken cancellationToken = default)
        {
            var existing = await _localizationDbContext.Translations
                .FindAsync(new object[] { translation.KeyId, translation.LanguageId }, cancellationToken);
            if (existing is null)
                await _localizationDbContext.Translations.AddAsync(translation, cancellationToken);
            else
            {
                _localizationDbContext.Entry(existing)
                    .CurrentValues
                    .SetValues(translation);
            }
            await _localizationDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(Translation translation, CancellationToken cancellationToken = default)
        {
            _localizationDbContext.Translations.Remove(translation);
            await _localizationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}