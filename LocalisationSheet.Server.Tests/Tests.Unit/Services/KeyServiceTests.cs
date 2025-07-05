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
    public class KeyServiceTests
    {
        private readonly Mock<IKeyRepository> _repo;
        private readonly IMapper _mapper;
        private readonly KeyService _service;

        public KeyServiceTests()
        {
            _repo = new Mock<IKeyRepository>();
            var cfg = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = cfg.CreateMapper();
            _service = new KeyService(_repo.Object, _mapper);
        }

        [Fact]
        public async Task CreateAsync_ValidDto_CreatesAndReturnsKeyDto()
        {
            var dto = new CreateKeyDto("test_key");
            var entity = new Key { Id = Guid.NewGuid(), Name = "test_key" };
            _repo.Setup(r => r.AddAsync(It.IsAny<Key>(), It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

            var result = await _service.CreateAsync(dto);

            _repo.Verify(r => r.AddAsync(It.Is<Key>(k => k.Name == "test_key"), It.IsAny<CancellationToken>()), Times.Once);
            result.Name.Should().Be("test_key");
            result.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllKeys()
        {
            var list = new List<Key>
            {
                new Key { Id = Guid.NewGuid(), Name = "A" },
                new Key { Id = Guid.NewGuid(), Name = "B" }
            };
            _repo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(list);

            var result = await _service.GetAllAsync();

            result.Should().HaveCount(2);
            result.Should().ContainSingle(x => x.Name == "A");
            result.Should().ContainSingle(x => x.Name == "B");
        }

        [Fact]
        public async Task GetByIdAsync_Existing_ReturnsKeyDto()
        {
            var id = Guid.NewGuid();
            var entity = new Key { Id = id, Name = "key1" };
            _repo.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(entity);

            var result = await _service.GetByIdAsync(id);

            result.Should().NotBeNull();
            result!.Id.Should().Be(id);
            result.Name.Should().Be("key1");
        }

        [Fact]
        public async Task GetByIdAsync_NotFound_ReturnsNull()
        {
            _repo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Key)null!);

            var result = await _service.GetByIdAsync(Guid.NewGuid());

            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteAsync_Existing_DeletesEntity()
        {
            var id = Guid.NewGuid();
            var entity = new Key { Id = id, Name = "to_delete" };
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
                 .ReturnsAsync((Key)null!);

            Func<Task> act = () => _service.DeleteAsync(id);

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}