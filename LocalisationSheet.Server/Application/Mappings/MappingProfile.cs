using AutoMapper;
using LocalisationSheet.Server.Application.DTOs;
using LocalisationSheet.Server.Domain.Entities;

namespace LocalisationSheet.Server.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Language, LanguageDto>();
            CreateMap<CreateLanguageDto, Language>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
            CreateMap<UpdateLanguageDto, Language>();

            CreateMap<Key, KeyDto>();
            CreateMap<CreateKeyDto, Key>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
            CreateMap<UpdateKeyDto, Key>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Translation, TranslationDto>();
            CreateMap<UpsertTranslationDto, Translation>();
        }
    }
}