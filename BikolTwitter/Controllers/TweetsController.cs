using BikolTwitter.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikolTwitter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        private readonly ITweetService _service;

        public TweetsController(ITweetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetAll([FromQuery]DateTimeOffset from)
        {
            var tweets = await _service.GetAllTweetsAsync(from);
            return Ok(tweets);
        }
    }
}
