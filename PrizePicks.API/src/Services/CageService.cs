using Microsoft.Extensions.Logging;

using PrizePicks.API.Data;
using PrizePicks.API.Models;
using PrizePicks.API.Rules;

namespace PrizePicks.API.Services;

public interface ICageService
{
    Task<IEnumerable<ICage>> CagesAsync();
    Task<ICage> CageAsync(int cageId);
    Task<ICage> Create(ICage cage);
    Task<ICage> UpdateAsync(ICage cage);

    public Task AssociateDinosaurAsync(ICage cage, IDinosaur dinosaur);
    Task<ICage> UpdatePowerStatus(int cageId, PowerStatusType powerStatus);
}

public class CageService : ICageService
{
    private readonly ILogger<CageService> _logger;
    private readonly ICageRepository _cageRepository;
    private readonly ICageRules _cageRules;

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

    /// <summary>
    /// Will pull all known cages and return.
    ///
    /// The results are not sorted or filtered
    /// </summary>
    /// <returns>Enumerable of ICages</returns>
    public async Task<IEnumerable<ICage>> CagesAsync()
    {
        _logger.LogInformation("Pulling all cages via Cage Service");
        var cages = await _cageRepository.CagesAsync();

        return cages;
    }

    /// <summary>
    /// Will return a given a valid Cage Id, will return the cage associated
    /// </summary>
    /// <param name="cageId">Valid ID of a cage as an Int</param>
    /// <returns>ICage</returns>
    public async Task<ICage> CageAsync(int cageId)
    {
        _logger.LogInformation($"Pulling cage with id {cageId} via Cage Service");
        var cage = await _cageRepository.CageAsync(cageId);

        return cage;
    }

    /// <summary>
    /// When given a valid cage, will attempt to create a new instance.
    /// Will return the updated/new instance as part of the invoke
    /// </summary>
    /// <param name="cage">Populated ICage</param>
    /// <returns>ICage</returns>
    public async Task<ICage> Create(ICage cage)
    {
        _logger.LogInformation($"Attempting to create a new Cage");

        // canot create a cage that is not powered on
        _cageRules.AssertCageIsPoweredOn(cage);

        await _cageRepository.Update(cage);

        return cage;
    }

    /// <summary>
    /// Will attempt to update an existing cage
    /// Will return the newly updated cage
    /// </summary>
    /// <param name="cage"></param>
    /// <returns>ICage</returns>
    public async Task<ICage> UpdateAsync(ICage cage)
    {
        _logger.LogInformation($"Attempting to update an existing Cage with Id {cage.Id}");

        _cageRules.AssertCageIsPoweredOn(cage);

        await _cageRepository.Update(cage);

        return cage;
    }

    public async Task AssociateDinosaurAsync(ICage cage, IDinosaur dinosaur)
    {
        _logger.LogInformation($"Attempt add Dino with ID {dinosaur.Id} to Cage with Id {cage.Id}");

        // check to see if cage is powered on
        // check to see if cage is already at capacity
        // check to see if the dino is valid tye to be added to the cage
        _cageRules.AssertCageIsPoweredOn(cage);
        _cageRules.AssertCageNotAtCapacity(cage);
        _cageRules.AssertDinoValidForCage(cage, dinosaur);

        cage.AssociateDinosaur(dinosaur);

        // We have valid info/rules, time to take action
        await _cageRepository.Update(cage);
    }

    public async Task<ICage> UpdatePowerStatus(int cageId, PowerStatusType powerStatus)
    {
        // Pull the latest/greatest cage
        var cageToUpdate = await _cageRepository.CageAsync(cageId);

        if (powerStatus == PowerStatusType.Down && !_cageRules.IsAbleToBePoweredDown(cageToUpdate))
        {
            throw new InvalidOperationException(
                $"Cage {cageId} is not able to be powered down as it has Dinosaurs"
            );
        }

        cageToUpdate.PowerStatus = powerStatus;
        var updatedCage = await _cageRepository.Update(cageToUpdate);

        return updatedCage;
    }
}
