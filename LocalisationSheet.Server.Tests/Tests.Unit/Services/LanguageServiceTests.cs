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
    public class LanguageServiceTests
    {
        private readonly Mock<ILanguageRepository> _repo;
        private readonly IMapper _mapper;
        private readonly LanguageService _service;

        public LanguageServiceTests()
        {
            _repo = new Mock<ILanguageRepository>();
            var cfg = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = cfg.CreateMapper();
            _service = new LanguageService(_repo.Object, _mapper);
        }

        [Fact]
        public async Task CreateAsync_ValidDto_AddsAndReturnsLanguageDto()
        {
            var dto = new CreateLanguageDto("en", "English");
            var entity = new Language { Id = Guid.NewGuid(), Code = "en", Name = "English" };
            _repo.Setup(r => r.AddAsync(It.IsAny<Language>(), It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

            var result = await _service.CreateAsync(dto);

            _repo.Verify(r => r.AddAsync(It.Is<Language>(l => l.Code == "en" && l.Name == "English"), It.IsAny<CancellationToken>()), Times.Once);
            result.Code.Should().Be("en");
            result.Name.Should().Be("English");
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllLanguages()
        {
            var list = new List<Language>
            {
                new Language { Id = Guid.NewGuid(), Code = "en", Name = "English" },
                new Language { Id = Guid.NewGuid(), Code = "ru", Name = "Russian" }
            };
            _repo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(list);

            var result = await _service.GetAllAsync();

            result.Should().HaveCount(2);
            result.Should().ContainSingle(l => l.Code == "en" && l.Name == "English");
            result.Should().ContainSingle(l => l.Code == "ru" && l.Name == "Russian");
        }

        [Fact]
        public async Task GetByIdAsync_Existing_ReturnsLanguageDto()
        {
            var id = Guid.NewGuid();
            var entity = new Language { Id = id, Code = "de", Name = "German" };
            _repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(entity);

            var result = await _service.GetByIdAsync(id);

            result.Should().NotBeNull();
            result!.Id.Should().Be(id);
            result.Code.Should().Be("de");
            result.Name.Should().Be("German");
        }

        [Fact]
        public async Task GetByIdAsync_NotFound_ReturnsNull()
        {
            _repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Language)null!);

            var result = await _service.GetByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateAsync_Existing_UpdatesEntity()
        {
            var id = Guid.NewGuid();
            var existing = new Language { Id = id, Code = "en", Name = "English" };
            var dto = new UpdateLanguageDto(id, "fr", "French");
            _repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(existing);
            _repo.Setup(r => r.UpdateAsync(It.IsAny<Language>(), It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

            await _service.UpdateAsync(id, dto);

            _repo.Verify(r => r.UpdateAsync(It.Is<Language>(l => l.Id == id && l.Code == "fr" && l.Name == "French"), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_NotFound_Throws()
        {
            var id = Guid.NewGuid();
            var dto = new UpdateLanguageDto(id, "fr", "French");
            _repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Language)null!);

            Func<Task> act = () => _service.UpdateAsync(id, dto);

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_Existing_DeletesEntity()
        {
            var id = Guid.NewGuid();
            var entity = new Language { Id = id, Code = "de", Name = "German" };
            _repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(entity);
            _repo.Setup(r => r.RemoveAsync(entity, It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

            await _service.DeleteAsync(id);

            _repo.Verify(r => r.RemoveAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_NotFound_Throws()
        {
            var id = Guid.NewGuid();
            _repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Language)null!);

            Func<Task> act = () => _service.DeleteAsync(id);

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}