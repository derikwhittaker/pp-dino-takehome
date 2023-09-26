using Microsoft.Extensions.Logging;
using PrizePicks.API.Models;

namespace PrizePicks.API.Data;

public interface IDinosaurRepository
{
    Task<IEnumerable<IDinosaur>> DinosaursAsync();
    Task<IDinosaur> DinosaurAsync(int dinosaurId);

    Task<IDinosaur> UpdateAsync(IDinosaur dinosaur);
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

    /// <summary>
    /// When given a dinosaur, will update it to the DB
    /// </summary>
    /// <param name="dinosaur"></param>
    /// <returns></returns>
    public async Task<IDinosaur> UpdateAsync(IDinosaur dinosaur)
    {
        _logger.LogInformation($"Attempting to create/update Dinosaur {dinosaur.Id}");

        // mimic db setting the key
        if (dinosaur.Id <= 0)
        {
            dinosaur.Id = new Random().Next(100);
        }
        else
        {
            var dinosaurs = await _database.DinosaursAsync();
            if (!dinosaurs.Any(x => x.Id == dinosaur.Id))
            {
                throw new KeyNotFoundException($"Unable to find Dinosaur w/ id {dinosaur.Id}");
            }
        }

        // because we are not using sql, this is bit more work.
        _database.Update(dinosaur);

        return dinosaur;
    }
}
