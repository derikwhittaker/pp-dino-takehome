using System.Text.Json.Serialization;

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

    // [JsonConverter(typeof(JsonStringEnumConverter))]
    public FoodType Food { get; private set; }

    // [JsonConverter(typeof(JsonStringEnumConverter))]
    public SpeciesType SpeciesType { get; private set; }
}
