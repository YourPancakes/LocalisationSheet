using AutoMapper;
using LocalisationSheet.Server.Application.DTOs;
using LocalisationSheet.Server.Domain.Interfaces;
using LocalisationSheet.Server.Services.Interfaces;

namespace LocalisationSheet.Server.Services.Implementations
{
    public class KeyService : IKeyService
    {
        private readonly IKeyRepository _keyRepository;
        private readonly IMapper _mapper;

        public KeyService(IKeyRepository keyRepository, IMapper mapper)
        {
            _keyRepository = keyRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<KeyDto>> GetAllAsync()
        {
            var keys = await _keyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<KeyDto>>(keys);
        }

        public async Task<KeyDto?> GetByIdAsync(Guid id)
        {
            var key = await _keyRepository.GetByIdAsync(id);
            return key is null ? null : _mapper.Map<KeyDto>(key);
        }

        public async Task<KeyDto> CreateAsync(CreateKeyDto dto)
        {
            var entity = _mapper.Map<Domain.Entities.Key>(dto);
            await _keyRepository.AddAsync(entity);
            return _mapper.Map<KeyDto>(entity);
        }

        public async Task UpdateAsync(Guid id, UpdateKeyDto dto)
        {
            var existing = await _keyRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Key {id} not found.");
            _mapper.Map(dto, existing);
            await _keyRepository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(Guid id)
        {
            var existing = await _keyRepository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Key {id} not found.");
            await _keyRepository.RemoveAsync(existing);
        }
    }
}