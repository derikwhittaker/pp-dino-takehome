using Microsoft.AspNetCore.Mvc;

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
            return NotFound(knfException.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Cage))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ICage>> CreateAsync(Cage cage)
    {
        _logger.LogInformation("Attempting to create a new Cages");

        try
        {
            var updatedCage = await _cageService.CreatAsync(cage);

            return Ok(updatedCage);
        }
        catch (InvalidOperationException ioException)
        {
            return BadRequest(ioException.Message);
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
            return NotFound(knfException.Message);
        }
        catch (InvalidOperationException ioException)
        {
            return BadRequest(ioException.Message);
        }
    }

    [HttpPut]
    [Route("{cageId}/powerdown")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ICage>> PowerDownAsync(int cageId)
    {
        _logger.LogInformation($"Attempting to powerdown Cage with id {cageId}");

        try
        {
            var updatedCage = await _cageService.UpdatePowerStatusAsync(
                cageId,
                PowerStatusType.Down
            );

            return Ok(updatedCage);
        }
        catch (InvalidOperationException ioException)
        {
            return BadRequest(ioException.Message);
        }
        catch (KeyNotFoundException knfException)
        {
            return NotFound(knfException.Message);
        }
    }

    [HttpPut]
    [Route("{cageId}/powerup")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ICage>> PowerUpAsync(int cageId)
    {
        _logger.LogInformation($"Attempting to powerup Cage with id {cageId}");

        try
        {
            var updatedCage = await _cageService.UpdatePowerStatusAsync(
                cageId,
                PowerStatusType.Active
            );

            return Ok(updatedCage);
        }
        catch (KeyNotFoundException knfException)
        {
            return NotFound(knfException.Message);
        }
    }

    [HttpPut]
    [Route("{cageId}/associatedinosaur/{dinosaurId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ICage>> AssociateDinosaurAsync(int cageId, int dinosaurId)
    {
        _logger.LogInformation($"Attempting to associate Dino {dinosaurId} to Cage {cageId}");

        try
        {
            var updatedCage = await _cageService.AssociateDinosaurAsync(cageId, dinosaurId);

            return Ok(updatedCage);
        }
        catch (KeyNotFoundException knfException)
        {
            return NotFound(knfException.Message);
        }
        catch (InvalidOperationException ioException)
        {
            return BadRequest(ioException.Message);
        }
    }

    [HttpPut]
    [Route("{cageId}/unassociatedinosaur/{dinosaurId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICage))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ICage>> UnassociateDinosaurAsync(int cageId, int dinosaurId)
    {
        _logger.LogInformation($"Attempting to unassociate Dino {dinosaurId} to Cage {cageId}");

        try
        {
            var updatedCage = await _cageService.UnassociateDinosaurAsync(cageId, dinosaurId);

            return Ok(updatedCage);
        }
        catch (KeyNotFoundException knfException)
        {
            return NotFound(knfException.Message);
        }
        catch (InvalidOperationException ioException)
        {
            return BadRequest(ioException.Message);
        }
    }
}
