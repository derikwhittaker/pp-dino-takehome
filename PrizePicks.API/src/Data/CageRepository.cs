using Microsoft.Extensions.Logging;
using PrizePicks.API.Models;

namespace PrizePicks.API.Data;

public interface ICageRepository
{
    public Task<IEnumerable<ICage>> CagesAsync();

    public Task<ICage> CageAsync(int cageId);

    public Task<ICage> UpdateAsync(ICage cage);
}

public class CageRepository : ICageRepository
{
    private readonly ILogger<CageRepository> _logger;
    private readonly IDatabase _database;

    public CageRepository(ILogger<CageRepository> logger, IDatabase database)
    {
        _logger = logger;

        _database = database;
    }

    /// <summary>
    /// Will pull all dinos
    /// </summary>
    /// <returns>List of all dinosaurs</returns>
    public async Task<IEnumerable<ICage>> CagesAsync()
    {
        return await _database.CagesAsync();
    }

    /// <summary>
    /// Will pull a given cage by its id.
    ///
    /// If the cage is not found, will throw exception
    /// </summary>
    /// <param name="cageId"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException">Thrown if the provided Id was not valid</exception>
    public async Task<ICage> CageAsync(int cageId)
    {
        var cages = await _database.CagesAsync();
        var foundCage = cages.Where(x => x.Id == cageId).FirstOrDefault();

        // Need to return the caller when something is not found.
        if (foundCage == null)
        {
            throw new KeyNotFoundException($"Unable to find Cage w/ id {cageId}");
        }

        return foundCage;
    }

    /// <summary>
    /// When given a cage, will update it to the DB
    /// </summary>
    /// <param name="cage"></param>
    /// <returns></returns>
    public async Task<ICage> UpdateAsync(ICage cage)
    {
        _logger.LogInformation($"Attempting to assocaite new dinosaur to Cage {cage.Id}");
        var cages = await _database.CagesAsync();

        // mimic db setting the key
        if (cage.Id <= 0)
        {
            cage.Id = cages.Count() + 1;
        }
        else
        {
            if (!cages.Any(x => x.Id == cage.Id))
            {
                throw new KeyNotFoundException($"Unable to find Cage w/ id {cage.Id}");
            }
        }

        // because we are not using sql, this is bit more work.
        _database.UpdateAsync(cage);

        return cage;
    }
}
