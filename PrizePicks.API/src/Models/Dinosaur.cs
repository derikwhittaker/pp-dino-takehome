namespace PrizePicks.API.Models;

public interface IDinosaur
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Food Food { get; set; }

    public Species Species { get; set; }
}

public class Dinosaur : IDinosaur
{
    public int Id { get; set; }

    public string Name { get; set; }

    /// <summary>
    /// Is the Dino an Carnivor or Herbivor
    /// </summary>
    public Food Food { get; set; }

    public Species Species { get; set; }
}
