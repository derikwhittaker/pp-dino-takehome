using Microsoft.Extensions.Logging;

using Moq;

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
}
