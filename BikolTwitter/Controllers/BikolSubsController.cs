using BikolTwitter.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace BikolTwitter.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BikolSubsController : ControllerBase
{
    private readonly IBikolSubService _service;

    public BikolSubsController(IBikolSubService service)
	{
		_service = service;
	}

	[HttpPost]
	public async Task<ActionResult<BikolSubDto>> Create([FromBody] CreateBikolSubDto dto)
	{
		var bikolSub = await _service.CreateAsync(dto);
		return Created($"api/bikolsubs/{bikolSub.Id}", bikolSub);
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<BikolSubDto>>> GetAll()
	{
		var bikolSubs = await _service.GetAllAsync();
		return Ok(bikolSubs);
	}
}
