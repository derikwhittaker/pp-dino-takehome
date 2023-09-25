using System.Text.Json;

using PrizePicks.API.Models;

namespace PrizePicks.API.Data;

public class InMemoryDB : IDatabase
{
    private static IList<ICage> _cages { get; set; }
    private static IList<IDinosaur> _dinosaurs { get; set; }

    public IEnumerable<ICage> Cages()
    {
        return _cages;
    }

    public void Remove(ICage instance)
    {
        if (instance.Id <= 0)
        {
            throw new ArgumentException("Instace must have valid Id", nameof(instance));
        }

        _cages = _cages.Where(x => x.Id != instance.Id).ToList();
    }

    /// <summary>
    /// This is generic update/replace/insert method.
    ///
    /// If the item is in the list, it will be removed first and the new version
    ///     will be placed in the list.
    ///
    /// If rthe item is new, nothing will be removed from the list and the new
    ///     instance will be placed in the list.
    /// </summary>
    /// <param name="instance"></param>
    /// <exception cref="ArgumentException"></exception>
    public void Update(ICage instance)
    {
        if (instance.Id <= 0)
        {
            throw new ArgumentException("Instace must have valid Id", nameof(instance));
        }

        Remove(instance);

        _cages.Add(instance);
    }

    public IEnumerable<IDinosaur> Dinosaurs()
    {
        return _dinosaurs;
    }

    public void Remove(IDinosaur instance)
    {
        if (instance.Id <= 0)
        {
            throw new ArgumentException("Instace must have valid Id", nameof(instance));
        }

        _dinosaurs = _dinosaurs.Where(x => x.Id != instance.Id).ToList();
    }

    /// <summary>
    /// This is generic update/replace/insert method.
    ///
    /// If the item is in the list, it will be removed first and the new version
    ///     will be placed in the list.
    ///
    /// If rthe item is new, nothing will be removed from the list and the new
    ///     instance will be placed in the list.
    /// </summary>
    /// <param name="instance"></param>
    /// <exception cref="ArgumentException"></exception>
    public void Update(IDinosaur instance)
    {
        if (instance.Id <= 0)
        {
            throw new ArgumentException("Instace must have valid Id", nameof(instance));
        }

        Remove(instance);

        _dinosaurs.Add(instance);
    }

    /// Seed/Populate below

    /// <summary>
    /// Simple helper method to load the initial static data
    /// </summary>
    public static void Seed()
    {
        SeedDinosaurs();
        SeedCages();
    }

    private static void SeedDinosaurs()
    {
        _dinosaurs = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 1,
                Name = "Roary",
                Food = Food.Carnivore,
                Species = Species.Tyrannosaurus
            },
            new Dinosaur
            {
                Id = 2,
                Name = "Dizzy",
                Food = Food.Herbivore,
                Species = Species.Megalosaurus
            },
            new Dinosaur
            {
                Id = 3,
                Name = "Spike",
                Food = Food.Carnivore,
                Species = Species.Velociraptor
            },
            new Dinosaur
            {
                Id = 4,
                Name = "T-Rexi",
                Food = Food.Carnivore,
                Species = Species.Tyrannosaurus
            },
            new Dinosaur
            {
                Id = 5,
                Name = "Rumble",
                Food = Food.Herbivore,
                Species = Species.Spinosaurus
            }
        };
    }

    private static void SeedCages()
    {
        _cages = new List<ICage>
        {
            new Cage
            {
                Id = 1,
                PowerStatus = PowerStatus.Active,
                Capacity = 1
                // Name = "Roary",
                // Food = Food.Carnivore,
                // Species = Species.Tyrannosaurus
            },
            new Cage
            {
                Id = 2,
                PowerStatus = PowerStatus.Active,
                Capacity = 1
                // Name = "Roary",
                // Food = Food.Carnivore,
                // Species = Species.Tyrannosaurus
            },
        };
    }
}
