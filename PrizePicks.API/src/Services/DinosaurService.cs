using Microsoft.Extensions.Logging;
using System.Text.Json;

using PrizePicks.API.Data;
using PrizePicks.API.Models;
using PrizePicks.API.Rules;

namespace PrizePicks.API.Services;

public interface IDinosaurService
{
    Task<IEnumerable<IDinosaur>> DinosaursAsync();

    Task<IDinosaur> DinosaurAsync(int dinosaurId);

    Task<IDinosaur> CreateAsync(IDinosaur dinosaur);
}

public class DinosaurService : IDinosaurService
{
    private readonly ILogger<DinosaurService> _logger;
    private readonly IDinosaurRepository _dinosaurRepository;
    private readonly IDinosaurRules _dinosaurRules;

    public DinosaurService(
        ILogger<DinosaurService> logger,
        IDinosaurRules dinosaurRules,
        IDinosaurRepository dinosaurRepository
    )
    {
        _logger = logger;
        _dinosaurRules = dinosaurRules;
        _dinosaurRepository = dinosaurRepository;
    }

    /// <summary>
    /// Will pull all known Dinosaurs and return.
    ///
    /// The results are not sorted or filtered
    /// </summary>
    /// <returns>Enumerable of IDinosaur</returns>
    public async Task<IEnumerable<IDinosaur>> DinosaursAsync()
    {
        _logger.LogInformation("Pulling all Dinosaurs via Dinosaur Service");

        var dinosaurs = await _dinosaurRepository.DinosaursAsync();

        return dinosaurs;
    }

    /// <summary>
    /// Will return a given a valid Dinosaur Id, will return the cage associated
    /// </summary>
    /// <param name="dinosaurId">Valid ID of a cage as an Int</param>
    /// <returns>ICage</returns>
    public async Task<IDinosaur> DinosaurAsync(int dinosaurId)
    {
        _logger.LogInformation($"Pulling Dinosaur with id {dinosaurId} via Dinosaur Service");
        var dinosaur = await _dinosaurRepository.DinosaurAsync(dinosaurId);

        return dinosaur;
    }

    /// <summary>
    /// When given a valid dinosaur, will attempt to create a new instance.
    /// Will return the updated/new instance as part of the invoke
    /// </summary>
    /// <param name="Dinosaur">Populated IDinosaur</param>
    /// <returns>IDinosaur</returns>
    public async Task<IDinosaur> CreateAsync(IDinosaur dinosaur)
    {
        _logger.LogInformation($"Attempting to create a new Dinosaur");

        // canot create a cage that is not powered on
        _dinosaurRules.AssertDinosaurHasName(dinosaur);

        var updatedDinosaur = await _dinosaurRepository.UpdateAsync(dinosaur);

        return updatedDinosaur;
    }
}
