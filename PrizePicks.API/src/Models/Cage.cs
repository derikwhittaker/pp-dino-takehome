namespace PrizePicks.API.Models;

public interface ICage
{
    public int Id { get; set; }
    public PowerStatus PowerStatus { get; set; }

    public int Capacity { get; set; }

    public IEnumerable<IDinosaur> Dinosaurs { get; set; }
}

public class Cage : ICage
{
    public Cage() { }

    public Cage(IList<IDinosaur> dinosaurs)
    {
        Dinosaurs = dinosaurs;
    }

    public int Id { get; set; }

    public PowerStatus PowerStatus { get; set; }

    public int Capacity { get; set; }

    public IEnumerable<IDinosaur> Dinosaurs { get; set; }
}
