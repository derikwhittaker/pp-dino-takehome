namespace PrizePicks.API.Models;

public interface IDinosaur
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ISpecies Species { get; set; }
}

public class Dinosaur : IDinosaur
{
    public int Id { get; set; }

    public string Name { get; set; }

    public ISpecies Species { get; set; }
}
