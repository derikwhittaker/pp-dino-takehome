namespace PrizePicks.API.Models;

public class Species
{
    /// <summary>
    /// This is needed for WebAPI serializ
    /// </summary>
    public Species() { }

    public Species(FoodType food, SpeciesType species)
    {
        Food = food;
        SpeciesType = species;
    }

    public FoodType Food { get; private set; }

    public SpeciesType SpeciesType { get; private set; }
}
