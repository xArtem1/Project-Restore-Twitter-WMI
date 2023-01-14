namespace BikolTwitter.Services;

public interface IBikolSubService
{
    Task<BikolSubDto> CreateAsync(CreateBikolSubDto dto);
}
