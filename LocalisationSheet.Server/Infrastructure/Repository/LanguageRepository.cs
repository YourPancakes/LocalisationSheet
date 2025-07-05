using LocalisationSheet.Server.Domain.Entities;
using LocalisationSheet.Server.Domain.Interfaces;
using LocalisationSheet.Server.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LocalisationSheet.Server.Infrastructure.Repository
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly LocalizationDbContext _localizationDbContext;

        public LanguageRepository(LocalizationDbContext localizationDbContext)
        {
            _localizationDbContext = localizationDbContext;
        }

        public async Task<IEnumerable<Language>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _localizationDbContext.Languages.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<Language?> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
            => await _localizationDbContext.Languages.FindAsync(Id, cancellationToken);

        public async Task AddAsync(Language language, CancellationToken cancellationToken = default)
        {
            _localizationDbContext.Languages.Add(language);
            await _localizationDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(Language language, CancellationToken cancellationToken = default)
        {
            _localizationDbContext.Languages.Remove(language);
            await _localizationDbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(string code, CancellationToken cancellationToken = default)
            => await _localizationDbContext.Languages.AnyAsync(c => c.Code == code, cancellationToken);

        public async Task UpdateAsync(Language language, CancellationToken cancellationToken = default)
        {
            var existingLanguage = await _localizationDbContext.Languages.FindAsync(new object[] { language.Id }, cancellationToken);

            if (existingLanguage == null)
                throw new KeyNotFoundException($"Language with ID '{language.Id}' not found.");

            _localizationDbContext.Entry(existingLanguage).CurrentValues.SetValues(language);
            await _localizationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}