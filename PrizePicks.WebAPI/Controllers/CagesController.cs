using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PrizePicks.API.Models;
using PrizePicks.API.Services;

namespace PrizePicks.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CagesController : ControllerBase
{
    private readonly ILogger<CagesController> _logger;
    private readonly ICageService _cageService;

    public CagesController(ILogger<CagesController> logger, ICageService cageService)
    {
        _logger = logger;
        _cageService = cageService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    public async Task<ActionResult<IEnumerable<ICage>>> GetAllAsync()
    {
        _logger.LogInformation($"Attempting to get all Cages from repo");
        var cages = await _cageService.CagesAsync();

        return Ok(cages);
    }

    [HttpGet]
    [Route("{cageId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ICage>> GetSingleAsync(int cageId)
    {
        _logger.LogInformation($"Attempting to Cages for id {cageId}");

        try
        {
            var cage = await _cageService.CageAsync(cageId);

            return Ok(cage);
        }
        catch (KeyNotFoundException knfException)
        {
            _logger.LogError(knfException.Message);
            return NotFound($"No Cage found for provided key {cageId}");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cage))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cage>> CreateAsync(Cage cage)
    {
        _logger.LogInformation("Attempting to create a new Cages");

        try
        {
            var updatedCage = await _cageService.Create(cage);

            return Ok(updatedCage);
        }
        catch (InvalidOperationException ioException)
        {
            _logger.LogError(ioException.Message);
            return BadRequest($"Cage canot be created unless it is powered on");
        }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ICage>> UpdateAsync(Cage cage)
    {
        _logger.LogInformation($"Attempting to update a Cage with id {cage.Id}");

        try
        {
            var updatedCage = await _cageService.UpdateAsync(cage);

            return Ok(updatedCage);
        }
        catch (KeyNotFoundException knfException)
        {
            _logger.LogError(knfException.Message);        
            return NotFound($"No Cage found for provided key {cage.Id}");
        }
        catch (InvalidOperationException ioException)
        {
            _logger.LogError(ioException.Message);
            return BadRequest(
                $"Cage canot be updated unless it is powered on.  use /poweroff route to powerdown the cage"
            );
        }
    }

    [HttpPut]
    [Route("{cageId}/powerdown")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ICage>> PowerDownAsync(int cageId)
    {
        _logger.LogInformation($"Attempting to powerdown Cage with id {cageId}");

        try
        {
            await _cageService.UpdatePowerStatus(cageId, PowerStatusType.Down);
        }
        catch (InvalidOperationException ioExcetion)
        {
            return BadRequest("Cannot power down Cage as it would be put in an invalid state");
        }

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
