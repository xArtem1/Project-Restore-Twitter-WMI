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
        CreateMap<ITweet, BikolSubTweet>()
            .ConstructUsing((t, ctx) => new()
            {
                CreatedAt = t.CreatedAt,
                Text = t.Text,
                FullText = t.FullText,
                Prefix = t.Prefix,
                Suffix = t.Suffix,
                FavoriteCount = t.FavoriteCount,
                CreatedBy = t.CreatedBy.Name
            });
        CreateMap<BikolSubTweet, TweetDto>();
    }
}
