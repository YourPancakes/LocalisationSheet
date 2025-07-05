using AutoMapper;
using LocalisationSheet.Server.Mappings;

namespace Tests.Unit.Mapping
{
    public class MappingFixture
    {
        public IMapper Mapper { get; }

        public MappingFixture()
        {
            var cfg = new MapperConfiguration(c => c.AddProfile<MappingProfile>());
            Mapper = cfg.CreateMapper();
        }
    }
}