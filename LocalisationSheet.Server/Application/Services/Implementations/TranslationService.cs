using AutoMapper;
using LocalisationSheet.Server.Application.DTOs;
using LocalisationSheet.Server.Domain.Interfaces;
using LocalisationSheet.Server.Services.Interfaces;

namespace LocalisationSheet.Server.Services.Implementations
{
    public class TranslationService : ITranslationService
    {
        private readonly ITranslationRepository _translationRepository;
        private readonly IMapper _mapper;

        public TranslationService(ITranslationRepository translationRepository, IMapper mapper)
        {
            _translationRepository = translationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TranslationTableDto>> GetTranslationsAsync(int page, int pageSize, string? filterKey, string? filterLanguage)
        {
            var flat = await _translationRepository.GetPagedAsync(page, pageSize, filterKey, filterLanguage);
            return flat
                .GroupBy(t => t.Key.Name)
                .Select(g => new TranslationTableDto
                {
                    KeyName = g.Key,
                    Translations = g.Select(x => new TranslationItemDto
                    {
                        Code = x.Language.Code,
                        Value = x.Value
                    }).ToList()
                });
        }

        public async Task UpsertAsync(UpsertTranslationDto dto)
        {
            var entity = _mapper.Map<Domain.Entities.Translation>(dto);
            await _translationRepository.AddOrUpdateAsync(entity);
        }

        public async Task DeleteAsync(Guid keyId, Guid languageId)
        {
            var existing = await _translationRepository.GetAsync(keyId, languageId);
            if (existing is null)
                throw new KeyNotFoundException($"Translation ({keyId},{languageId}) not found.");
            await _translationRepository.RemoveAsync(existing);
        }
    }
}