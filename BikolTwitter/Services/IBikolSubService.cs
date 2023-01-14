namespace BikolTwitter.Services;

public interface IBikolSubService
{
    Task<BikolSubDto> CreateAsync(CreateBikolSubDto dto);
    Task<IEnumerable<BikolSubDto>> GetAllAsync();
    Task DeleteAsync(Guid id);
}
