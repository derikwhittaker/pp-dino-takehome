using Microsoft.Extensions.Logging;

using PrizePicks.API.Models;

namespace PrizePicks.API.Rules;

public interface ICageRules
{
    public void AssertCageNotAtCapacity(ICage cage);

    public void AssertDinoValidForCage(ICage cage, IDinosaur dinosaur);

    public void AssertCageIsPoweredOn(ICage cage);

    public bool IsAbleToBePoweredDown(ICage cage);
}

/// <summary>
/// Dedicated business rules for Cage's.  Each rule is stand along and should never
///     depend on another rule.
///
/// If rules need to be chained, they should be chained in service layer
/// </summary>
public class CageRules : ICageRules
{
    private readonly ILogger<CageRules> _logger;

    public CageRules(ILogger<CageRules> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Will throw an exception if the cage already has the max
    ///     allowed allowed dinosaurs.
    /// </summary>
    /// <param name="cage"></param>
    /// <exception cref="CagePowerExceptionException"></exception>
    public void AssertCageNotAtCapacity(ICage cage)
    {
        if (cage.Capacity == cage.Dinosaurs.Count())
        {
            _logger.LogDebug($"Unable to use Cage {cage.Id} as it is already at capacity");
            throw new InvalidOperationException(
                $"Cage {cage.Id} cannot be used as it is already at capacity"
            );
        }
    }

    /// <summary>
    /// Will throw exception if the cage is not powered on
    /// </summary>
    /// <param name="cage"></param>
    /// <exception cref="CagePowerExceptionException"></exception>
    public void AssertCageIsPoweredOn(ICage cage)
    {
        if (cage.PowerStatus != PowerStatusType.Active)
        {
            _logger.LogDebug($"Unable to use Cage {cage.Id} as it is not powered on");
            throw new InvalidOperationException(
                $"Cage {cage.Id} cannot be used as it is not powered on"
            );
        }
    }

    /// <summary>
    /// There are rules around when a cage can be powered down.
    /// If there are any dinosaurs in the cage, it cannot be powered down.
    /// </summary>
    /// <param name="cage"></param>
    public bool IsAbleToBePoweredDown(ICage cage)
    {
        _logger.LogInformation($"Checking to see if Cage {cage.Id} is able to be powered down");

        // If the cage is already powered down, we can leave now
        if (cage.PowerStatus == PowerStatusType.Down)
        {
            return true;
        }

        if (cage.Dinosaurs.Count() > 0)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// There are some rules needed to determine if the dino is valid
    ///     for the given cage
    ///
    ///     1. If a Herbivor is in a cage, cannot add a Carnivor (vice versa)
    ///     2. If is Carnivor, it can only be in the case w/ same species
    /// </summary>
    /// <param name="cage"></param>
    /// <param name="dinosaur"></param>
    public void AssertDinoValidForCage(ICage cage, IDinosaur dinosaur)
    {
        // If the cage is empty, we can exit now as we know we are good
        if (cage.Dinosaurs.Count() == 0)
        {
            return;
        }

        // We cannot mix the type of dino (Food)
        if (cage.Dinosaurs.Any(x => x.Species.Food != dinosaur.Species.Food))
        {
            _logger.LogDebug($"Unable to add Dino to Cage {cage.Id} as you cannot mix Food Types");
            throw new InvalidOperationException(
                $"Cage {cage.Id} cannot accept {dinosaur.Species.Food}"
            );
        }

        // Carnivor's are special, they cannot be in a cage w/ another species
        //  If we get here, we know that all Dino's are Carnivors, so no need to do that check
        if (
            dinosaur.Species.Food == FoodType.Carnivore
            && cage.Dinosaurs.Any(x => x.Species.SpeciesType != dinosaur.Species.SpeciesType)
        )
        {
            _logger.LogDebug(
                $"Unable to add Dino to Cage {cage.Id} as you cannot mix Carnivor Species "
            );
            throw new InvalidOperationException(
                $"Cage {cage.Id} cannot accept {dinosaur.Species.SpeciesType}"
            );
        }
    }
}
