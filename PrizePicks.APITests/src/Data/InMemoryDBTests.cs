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
    public async Task Seed_WillBuildDataCorrectly()
    {
        var expectedSpeciesCount = 8;
        var expectedDinosoaurCount = 9;
        var expectedCageCount = 2;

        var species = _inMemoryDB.Species();
        var dinosaurs = _inMemoryDB.Dinosaurs();
        var cages = await _inMemoryDB.CagesAsync();

        Assert.True(species.Count() == expectedSpeciesCount);
        Assert.True(dinosaurs.Count() == expectedDinosoaurCount);
        Assert.True(cages.Count() == expectedCageCount);
    }

    [Test]
    public void Cages_Remove_WhenInvalidIdProvided_WillThrowException()
    {
        Assert.Throws<ArgumentException>(() => _inMemoryDB.Remove(new Cage()));
    }

    [Test]
    public async Task Cages_Remove_WhenValidIdProvided_WillRemoveFromList()
    {
        var cageToRemove = new Cage { Id = 2 };
        var cages = await _inMemoryDB.CagesAsync();
        var expectedFinalCageCount = cages.Count() - 1;

        _inMemoryDB.Remove(cageToRemove);

        var updatedCages = await _inMemoryDB.CagesAsync();
        Assert.True(updatedCages.Count() == expectedFinalCageCount);
    }

    [Test]
    public async Task Cages_Remove_WhenInValidIdProvided_WillNotRemoveFromList()
    {
        var cageToRemove = new Cage { Id = 222 };
        var cages = await _inMemoryDB.CagesAsync();
        var expectedFinalCageCount = cages.Count();

        _inMemoryDB.Remove(cageToRemove);

        var updatedCages = await _inMemoryDB.CagesAsync();
        Assert.True(updatedCages.Count() == expectedFinalCageCount);
    }

    [Test]
    public async Task Cages_Update_WhenExistingItemFound_WillReplaceWithNewInstance()
    {
        var cages = await _inMemoryDB.CagesAsync();
        var cageToUpdate = cages.Where(x => x.Id == 2).First();

        cageToUpdate.Capacity = 2;

        _inMemoryDB.Update(cageToUpdate);

        var updatedCages = await _inMemoryDB.CagesAsync();
        var cageUpdated = updatedCages.Where(x => x.Id == 2).First();

        Assert.True(cageToUpdate.Capacity == 2);
    }

    [Test]
    public async Task Cages_Update_WhenExistingIsNotItemFound_WillAddNewInstance()
    {
        var cageToUpdate = new Cage { Id = 4, Capacity = 2 };
        var cages = await _inMemoryDB.CagesAsync();
        var expectedNewCagesCount = cages.Count() + 1;

        _inMemoryDB.Update(cageToUpdate);

        var updatedCages = await _inMemoryDB.CagesAsync();
        var newCagesCount = updatedCages.Count();

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
