namespace PrizePicks.API.Models;

public interface ISpecies
{
    public FoodType Food { get; }

    public SpeciesType SpeciesType { get; }
}

public class Species : ISpecies
{
    public Species(FoodType food, SpeciesType species)
    {
        Food = food;
        SpeciesType = species;
    }

    public FoodType Food { get; private set; }

    public SpeciesType SpeciesType { get; private set; }
}
