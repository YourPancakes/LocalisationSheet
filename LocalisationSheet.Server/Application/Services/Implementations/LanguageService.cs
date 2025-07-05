using AutoMapper;
using LocalisationSheet.Server.Application.DTOs;
using LocalisationSheet.Server.Domain.Interfaces;
using LocalisationSheet.Server.Services.Interfaces;

namespace LocalisationSheet.Server.Services.Implementations
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly IMapper _mapper;

        public LanguageService(ILanguageRepository languageRepository, IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LanguageDto>> GetAllAsync()
        {
            var langs = await _languageRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LanguageDto>>(langs);
        }

        public async Task<LanguageDto?> GetByIdAsync(Guid id)
        {
            var lang = await _languageRepository.GetByIdAsync(id);
            return lang is null ? null : _mapper.Map<LanguageDto>(lang);
        }

        public async Task<IEnumerable<AvailableLanguageDto>> GetAvailableLanguagesAsync()
        {
            var allLanguages = new[] {
            new AvailableLanguageDto { Code = "ru", Name = "Русский" },
            new AvailableLanguageDto { Code = "en", Name = "English" },
            new AvailableLanguageDto { Code = "tr", Name = "Türkçe" }
        };

            var existingLanguages = await _languageRepository.GetAllAsync();
            var existingCodes = existingLanguages.Select(l => l.Code).ToList();

            var available = allLanguages.Where(l => !existingCodes.Contains(l.Code));
            return available;
        }

        public async Task<LanguageDto> CreateAsync(CreateLanguageDto dto)
        {
            var entity = _mapper.Map<Domain.Entities.Language>(dto);
            await _languageRepository.AddAsync(entity);
            return _mapper.Map<LanguageDto>(entity);
        }

        public async Task UpdateAsync(Guid id, UpdateLanguageDto dto)
        {
            var existing = await _languageRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Language {id} not found.");
            _mapper.Map(dto, existing);
            await _languageRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await _languageRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Language {id} not found.");
            await _languageRepository.RemoveAsync(existing);
        }
    }
}