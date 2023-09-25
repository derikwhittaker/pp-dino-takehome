using Microsoft.Extensions.Logging;
using PrizePicks.API.Models;

namespace PrizePicks.API.Data;

public interface ICageRepository
{
    public Task Update(ICage cage);
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
    /// When given a cage, will update it to the DB
    /// </summary>
    /// <param name="cage"></param>
    /// <returns></returns>
    public async Task Update(ICage cage)
    {
        _logger.LogInformation($"Attempting to assocaite new dinosaur to Cage {cage.Id}");

        // because we are not using sql, this is bit more work.
        _database.Update(cage);
    }
}
