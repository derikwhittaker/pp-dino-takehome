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

    Task<IDinosaur> UpdateAsync(IDinosaur dinosaur);
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
    /// Will return a given a valid Dinosaur Id, will return the Dinosaur associated
    /// </summary>
    /// <param name="dinosaurId">Valid ID of a Dinosaur as an Int</param>
    /// <returns>IDinosaur</returns>
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

        // canot create a dino that has no name, just not nice
        _dinosaurRules.AssertDinosaurHasName(dinosaur);

        var updatedDinosaur = await _dinosaurRepository.UpdateAsync(dinosaur);

        return updatedDinosaur;
    }

    /// <summary>
    /// Will attempt to update an existing dinosaur
    /// Will return the newly updated dinosaur
    /// </summary>
    /// <param name="dinosaur"></param>
    /// <returns>IDinosaur</returns>
    public async Task<IDinosaur> UpdateAsync(IDinosaur dinosaur)
    {
        _logger.LogInformation($"Attempting to update an existing dinosaur with Id {dinosaur.Id}");

        // canot create a dino that has no name, just not nice
        _dinosaurRules.AssertDinosaurHasName(dinosaur);

        var updatedDinosaur = await _dinosaurRepository.UpdateAsync(dinosaur);

        return updatedDinosaur;
    }
}
