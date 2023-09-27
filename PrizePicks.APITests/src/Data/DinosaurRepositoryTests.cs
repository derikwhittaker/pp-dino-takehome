using PrizePicks.API.Data;
using PrizePicks.API.Models;

namespace PrizePicks.API.Services;

[TestFixture]
public class DinosaurRepositoryTests
{
    private Mock<ILogger<DinosaurRepository>> _loggerMock = new Mock<ILogger<DinosaurRepository>>();
    private Mock<IDatabase> _databaseMock = new Mock<IDatabase>();

    private IDinosaurRepository _dinosaurRepository;

    [SetUp]
    public void Setup()
    {
        _databaseMock = new Mock<IDatabase>();

        _dinosaurRepository = new DinosaurRepository(_loggerMock.Object, _databaseMock.Object);
    }

    [Test]
    public async Task DinosaursAsync_Will_PullListFromUnderlyingDB()
    {
        var dinosaursUnderMock = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 1,
                Name = "Fred",
                Species = new Species()
            }
        };

        _databaseMock.Setup(db => db.DinosaursAsync().Result).Returns(dinosaursUnderMock);

        var dinosaurs = await _dinosaurRepository.DinosaursAsync();

        Assert.That(dinosaurs, Is.SameAs(dinosaursUnderMock));
    }

    [Test]
    public async Task DinosaurAsync_Will_PullItemFromUnderlyingDB()
    {
        int idUnderTest = 1;
        var dinosaurUnderTest = new Dinosaur
        {
            Id = idUnderTest,
            Name = "Fred",
            Species = new Species()
        };
        var dinosaursUnderMock = new List<IDinosaur> { dinosaurUnderTest };

        _databaseMock.Setup(db => db.DinosaursAsync().Result).Returns(dinosaursUnderMock);

        var dinosaur = await _dinosaurRepository.DinosaurAsync(idUnderTest);

        Assert.That(dinosaur, Is.SameAs(dinosaurUnderTest));
    }

    [Test]
    public async Task UpdateAsync_WhenNoIdProvided_WillAddId_N_Plus_One()
    {
        var dinoUnderTest = new Dinosaur { Name = "Fred", Species = new Species() };

        var dinosaursUnderMock = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 1,
                Name = "Fred",
                Species = new Species()
            }
        };

        _databaseMock.Setup(db => db.DinosaursAsync().Result).Returns(dinosaursUnderMock);

        var dinosaurs = await _dinosaurRepository.UpdateAsync(dinoUnderTest);

        Assert.That(dinosaurs.Id, Is.EqualTo(dinosaursUnderMock.Count() + 1));
    }

    [Test]
    public void UpdateAsync_WhenIdProvided_ButNotExisting_WillThrowException()
    {
        var dinoUnderTest = new Dinosaur
        {
            Id = 99,
            Name = "Fred",
            Species = new Species()
        };

        var dinosaursUnderMock = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 1,
                Name = "Fred",
                Species = new Species()
            }
        };

        _databaseMock.Setup(db => db.DinosaursAsync().Result).Returns(dinosaursUnderMock);

        Assert.ThrowsAsync<KeyNotFoundException>(
            async () => await _dinosaurRepository.UpdateAsync(dinoUnderTest)
        );
    }

    [Test]
    public async Task UpdateAsync_WhenIdProvided_IsFound_WillInvokeUpdate()
    {
        var dinoUnderTest = new Dinosaur
        {
            Id = 1,
            Name = "Fred Updated",
            Species = new Species()
        };

        var dinosaursUnderMock = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 1,
                Name = "Fred",
                Species = new Species()
            }
        };

        _databaseMock.Setup(db => db.DinosaursAsync().Result).Returns(dinosaursUnderMock);

        await _dinosaurRepository.UpdateAsync(dinoUnderTest);

        _databaseMock.Verify(x => x.UpdateAsync(dinoUnderTest), Times.Once());
    }
}
