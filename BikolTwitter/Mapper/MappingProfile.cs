using AutoMapper;
using BikolTwitter.Entities;

namespace BikolTwitter.Mapper;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		ApplyMappings();
	}

    private void ApplyMappings()
    {
        CreateMap<CreateBikolSubDto, BikolSub>();
        CreateMap<BikolSub, BikolSubDto>();
    }
}
