using AutoMapper;
using BikolTwitter.Database;
using BikolTwitter.Entities;
using BikolTwitter.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BikolTwitter.Services;

public class BikolSubService : IBikolSubService
{
    private readonly BikolTwitterDbContext _dbContext;
    private readonly IMapper _mapper;

    public BikolSubService(BikolTwitterDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<BikolSubDto> CreateAsync(CreateBikolSubDto dto)
    {
        var bikolSub = _mapper.Map<BikolSub>(dto);
        await _dbContext.BikolSubs.AddAsync(bikolSub);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<BikolSubDto>(bikolSub);
    }

    public async Task<IEnumerable<BikolSubDto>> GetAllAsync()
    {
        var allBikolSubs = await _dbContext.BikolSubs.ToListAsync();
        return _mapper.Map<IEnumerable<BikolSubDto>>(allBikolSubs);
    }

    public async Task DeleteAsync(Guid id)
    {
        var bikolSub = await _dbContext.BikolSubs.FirstOrDefaultAsync(s => s.Id == id);

        if (bikolSub is null) 
        {
            throw new NotFoundException();
        }

        _dbContext.BikolSubs.Remove(bikolSub);
        await _dbContext.SaveChangesAsync();
    }
}
