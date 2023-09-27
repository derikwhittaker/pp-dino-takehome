using PrizePicks.API.Models;

namespace PrizePicks.APITests.Models;

[TestFixture]
public class CageTests
{
    [Test]
    public void AssociateDinosaur_When_ExisingIsFound_WillRemoveBeforeAddingToList()
    {
        var initialDinosaurs = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 1,
                Name = "Fred",
                Species = new Species()
            }
        };

        var dinoToAssociate = new Dinosaur
        {
            Id = 1,
            Name = "New Fred",
            Species = new Species()
        };

        var cage = new Cage(initialDinosaurs);

        cage.AssociateDinosaur(dinoToAssociate);

        // validate that we still only have 1
        Assert.True(cage.Dinosaurs.Count() == 1);

        var updatedDino = cage.Dinosaurs.First(x => x.Id == 1);
        Assert.True(updatedDino.Name == dinoToAssociate.Name);
    }

    [Test]
    public void AssociateDinosaur_When_ExisingIsNotFound_WillAddingToList()
    {
        var initialDinosaurs = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 2,
                Name = "Fred",
                Species = new Species()
            }
        };

        var dinoToAssociate = new Dinosaur
        {
            Id = 1,
            Name = "New Fred",
            Species = new Species()
        };

        var cage = new Cage(initialDinosaurs);

        cage.AssociateDinosaur(dinoToAssociate);

        // validate that we 2 as the new item was not already found
        Assert.True(cage.Dinosaurs.Count() == 2);
    }

    [Test]
    public void UnassociateDinosaur_When_ExisingIsFound_WillRemoveFromList()
    {
        var initialDinosaurs = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 1,
                Name = "Fred",
                Species = new Species()
            }
        };

        var dinoToUnassociate = new Dinosaur
        {
            Id = 1,
            Name = "Fred",
            Species = new Species()
        };

        var cage = new Cage(initialDinosaurs);

        cage.UnassociateDinosaur(dinoToUnassociate);

        // validate that are empty
        Assert.True(cage.Dinosaurs.Count() == 0);
    }
}
