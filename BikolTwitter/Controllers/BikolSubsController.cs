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

    /// <summary>
    /// Adds twitter user with specyfied username to bikolsubs.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST api/bikolsubs
    /// {
    ///		"username" : "@someusername"
    /// }
    /// </remarks>
    /// <response code="400">When username is null, empty, does not start with '@' or already exists in database</response>
    /// <response code="201">When bikolsub has succesfully saved in dataabse</response>
    [HttpPost]
	[ProducesResponseType(201)]
	[ProducesResponseType(400)]
	[Produces("application/json")]
	public async Task<ActionResult<BikolSubDto>> Create([FromBody] CreateBikolSubDto dto)
	{
		var bikolSub = await _service.CreateAsync(dto);
		return Created($"api/bikolsubs/{bikolSub.Id}", bikolSub);
	}

    /// <summary>
    /// Gets all bikolsubs.
    /// </summary>
    /// <response code="200">Bikolsubs have been returned successfully.</response>
    [HttpGet]
	[ProducesResponseType(200)]
	[Produces("application/json")]
	public async Task<ActionResult<IEnumerable<BikolSubDto>>> GetAll()
	{
		var bikolSubs = await _service.GetAllAsync();
		return Ok(bikolSubs);
	}

    /// <summary>
    /// Deletes bikolsub from database
    /// </summary>
    /// <response code="204">When twitter user has successfully been deleted from bikolsubs</response>
    /// <response code="404">When twitter user with id provided in route has not been found among bikolsubs</response>
    /// <param name="id">Id of bikolsub to be deleted</param>
    [HttpDelete("{id}")]
	[ProducesResponseType(204)]
	[ProducesResponseType(404)]
	public async Task<ActionResult> Delete([FromRoute] int id)
	{
		await _service.DeleteAsync(id);
		return NoContent();
	}
}
