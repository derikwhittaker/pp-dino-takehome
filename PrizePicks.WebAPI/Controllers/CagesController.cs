using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PrizePicks.API.Models;

namespace PrizePicks.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CagesController : ControllerBase
{
    private readonly ILogger<CagesController> _logger;

    public CagesController(ILogger<CagesController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    public async Task<ActionResult<IEnumerable<ICage>>> GetAllAsync()
    {
        _logger.LogInformation($"Attempting to get all Cages");
        return Enumerable.Range(1, 5).Select(index => new Cage()).ToArray();
    }

    [HttpGet]
    [Route("{cageId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    public async Task<ActionResult<ICage>> GetSingleAsync(int cageId)
    {
        _logger.LogInformation($"Attempting to Cages for id {cageId}");
        return new Cage { Id = cageId };
    }

    // [Route()]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    public async Task<ActionResult<ICage>> CreateAsync()
    {
        _logger.LogInformation("Attempting to create a new Cages");
        return new Cage { Id = 99 };
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    public async Task<ActionResult<ICage>> UpdateAsync()
    {
        _logger.LogInformation("Attempting to update a Cages");
        return new Cage();
    }

    [HttpPut]
    [Route("{cageId}/powerdown")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    public async Task<ActionResult<ICage>> PowerDownAsync(int cageId)
    {
        _logger.LogInformation($"Attempting to powerdown Cage with id {cageId}");
        return new Cage { Id = cageId };
    }

    [HttpPut]
    [Route("{cageId}/powerup")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    public async Task<ActionResult<ICage>> PowerUpAsync(int cageId)
    {
        _logger.LogInformation($"Attempting to powerup Cage with id {cageId}");
        return new Cage { Id = cageId };
    }
}
