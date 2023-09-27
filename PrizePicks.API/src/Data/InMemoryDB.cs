using PrizePicks.API.Models;

namespace PrizePicks.API.Data;

public class InMemoryDB : IDatabase
{
    private static IList<ICage> CagesDB { get; set; }
    private static IList<IDinosaur> DinosaursDB { get; set; }
    private static IList<Species> SpeciesDB { get; set; }

    public async Task<IEnumerable<ICage>> CagesAsync()
    {
        return CagesDB;
    }

    public void RemoveAsync(ICage instance)
    {
        if (instance.Id <= 0)
        {
            throw new ArgumentException("Instace must have valid Id", nameof(instance));
        }

        CagesDB = CagesDB.Where(x => x.Id != instance.Id).ToList();
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
    public void UpdateAsync(ICage instance)
    {
        if (instance.Id <= 0)
        {
            throw new ArgumentException("Instace must have valid Id", nameof(instance));
        }

        RemoveAsync(instance);

        CagesDB.Add(instance);
    }

    public async Task<IEnumerable<IDinosaur>> DinosaursAsync()
    {
        return DinosaursDB;
    }

    public void RemoveAsync(IDinosaur instance)
    {
        if (instance.Id <= 0)
        {
            throw new ArgumentException("Instace must have valid Id", nameof(instance));
        }

        DinosaursDB = DinosaursDB.Where(x => x.Id != instance.Id).ToList();
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
    public void UpdateAsync(IDinosaur instance)
    {
        if (instance.Id <= 0)
        {
            throw new ArgumentException("Instace must have valid Id", nameof(instance));
        }

        RemoveAsync(instance);

        DinosaursDB.Add(instance);
    }

    public IEnumerable<Species> Species()
    {
        return SpeciesDB;
    }

    /// Seed/Populate below

    /// <summary>
    /// Simple helper method to load the initial static data
    /// </summary>
    public static void Seed()
    {
        SeedSpecies();
        SeedDinosaurs();
        SeedCages();
    }

    private static void SeedDinosaurs()
    {
        DinosaursDB = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 1,
                Name = "T-Roary",
                Species = SpeciesDB.First(x => x.SpeciesType == SpeciesType.Tyrannosaurus)
            },
            new Dinosaur
            {
                Id = 2,
                Name = "Dizzy",
                Species = SpeciesDB.First(x => x.SpeciesType == SpeciesType.Megalosaurus)
            },
            new Dinosaur
            {
                Id = 3,
                Name = "Spike",
                Species = SpeciesDB.First(x => x.SpeciesType == SpeciesType.Velociraptor)
            },
            new Dinosaur
            {
                Id = 4,
                Name = "T-Rexi",
                Species = SpeciesDB.First(x => x.SpeciesType == SpeciesType.Tyrannosaurus)
            },
            new Dinosaur
            {
                Id = 5,
                Name = "Rumble",
                Species = SpeciesDB.First(x => x.SpeciesType == SpeciesType.Spinosaurus)
            },
            new Dinosaur
            {
                Id = 6,
                Name = "Leafia",
                Species = SpeciesDB.First(x => x.SpeciesType == SpeciesType.Brachiosaurus)
            },
            new Dinosaur
            {
                Id = 7,
                Name = "Staggy",
                Species = SpeciesDB.First(x => x.SpeciesType == SpeciesType.Stegosaurus)
            },
            new Dinosaur
            {
                Id = 8,
                Name = "VeggieRex",
                Species = SpeciesDB.First(x => x.SpeciesType == SpeciesType.Ankylosaurus)
            },
            new Dinosaur
            {
                Id = 9,
                Name = "Toppie",
                Species = SpeciesDB.First(x => x.SpeciesType == SpeciesType.Triceratops)
            },
            new Dinosaur
            {
                Id = 10,
                Name = "T-Short Arms",
                Species = SpeciesDB.First(x => x.SpeciesType == SpeciesType.Tyrannosaurus)
            }
        };
    }

    private static void SeedCages()
    {
        CagesDB = new List<ICage>
        {
            new Cage(
                DinosaursDB
                    .Where(x => x.Species.SpeciesType == SpeciesType.Tyrannosaurus)
                    .Take(1)
                    .ToList()
            )
            {
                Id = 1,
                PowerStatus = PowerStatusType.Active,
                Capacity = 10,
            },
            new Cage
            {
                Id = 2,
                PowerStatus = PowerStatusType.Active,
                Capacity = 1
            },
            new Cage(
                DinosaursDB
                    .Where(x => x.Species.SpeciesType == SpeciesType.Spinosaurus)
                    .Take(1)
                    .ToList()
            )
            {
                Id = 3,
                PowerStatus = PowerStatusType.Active,
                Capacity = 1
            },
        };
    }

    private static void SeedSpecies()
    {
        SpeciesDB = new List<Species>
        {
            new Species(FoodType.Carnivore, SpeciesType.Tyrannosaurus),
            new Species(FoodType.Carnivore, SpeciesType.Velociraptor),
            new Species(FoodType.Carnivore, SpeciesType.Spinosaurus),
            new Species(FoodType.Carnivore, SpeciesType.Megalosaurus),
            new Species(FoodType.Herbivore, SpeciesType.Brachiosaurus),
            new Species(FoodType.Herbivore, SpeciesType.Stegosaurus),
            new Species(FoodType.Herbivore, SpeciesType.Ankylosaurus),
            new Species(FoodType.Herbivore, SpeciesType.Triceratops),
        };
    }
}
