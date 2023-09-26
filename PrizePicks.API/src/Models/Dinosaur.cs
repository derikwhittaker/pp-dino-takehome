namespace PrizePicks.API.Models;

public interface IDinosaur
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Species Species { get; set; }
    FoodType Food { get; set; }
}

public class Dinosaur : IDinosaur
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required Species Species { get; set; }

    public FoodType Food { get; set; } = FoodType.Herbivore;
}
