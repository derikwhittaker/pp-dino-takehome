using Microsoft.Extensions.Logging;

using Moq;

using PrizePicks.API.Data;
using PrizePicks.API.Models;
using PrizePicks.API.Rules;

namespace PrizePicks.API.Services;

[TestFixture]
public class DinosaurServiceTests
{
    private Mock<ILogger<DinosaurService>> _loggerMock = new Mock<ILogger<DinosaurService>>();
    private Mock<IDinosaurRepository> _dinosaurRepositoryMock = new Mock<IDinosaurRepository>();

    private IDinosaurService _dinosaurService;

    [SetUp]
    public void Setup()
    {
        _dinosaurRepositoryMock = new Mock<IDinosaurRepository>();
        _dinosaurRepositoryMock = new Mock<IDinosaurRepository>();

        _dinosaurService = new DinosaurService(_loggerMock.Object, _dinosaurRepositoryMock.Object);
    }

    [Test]
    public async Task DinosaursAsync_WillPullFromRepository_WillReturnResults()
    {
        var DinosaursMocked = new List<IDinosaur>
        {
            new Dinosaur()
            {
                Id = 100,
                Name = "Fred",
                Species = new Species()
            }
        };

        _dinosaurRepositoryMock.Setup(x => x.DinosaursAsync().Result).Returns(DinosaursMocked);

        var returnedDinosaurs = await _dinosaurService.DinosaursAsync();

        Assert.IsTrue(returnedDinosaurs.Count() == 1);
    }

    [Test]
    public async Task DinosaurAsync_WhenInvoked_WillUseProvidedIdToFetchDataFromRepository()
    {
        var idUnderTest = 100;

        var returnedDinosaurs = await _dinosaurService.DinosaurAsync(idUnderTest);

        _dinosaurRepositoryMock.Verify(x => x.DinosaurAsync(idUnderTest), Times.Once());
    }
}
