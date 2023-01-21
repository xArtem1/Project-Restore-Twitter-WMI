using AutoMapper;
using BikolTwitter.Dtos;
using BikolTwitter.Entities;
using Tweetinvi.Models;

namespace BikolTwitter.Mapper;

/// <inheritdoc/>
public class MappingProfile : Profile
{
    /// <inheritdoc/>
	public MappingProfile()
	{
		ApplyMappings();
	}

    private void ApplyMappings()
    {
        CreateMap<CreateBikolSubDto, BikolSub>();
        CreateMap<BikolSub, BikolSubDto>();
        CreateMap<ITweet, BikolSubTweet>();
        CreateMap<BikolSubTweet, TweetDto>();
    }
}
