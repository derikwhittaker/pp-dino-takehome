namespace PrizePicks.API.Models;

public interface ICage
{
    public int Id { get; set; }
    public PowerStatusType PowerStatus { get; set; }

    public int Capacity { get; set; }

    public IEnumerable<IDinosaur> Dinosaurs { get; }

    public void AssociateDinosaur(IDinosaur dinosaur);
    public void UnassociateDinosaur(IDinosaur dinosaur);
}

public class Cage : ICage
{
    public Cage() { }

    private readonly IList<IDinosaur> _dinosaurs = new List<IDinosaur>();

    public Cage(IList<IDinosaur> dinosaurs)
    {
        _dinosaurs = dinosaurs;
    }

    public void AssociateDinosaur(IDinosaur dinosaur)
    {
        var existingDino = _dinosaurs.FirstOrDefault(x => x.Id == dinosaur.Id);

        // If we find an existing, we are going to swap it out
        if (existingDino != null)
        {
            _dinosaurs.Remove(existingDino);
        }

        _dinosaurs.Add(dinosaur);
    }

    public void UnassociateDinosaur(IDinosaur dinosaur)
    {
        var existingDino = _dinosaurs.FirstOrDefault(x => x.Id == dinosaur.Id);

        // If we find an existing, we are going to swap it out
        if (existingDino != null)
        {
            _dinosaurs.Remove(existingDino);
        }
    }

    public int Id { get; set; }

    public PowerStatusType PowerStatus { get; set; }

    public int Capacity { get; set; }

    public IEnumerable<IDinosaur> Dinosaurs
    {
        get => _dinosaurs;
    }
}
