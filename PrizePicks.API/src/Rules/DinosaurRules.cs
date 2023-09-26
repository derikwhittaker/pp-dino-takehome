using Microsoft.Extensions.Logging;

using PrizePicks.API.Models;

namespace PrizePicks.API.Rules;

public interface IDinosaurRules
{
    public void AssertDinosaurHasName(IDinosaur dinosau);
}

/// <summary>
/// Dedicated business rules for Dinosaur's.  Each rule is stand along and should never
///     depend on another rule.
///
/// If rules need to be chained, they should be chained in service layer
/// </summary>
public class DinosaurRules : IDinosaurRules
{
    private readonly ILogger<DinosaurRules> _logger;

    public DinosaurRules(ILogger<DinosaurRules> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Will throw an exception if the cage already has the max
    ///     allowed allowed dinosaurs.
    /// </summary>
    /// <param name="cage"></param>
    /// <exception cref="CagePowerExceptionException"></exception>
    public void AssertDinosaurHasName(IDinosaur dinosaur)
    {
        if (string.IsNullOrEmpty(dinosaur.Name))
        {
            _logger.LogDebug($"Dinosaur does not have a name, this is required");
            throw new InvalidOperationException($"Dinosaur must have a name");
        }
    }
}
