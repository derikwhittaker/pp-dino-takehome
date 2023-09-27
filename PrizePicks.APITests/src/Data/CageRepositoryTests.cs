using PrizePicks.API.Models;

namespace PrizePicks.API.Data;

[TestFixture]
public class CageRepositoryTests
{
    private Mock<ILogger<CageRepository>> _loggerMock = new Mock<ILogger<CageRepository>>();
    private Mock<IDatabase> _databaseMock = new Mock<IDatabase>();

    private ICageRepository _cageRepository;

    [SetUp]
    public void Setup()
    {
        _databaseMock = new Mock<IDatabase>();

        _cageRepository = new CageRepository(_loggerMock.Object, _databaseMock.Object);
    }

    [Test]
    public async Task CagesAsync_Will_PullListFromUnderlyingDB()
    {
        var CagesUnderMock = new List<ICage>
        {
            new Cage { Id = 1, Capacity = 1 }
        };

        _databaseMock.Setup(db => db.CagesAsync().Result).Returns(CagesUnderMock);

        var Cages = await _cageRepository.CagesAsync();

        Assert.That(Cages, Is.SameAs(CagesUnderMock));
    }

    [Test]
    public async Task CageAsync_Will_PullItemFromUnderlyingDB()
    {
        int idUnderTest = 1;
        var cageUnderTest = new Cage { Id = 1, Capacity = 1 };

        var cagesUnderMock = new List<ICage> { cageUnderTest };

        _databaseMock.Setup(db => db.CagesAsync().Result).Returns(cagesUnderMock);

        var cage = await _cageRepository.CageAsync(idUnderTest);

        Assert.That(cage, Is.SameAs(cageUnderTest));
    }

    [Test]
    public async Task UpdateAsync_WhenNoIdProvided_WillAddId_N_Plus_One()
    {
        var cageUnderTest = new Cage { Capacity = 1 };

        var cagesUnderMock = new List<ICage>
        {
            new Cage { Id = 1, Capacity = 1 }
        };

        _databaseMock.Setup(db => db.CagesAsync().Result).Returns(cagesUnderMock);

        var cages = await _cageRepository.UpdateAsync(cageUnderTest);

        Assert.That(cages.Id, Is.EqualTo(cagesUnderMock.Count() + 1));
    }

    [Test]
    public void UpdateAsync_WhenIdProvided_ButNotExisting_WillThrowException()
    {
        var cageUnderTest = new Cage { Id = 99, Capacity = 1 };

        var CagesUnderMock = new List<ICage>
        {
            new Cage { Id = 1, Capacity = 1 }
        };

        _databaseMock.Setup(db => db.CagesAsync().Result).Returns(CagesUnderMock);

        Assert.ThrowsAsync<KeyNotFoundException>(
            async () => await _cageRepository.UpdateAsync(cageUnderTest)
        );
    }

    [Test]
    public async Task UpdateAsync_WhenIdProvided_IsFound_WillInvokeUpdate()
    {
        var cageUnderTest = new Cage { Id = 1, Capacity = 1 };

        var cagesUnderMock = new List<ICage>
        {
            new Cage { Id = 1, Capacity = 1 }
        };

        _databaseMock.Setup(db => db.CagesAsync().Result).Returns(cagesUnderMock);

        await _cageRepository.UpdateAsync(cageUnderTest);

        _databaseMock.Verify(x => x.UpdateAsync(cageUnderTest), Times.Once());
    }
}
