using Microsoft.Extensions.Logging;

using Moq;

using PrizePicks.API.Data;
using PrizePicks.API.Models;
using PrizePicks.API.Rules;

namespace PrizePicks.API.Services;

[TestFixture]
public class CageServiceTests
{
    private Mock<ILogger<CageService>> _loggerMock = new Mock<ILogger<CageService>>();
    private Mock<ICageRepository> _cageRepositoryMock = new Mock<ICageRepository>();
    private Mock<IDinosaurRepository> _dinosaurRepositoryMock = new Mock<IDinosaurRepository>();
    private Mock<ICageRules> _cageRulesMock = new Mock<ICageRules>();

    private ICageService _cageService;

    [SetUp]
    public void Setup()
    {
        _cageRepositoryMock = new Mock<ICageRepository>();
        _dinosaurRepositoryMock = new Mock<IDinosaurRepository>();
        _cageRulesMock = new Mock<ICageRules>();

        _cageService = new CageService(
            _loggerMock.Object,
            _cageRulesMock.Object,
            _cageRepositoryMock.Object,
            _dinosaurRepositoryMock.Object
        );
    }

    [Test]
    public async Task AssociateDinosaurAsync_WhenAllRulesPass_WillAttmptDBUpdate()
    {
        int cageIdUnderTest = 1;
        var cageUnderTest = new Cage { Id = cageIdUnderTest };
        var dinoIdUnderTest = 1;
        var dinoUnderTest = new Dinosaur
        {
            Id = dinoIdUnderTest,
            Name = "Fred",
            Species = new Species(FoodType.Carnivore, SpeciesType.Tyrannosaurus)
        };

        _cageRepositoryMock
            .Setup(db => db.CageAsync(cageIdUnderTest).Result)
            .Returns(cageUnderTest);
        _dinosaurRepositoryMock
            .Setup(db => db.DinosaurAsync(dinoIdUnderTest).Result)
            .Returns(dinoUnderTest);
        _cageRulesMock.Setup(rule => rule.AssertCageIsPoweredOn(cageUnderTest));
        _cageRulesMock.Setup(rule => rule.AssertCageNotAtCapacity(cageUnderTest));
        _cageRulesMock.Setup(rule => rule.AssertDinoValidForCage(cageUnderTest, dinoUnderTest));

        // Goal is to get the cage value that was pushed into the repo
        //  we want to validat the the provided ino was added correctly
        ICage passedCage = new Cage();
        _cageRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<ICage>()))
            .Callback(new InvocationAction(i => passedCage = (ICage)i.Arguments[0]));

        await _cageService.AssociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest);

        Assert.True(passedCage.Dinosaurs.Count() == 1);
        Assert.True(passedCage.Dinosaurs.First().Id == dinoUnderTest.Id);
        Assert.True(passedCage.Dinosaurs.First().Name == dinoUnderTest.Name);
    }

    [Test]
    public async Task AssociateDinosaurAsync_WhenValid_WillReturnUpdatedCage()
    {
        int cageIdUnderTest = 1;
        var cageUnderTest = new Cage { Id = cageIdUnderTest };
        var dinoIdUnderTest = 1;
        var dinoUnderTest = new Dinosaur
        {
            Id = dinoIdUnderTest,
            Name = "Fred",
            Species = new Species(FoodType.Carnivore, SpeciesType.Tyrannosaurus)
        };

        _cageRepositoryMock
            .Setup(db => db.CageAsync(cageIdUnderTest).Result)
            .Returns(cageUnderTest);
        _dinosaurRepositoryMock
            .Setup(db => db.DinosaurAsync(dinoIdUnderTest).Result)
            .Returns(dinoUnderTest);
        _cageRulesMock.Setup(rule => rule.AssertCageIsPoweredOn(cageUnderTest));
        _cageRulesMock.Setup(rule => rule.AssertCageNotAtCapacity(cageUnderTest));
        _cageRulesMock.Setup(rule => rule.AssertDinoValidForCage(cageUnderTest, dinoUnderTest));

        var updatedCage = await _cageService.AssociateDinosaurAsync(
            cageIdUnderTest,
            dinoIdUnderTest
        );

        Assert.True(updatedCage.Dinosaurs.Count() == 1);
        Assert.True(updatedCage.Dinosaurs.First().Id == dinoUnderTest.Id);
        Assert.True(updatedCage.Dinosaurs.First().Name == dinoUnderTest.Name);
    }

    [Test]
    public void AssociateDinosaurAsync_IdNotValid_WillThrowException()
    {
        int cageIdUnderTest = 1;
        var cageUnderTest = new Cage { Id = cageIdUnderTest };
        var dinoIdUnderTest = 1;
        var dinoUnderTest = new Dinosaur
        {
            Id = dinoIdUnderTest,
            Name = "Fred",
            Species = new Species(FoodType.Carnivore, SpeciesType.Tyrannosaurus)
        };

        _cageRepositoryMock
            .Setup(db => db.CageAsync(cageIdUnderTest).Result)
            .Throws<KeyNotFoundException>();

        // Goal is to get the cage value that was pushed into the repo
        //  we want to validat the the provided ino was added correctly
        ICage passedCage = new Cage();
        _cageRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<ICage>()))
            .Callback(new InvocationAction(i => passedCage = (ICage)i.Arguments[0]));

        Assert.ThrowsAsync<KeyNotFoundException>(
            async () => await _cageService.AssociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest)
        );
    }

    [Test]
    public void AssociateDinosaurAsync_WhenAnyRuleFaile_WillThrowException()
    {
        int cageIdUnderTest = 1;
        var cageUnderTest = new Cage { Id = cageIdUnderTest };
        var dinoIdUnderTest = 1;
        var dinoUnderTest = new Dinosaur
        {
            Id = dinoIdUnderTest,
            Name = "Fred",
            Species = new Species(FoodType.Carnivore, SpeciesType.Tyrannosaurus)
        };

        _cageRepositoryMock
            .Setup(db => db.CageAsync(cageIdUnderTest).Result)
            .Returns(cageUnderTest);
        _cageRulesMock
            .Setup(rule => rule.AssertCageIsPoweredOn(cageUnderTest))
            .Throws<InvalidOperationException>();
        _cageRulesMock.Setup(rule => rule.AssertCageNotAtCapacity(cageUnderTest));
        _cageRulesMock.Setup(rule => rule.AssertDinoValidForCage(cageUnderTest, dinoUnderTest));

        // Goal is to get the cage value that was pushed into the repo
        //  we want to validat the the provided ino was added correctly
        ICage passedCage = new Cage();
        _cageRepositoryMock
            .Setup(x => x.UpdateAsync(It.IsAny<ICage>()))
            .Callback(new InvocationAction(i => passedCage = (ICage)i.Arguments[0]));

        Assert.ThrowsAsync<InvalidOperationException>(
            async () => await _cageService.AssociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest)
        );
    }

    [Test]
    public async Task UpdatePowerStatus_WhenPoweringDown_And_HasDinosaurs_WillThrowException()
    {
        var idUnderTest = 99;
        var dinosaurs = new List<IDinosaur>
        {
            new Dinosaur { Name = "Fred", Species = new Species() }
        };
        var cage = new Cage(dinosaurs) { Id = idUnderTest, PowerStatus = PowerStatusType.Active };

        // force cage repo to pull back the right cage for us]]

        _cageRepositoryMock.Setup(x => x.CageAsync(It.IsAny<int>()).Result).Returns(cage);
        _cageRulesMock.Setup(x => x.IsAbleToBePoweredDown(It.IsAny<ICage>())).Returns(false);

        // setup -> create the cage we want to work with
        await _cageService.Create(cage);

        Assert.ThrowsAsync<InvalidOperationException>(
            async () => await _cageService.UpdatePowerStatus(idUnderTest, PowerStatusType.Down)
        );
    }

    [Test]
    public async Task UpdatePowerStatus_WhenPoweringDown_InvalidState_WillReturnUpdatedCage()
    {
        var idUnderTest = 100;
        var cage = new Cage() { Id = idUnderTest, PowerStatus = PowerStatusType.Active };

        // force cage repo to pull back the right cage for us
        _cageRepositoryMock.Setup(x => x.CageAsync(It.IsAny<int>()).Result).Returns(cage);
        _cageRulesMock.Setup(x => x.IsAbleToBePoweredDown(It.IsAny<ICage>())).Returns(true);

        // setup -> create the cage we want to work with
        await _cageService.Create(cage);

        var updatedCage = await _cageService.UpdatePowerStatus(idUnderTest, PowerStatusType.Down);
    }

    [Test]
    public async Task Create_WhenCageIsProvided_WillInvokeUpdateOnRepository()
    {
        var idUnderTest = 100;
        var cage = new Cage() { Id = idUnderTest, PowerStatus = PowerStatusType.Active };

        await _cageService.Create(cage);

        _cageRepositoryMock.Verify(x => x.UpdateAsync(cage), Times.Once());
    }

    [Test]
    public async Task Create_WhenCageNotPoweredOn_WillNotInvokeUpdate()
    {
        var idUnderTest = 100;
        var cage = new Cage() { Id = idUnderTest, PowerStatus = PowerStatusType.Down };

        _cageRulesMock
            .Setup(x => x.AssertCageIsPoweredOn(cage))
            .Throws<InvalidOperationException>();

        try
        {
            await _cageService.Create(cage);
        }
        catch
        {
            // kinda cheating the system, but we want to validate the update is NOT called
            _cageRepositoryMock.Verify(x => x.UpdateAsync(cage), Times.Never());
        }
    }

    [Test]
    public async Task CagesAsync_WillPullFromRepository_WillReturnResults()
    {
        var cagesMocked = new List<ICage>
        {
            new Cage() { Id = 100, PowerStatus = PowerStatusType.Active }
        };

        _cageRepositoryMock.Setup(x => x.CagesAsync().Result).Returns(cagesMocked);

        var returnedCages = await _cageService.CagesAsync();

        Assert.IsTrue(returnedCages.Count() == 1);
    }

    [Test]
    public async Task CageAsync_WhenInvoked_WillUseProvidedIdToFetchDataFromRepository()
    {
        var idUnderTest = 100;

        var returnedCages = await _cageService.CageAsync(idUnderTest);

        _cageRepositoryMock.Verify(x => x.CageAsync(idUnderTest), Times.Once());
    }

    [Test]
    public async Task CageAsync_WhenInvoked_WillReturnCageFromRepository()
    {
        var idUnderTest = 100;
        var cage = new Cage() { Id = idUnderTest, PowerStatus = PowerStatusType.Active };

        _cageRepositoryMock.Setup(x => x.CageAsync(idUnderTest).Result).Returns(cage);

        var returnedCages = await _cageService.CageAsync(idUnderTest);

        Assert.That(returnedCages, Is.SameAs(cage));
    }

    [Test]
    public async Task UpdateAsync_WhenCageIsProvided_WillInvokeUpdateOnRepository()
    {
        var idUnderTest = 100;
        var cage = new Cage() { Id = idUnderTest, PowerStatus = PowerStatusType.Active };

        _cageRulesMock.Setup(x => x.AssertCageIsPoweredOn(cage));

        await _cageService.UpdateAsync(cage);

        _cageRepositoryMock.Verify(x => x.UpdateAsync(cage), Times.Once());
    }

    [Test]
    public async Task UpdateAsync_WhenCageNotPoweredOn_WillNotInvokeUpdate()
    {
        var idUnderTest = 100;
        var cage = new Cage() { Id = idUnderTest, PowerStatus = PowerStatusType.Down };

        _cageRulesMock
            .Setup(x => x.AssertCageIsPoweredOn(cage))
            .Throws<InvalidOperationException>();

        try
        {
            await _cageService.UpdateAsync(cage);
        }
        catch
        {
            // kinda cheating the system, but we want to validate the update is NOT called
            _cageRepositoryMock.Verify(x => x.UpdateAsync(cage), Times.Never());
        }
    }
}
