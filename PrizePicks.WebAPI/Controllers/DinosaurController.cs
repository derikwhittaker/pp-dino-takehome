using Microsoft.AspNetCore.Mvc;

using PrizePicks.API.Models;

namespace PrizePicks.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DinosaursController : ControllerBase
{
    private readonly ILogger<DinosaursController> _logger;

    public DinosaursController(ILogger<DinosaursController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDinosaur))]
    public async Task<ActionResult<IEnumerable<IDinosaur>>> GetDinosaur()
    {
        _logger.LogInformation($"Attempting to get all Dinosaurs");
        return Enumerable.Range(1, 5).Select(index => new Dinosaur()).ToArray();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDinosaur))]
    [Route("{dinosaurId}")]
    public async Task<ActionResult<IDinosaur>> GetSingle(int dinosaurId)
    {
        _logger.LogInformation($"Attempting to Dinosaur for id {dinosaurId}");
        return new Dinosaur { Id = dinosaurId };
    }

    [HttpPost(Name = "Dinosaur")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IDinosaur))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IDinosaur>> Create(Dinosaur dinosaur)
    {
        _logger.LogInformation($"Attempting to create a new Dinosaur");
        // return dinosaur;
        return NotFound();
    }

    [HttpPut(Name = "Dinosaur")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IDinosaur))]
    public async Task<ActionResult<IDinosaur>> Update(Dinosaur dinosaur)
    {
        return dinosaur;
    }
}
