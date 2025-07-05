using AutoMapper;
using FluentAssertions;
using LocalisationSheet.Server.Application.DTOs;
using LocalisationSheet.Server.Domain.Entities;
using LocalisationSheet.Server.Domain.Interfaces;
using LocalisationSheet.Server.Mappings;
using LocalisationSheet.Server.Services.Implementations;
using Moq;

namespace LocalisationSheet.Server.Tests.Services
{
    public class TranslationServiceTests
    {
        private readonly Mock<ITranslationRepository> _repo;
        private readonly IMapper _mapper;
        private readonly TranslationService _service;

        public TranslationServiceTests()
        {
            _repo = new Mock<ITranslationRepository>();
            var cfg = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = cfg.CreateMapper();
            _service = new TranslationService(_repo.Object, _mapper);
        }

        [Fact]
        public async Task GetTranslationsAsync_ReturnsGroupedTranslationTables()
        {
            var key = new Key { Id = Guid.NewGuid(), Name = "HomeTitle" };
            var lang1 = new Language { Id = Guid.NewGuid(), Code = "en", Name = "English" };
            var lang2 = new Language { Id = Guid.NewGuid(), Code = "fr", Name = "French" };
            var translations = new List<Translation>
            {
                new Translation { Key = key, KeyId = key.Id, Language = lang1, LanguageId = lang1.Id, Value = "Hello" },
                new Translation { Key = key, KeyId = key.Id, Language = lang2, LanguageId = lang2.Id, Value = "Bonjour" }
            };
            _repo.Setup(r => r.GetPagedAsync(
                    1, 10, null, null, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(translations);

            var result = await _service.GetTranslationsAsync(1, 10, null, null);

            result.Should().HaveCount(1);
            var table = result.First();
            table.KeyName.Should().Be("HomeTitle");
            table.Translations.Should().Contain(x => x.Code == "en" && x.Value == "Hello");
            table.Translations.Should().Contain(x => x.Code == "fr" && x.Value == "Bonjour");
        }

        [Fact]
        public async Task UpsertAsync_ValidDto_MapsAndCallsRepository()
        {
            var dto = new UpsertTranslationDto(Guid.NewGuid(), Guid.NewGuid(), "Test");
            _repo.Setup(r => r.AddOrUpdateAsync(It.IsAny<Translation>(), It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

            await _service.UpsertAsync(dto);

            _repo.Verify(r => r.AddOrUpdateAsync(It.Is<Translation>(t =>
                t.KeyId == dto.KeyId &&
                t.LanguageId == dto.LanguageId &&
                t.Value == dto.Value
            ), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_Existing_DeletesEntity()
        {
            var keyId = Guid.NewGuid();
            var langId = Guid.NewGuid();
            var entity = new Translation { KeyId = keyId, LanguageId = langId, Value = "abc" };
            _repo.Setup(r => r.GetAsync(keyId, langId, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(entity);
            _repo.Setup(r => r.RemoveAsync(entity, It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

            await _service.DeleteAsync(keyId, langId);

            _repo.Verify(r => r.RemoveAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_NotFound_Throws()
        {
            var keyId = Guid.NewGuid();
            var langId = Guid.NewGuid();
            _repo.Setup(r => r.GetAsync(keyId, langId, It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Translation)null!);

            Func<Task> act = () => _service.DeleteAsync(keyId, langId);

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}