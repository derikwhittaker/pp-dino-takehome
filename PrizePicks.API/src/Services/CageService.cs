using Microsoft.Extensions.Logging;

using PrizePicks.API.Data;
using PrizePicks.API.Models;
using PrizePicks.API.Rules;

namespace PrizePicks.API.Services;

public interface ICageService
{
    public void AddDinosaur(ICage cage, IDinosaur dinosaur);
}

public class CageService : ICageService
{
    private readonly ILogger<CageService> _logger;
    private ICageRepository _cageRepository;
    private ICageRules _cageRules;

    public CageService(
        ILogger<CageService> logger,
        ICageRules cageRules,
        ICageRepository cageRepository
    )
    {
        _logger = logger;
        _cageRules = cageRules;
        _cageRepository = cageRepository;
    }

    public async void AddDinosaur(ICage cage, IDinosaur dinosaur)
    {
        _logger.LogInformation($"Attempt add Dino with ID {dinosaur.Id} to Cage with Id {cage.Id}");

        // check to see if cage is powered on
        // check to see if cage is already at capacity
        // check to see if the dino is valid tye to be added to the cage

        _cageRules.IsPoweredOn(cage);
        _cageRules.IsCageAtCapacity(cage);
    }

    private void IsDinoValidForCage(ICage cage, IDinosaur dinosaur) { }
}
