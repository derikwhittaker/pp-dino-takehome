using Microsoft.Extensions.Logging;

using Moq;

using PrizePicks.API.Data;
using PrizePicks.API.Models;

[TestFixture]
public class InMemoryDBTests
{
    private InMemoryDB _inMemoryDB = new InMemoryDB();

    [SetUp]
    public void Setup()
    {
        // Reset the data between each run
        PrizePicks.API.Data.InMemoryDB.Seed();
    }

    [Test]
    public void Seed_WillBuildDataCorrectly()
    {
        var expectedSpeciesCount = 8;
        var expectedDinosoaurCount = 9;
        var expectedCageCount = 2;

        Assert.True(_inMemoryDB.Species().Count() == expectedSpeciesCount);
        Assert.True(_inMemoryDB.Dinosaurs().Count() == expectedDinosoaurCount);
        Assert.True(_inMemoryDB.Cages().Count() == expectedCageCount);
    }

    [Test]
    public void Cages_Remove_WhenInvalidIdProvided_WillThrowException()
    {
        Assert.Throws<ArgumentException>(() => _inMemoryDB.Remove(new Cage()));
    }

    [Test]
    public void Cages_Remove_WhenValidIdProvided_WillRemoveFromList()
    {
        var cageToRemove = new Cage { Id = 2 };
        var expectedFinalCageCount = _inMemoryDB.Cages().Count() - 1;

        _inMemoryDB.Remove(cageToRemove);

        Assert.True(_inMemoryDB.Cages().Count() == expectedFinalCageCount);
    }

    [Test]
    public void Cages_Remove_WhenInValidIdProvided_WillNotRemoveFromList()
    {
        var cageToRemove = new Cage { Id = 222 };
        var expectedFinalCageCount = _inMemoryDB.Cages().Count();

        _inMemoryDB.Remove(cageToRemove);

        Assert.True(_inMemoryDB.Cages().Count() == expectedFinalCageCount);
    }

    [Test]
    public void Cages_Update_WhenExistingItemFound_WillReplaceWithNewInstance()
    {
        var cageToUpdate = _inMemoryDB.Cages().Where(x => x.Id == 2).First();

        cageToUpdate.Capacity = 2;

        _inMemoryDB.Update(cageToUpdate);

        var cageUpdated = _inMemoryDB.Cages().Where(x => x.Id == 2).First();

        Assert.True(cageToUpdate.Capacity == 2);
    }

    [Test]
    public void Cages_Update_WhenExistingIsNotItemFound_WillAddNewInstance()
    {
        var cageToUpdate = new Cage { Id = 4, Capacity = 2 };
        var expectedNewCagesCount = _inMemoryDB.Cages().Count() + 1;

        _inMemoryDB.Update(cageToUpdate);

        var newCagesCount = _inMemoryDB.Cages().Count();

        Assert.True(
            newCagesCount == expectedNewCagesCount,
            $"Expected {newCagesCount} but had {expectedNewCagesCount}"
        );
    }

    [Test]
    public void Dinosaurs_Remove_WhenInvalidIdProvided_WillThrowException()
    {
        Assert.Throws<ArgumentException>(() => _inMemoryDB.Remove(new Dinosaur()));
    }

    [Test]
    public void Dinosaurs_Remove_WhenValidIdProvided_WillRemoveFromList()
    {
        var cageToRemove = new Dinosaur { Id = 2 };
        var expectedFinalDinosaurCount = _inMemoryDB.Dinosaurs().Count() - 1;

        _inMemoryDB.Remove(cageToRemove);

        Assert.True(_inMemoryDB.Dinosaurs().Count() == expectedFinalDinosaurCount);
    }

    [Test]
    public void Dinosaurs_Remove_WhenInValidIdProvided_WillNotRemoveFromList()
    {
        var cageToRemove = new Dinosaur { Id = 222 };
        var expectedFinalDinosaurCount = _inMemoryDB.Dinosaurs().Count();

        _inMemoryDB.Remove(cageToRemove);

        Assert.True(_inMemoryDB.Dinosaurs().Count() == expectedFinalDinosaurCount);
    }

    [Test]
    public void Dinosaurs_Update_WhenExistingItemFound_WillReplaceWithNewInstance()
    {
        var dinosaurToUpdate = _inMemoryDB.Dinosaurs().Where(x => x.Id == 2).First();

        dinosaurToUpdate.Name = "Test Name";

        _inMemoryDB.Update(dinosaurToUpdate);

        var cageUpdated = _inMemoryDB.Dinosaurs().Where(x => x.Id == 2).First();

        Assert.True(dinosaurToUpdate.Name == "Test Name");
    }

    [Test]
    public void Dinosaurs_Update_WhenExistingIsNotItemFound_WillAddNewInstance()
    {
        var dinosuarToUpdate = new Dinosaur { Id = 44, Name = "TestName" };
        var expectedNewDionsaurCount = _inMemoryDB.Dinosaurs().Count() + 1;

        _inMemoryDB.Update(dinosuarToUpdate);

        var newDinosaurCount = _inMemoryDB.Dinosaurs().Count();

        Assert.True(
            newDinosaurCount == expectedNewDionsaurCount,
            $"Expected {newDinosaurCount} but had {expectedNewDionsaurCount}"
        );
    }
}
