using Microsoft.Extensions.Logging;
using PrizePicks.API.Models;

namespace PrizePicks.API.Data;

public interface IDinosaurRepository
{
    Task<IEnumerable<IDinosaur>> DinosaursAsync();
    Task<IDinosaur> DinosaurAsync(int dinosaurId);
}

public class DinosaurRepository : IDinosaurRepository
{
    private readonly ILogger<DinosaurRepository> _logger;
    private readonly IDatabase _database;

    public DinosaurRepository(ILogger<DinosaurRepository> logger, IDatabase database)
    {
        _logger = logger;
        _database = database;
    }

    public async Task<IEnumerable<IDinosaur>> DinosaursAsync()
    {
        return await _database.DinosaursAsync();
    }

    public async Task<IDinosaur> DinosaurAsync(int dinosaurId)
    {
        var dinosaurs = await _database.DinosaursAsync();
        var foundDinosaur = dinosaurs.Where(x => x.Id == dinosaurId).FirstOrDefault();

        // Need to return the caller when something is not found.
        if (foundDinosaur == null)
        {
            throw new KeyNotFoundException($"Unable to find Dinosaur w/ id {dinosaurId}");
        }

        return foundDinosaur;
    }
}
