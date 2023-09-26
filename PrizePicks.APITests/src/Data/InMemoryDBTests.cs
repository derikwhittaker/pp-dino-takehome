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
        var expectedDinosoaurCount = 10;
        var expectedCageCount = 3;

        var species = _inMemoryDB.Species();
        var dinosaurs = await _inMemoryDB.DinosaursAsync();
        var cages = await _inMemoryDB.CagesAsync();

        Assert.True(species.Count() == expectedSpeciesCount);
        Assert.True(dinosaurs.Count() == expectedDinosoaurCount);
        Assert.True(cages.Count() == expectedCageCount);
    }

    [Test]
    public void Cages_Remove_WhenInvalidIdProvided_WillThrowException()
    {
        Assert.Throws<ArgumentException>(() => _inMemoryDB.RemoveAsync(new Cage()));
    }

    [Test]
    public async Task Cages_Remove_WhenValidIdProvided_WillRemoveFromList()
    {
        var cageToRemove = new Cage { Id = 2 };
        var cages = await _inMemoryDB.CagesAsync();
        var expectedFinalCageCount = cages.Count() - 1;

        _inMemoryDB.RemoveAsync(cageToRemove);

        var updatedCages = await _inMemoryDB.CagesAsync();
        Assert.True(updatedCages.Count() == expectedFinalCageCount);
    }

    [Test]
    public async Task Cages_Remove_WhenInValidIdProvided_WillNotRemoveFromList()
    {
        var cageToRemove = new Cage { Id = 222 };
        var cages = await _inMemoryDB.CagesAsync();
        var expectedFinalCageCount = cages.Count();

        _inMemoryDB.RemoveAsync(cageToRemove);

        var updatedCages = await _inMemoryDB.CagesAsync();
        Assert.True(updatedCages.Count() == expectedFinalCageCount);
    }

    [Test]
    public async Task Cages_Update_WhenExistingItemFound_WillReplaceWithNewInstance()
    {
        var cages = await _inMemoryDB.CagesAsync();
        var cageToUpdate = cages.Where(x => x.Id == 2).First();

        cageToUpdate.Capacity = 2;

        _inMemoryDB.UpdateAsync(cageToUpdate);

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

        _inMemoryDB.UpdateAsync(cageToUpdate);

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
        Assert.Throws<ArgumentException>(
            () => _inMemoryDB.RemoveAsync(new Dinosaur { Name = "Fred", Species = new Species() })
        );
    }

    [Test]
    public async Task Dinosaurs_Remove_WhenValidIdProvided_WillRemoveFromList()
    {
        var cageToRemove = new Dinosaur
        {
            Id = 2,
            Name = "Fred",
            Species = new Species()
        };

        var updatedDinos = await _inMemoryDB.DinosaursAsync();
        var expectedFinalDinosaurCount = updatedDinos.Count() - 1;

        _inMemoryDB.RemoveAsync(cageToRemove);

        var finalDinos = await _inMemoryDB.DinosaursAsync();
        Assert.True(finalDinos.Count() == expectedFinalDinosaurCount);
    }

    [Test]
    public async Task Dinosaurs_Remove_WhenInValidIdProvided_WillNotRemoveFromList()
    {
        var cageToRemove = new Dinosaur
        {
            Id = 222,
            Name = "Fred",
            Species = new Species()
        };
        var updatedDinos = await _inMemoryDB.DinosaursAsync();
        var expectedFinalDinosaurCount = updatedDinos.Count();

        _inMemoryDB.RemoveAsync(cageToRemove);

        var finalDinos = await _inMemoryDB.DinosaursAsync();
        Assert.True(finalDinos.Count() == expectedFinalDinosaurCount);
    }

    [Test]
    public async Task Dinosaurs_Update_WhenExistingItemFound_WillReplaceWithNewInstance()
    {
        var dinos = await _inMemoryDB.DinosaursAsync();
        var dinosaurToUpdate = dinos.Where(x => x.Id == 2).First();

        dinosaurToUpdate.Name = "Test Name";

        _inMemoryDB.UpdateAsync(dinosaurToUpdate);

        var finalDinos = await _inMemoryDB.DinosaursAsync();
        var cageUpdated = finalDinos.Where(x => x.Id == 2).First();

        Assert.True(dinosaurToUpdate.Name == "Test Name");
    }

    [Test]
    public async Task Dinosaurs_Update_WhenExistingIsNotItemFound_WillAddNewInstance()
    {
        var dinosuarToUpdate = new Dinosaur
        {
            Id = 44,
            Name = "TestName",
            Species = new Species()
        };
        var dinos = await _inMemoryDB.DinosaursAsync();
        var expectedNewDionsaurCount = dinos.Count() + 1;

        _inMemoryDB.UpdateAsync(dinosuarToUpdate);

        var finalDinos = await _inMemoryDB.DinosaursAsync();
        var newDinosaurCount = finalDinos.Count();

        Assert.True(
            newDinosaurCount == expectedNewDionsaurCount,
            $"Expected {newDinosaurCount} but had {expectedNewDionsaurCount}"
        );
    }
}
