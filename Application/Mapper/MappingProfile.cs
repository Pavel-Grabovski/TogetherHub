using Application.Dtos;
using AutoMapper;

namespace Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Topic, TopicResponseDto>()
            .ForCtorParam(nameof(TopicResponseDto.Id), 
                options => options.MapFrom(source => source.Id.Value));

        CreateMap<Location, LocationResponseDto>();
    }
}

