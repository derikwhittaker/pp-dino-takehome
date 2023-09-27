using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

using PrizePicks.API.Models;
using PrizePicks.API.Services;

namespace PrizePicks.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DinosaursController : ControllerBase
{
    private readonly ILogger<DinosaursController> _logger;
    private readonly IDinosaurService _dinosaurService;

    public DinosaursController(
        ILogger<DinosaursController> logger,
        IDinosaurService dinosaurService
    )
    {
        _logger = logger;
        _dinosaurService = dinosaurService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dinosaur))]
    public async Task<ActionResult<IEnumerable<IDinosaur>>> GetAllAsync()
    {
        _logger.LogInformation($"Attempting to get all Dinosaurs from repo");
        var dinosaurs = await _dinosaurService.DinosaursAsync();

        return Ok(dinosaurs);
    }

    [HttpGet]
    [Route("{dinosaurId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dinosaur))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IDinosaur>> GetSingleAsync(int dinosaurId)
    {
        _logger.LogInformation($"Attempting to Cages for id {dinosaurId}");

        try
        {
            var dinosaur = await _dinosaurService.DinosaurAsync(dinosaurId);

            return Ok(dinosaur);
        }
        catch (KeyNotFoundException knfException)
        {
            return NotFound(knfException.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Dinosaur))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IDinosaur>> CreateAsync(Dinosaur dinosaur)
    {
        _logger.LogInformation("Attempting to create a new Dinosaur");

        try
        {
            var updatedDinosaur = await _dinosaurService.CreateAsync(dinosaur);

            return Ok(updatedDinosaur);
        }
        catch (InvalidOperationException ioException)
        {
            return BadRequest(ioException.Message);
        }
    }

    [HttpPut(Name = "Dinosaur")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDinosaur))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IDinosaur>> UpdateAsync(Dinosaur dinosaur)
    {
        _logger.LogInformation("Attempting to update an existing Dinosaur");

        _logger.LogDebug(JsonSerializer.Serialize(dinosaur));

        try
        {
            var updatedDinosaur = await _dinosaurService.UpdateAsync(dinosaur);

            return Ok(updatedDinosaur);
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
}
