using Microsoft.Extensions.Logging;

namespace PrizePicks.API.Data;

public interface ICageRepository { }

public class CageRepository : ICageRepository
{
    private readonly ILogger<CageRepository> _logger;
    private readonly IDatabase _database;
    

    public CageRepository(ILogger<CageRepository> logger, IDatabase database)
    {
        _logger = logger;

        _database = database;
    }
}
