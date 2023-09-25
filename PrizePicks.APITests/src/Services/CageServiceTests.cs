using System.Data;
using Microsoft.Extensions.Logging;

using Moq;

using PrizePicks.API.Data;
using PrizePicks.API.Models;
using PrizePicks.API.Rules;
using PrizePicks.API.Services;

[TestFixture]
public class CageServiceTests
{
    private Mock<ILogger<CageService>> _loggerMock = new Mock<ILogger<CageService>>();
    private Mock<ICageRepository> _cageRepository = new Mock<ICageRepository>();
    private Mock<ICageRules> _cageRules = new Mock<ICageRules>();

    private ICageService _cageService;

    [SetUp]
    public void Setup()
    {
        _cageRepository = new Mock<ICageRepository>();
        _cageRules = new Mock<ICageRules>();

        _cageService = new CageService(
            _loggerMock.Object,
            _cageRules.Object,
            _cageRepository.Object
        );
    }

    [Test]
    public async Task AssociateDinosaurAsync_WhenAllRulesPass_WillAttmptDBUpdate()
    {
        var cageUnderTest = new Cage();
        var dinoUnderTest = new Dinosaur
        {
            Id = 1,
            Name = "Fred",
            Species = new Species(FoodType.Carnivore, SpeciesType.Tyrannosaurus)
        };

        _cageRules.Setup(rule => rule.IsPoweredOn(cageUnderTest));
        _cageRules.Setup(rule => rule.IsCageAtCapacity(cageUnderTest));
        _cageRules.Setup(rule => rule.IsDinoValidForCage(cageUnderTest, dinoUnderTest));

        // Goal is to get the cage value that was pushed into the repo
        //  we want to validat the the provided ino was added correctly
        ICage passedCage = new Cage();
        _cageRepository
            .Setup(x => x.Update(It.IsAny<ICage>()))
            .Callback(new InvocationAction(i => passedCage = (ICage)i.Arguments[0]));

        await _cageService.AssociateDinosaurAsync(cageUnderTest, dinoUnderTest);

        Assert.True(passedCage.Dinosaurs.Count() == 1);
        Assert.True(passedCage.Dinosaurs.First().Id == dinoUnderTest.Id);
        Assert.True(passedCage.Dinosaurs.First().Name == dinoUnderTest.Name);
    }

    [Test]
    public async Task AssociateDinosaurAsync_WhenAnyRuleFailes_WillThrowException()
    {
        var cageUnderTest = new Cage();
        var dinoUnderTest = new Dinosaur
        {
            Id = 1,
            Name = "Fred",
            Species = new Species(FoodType.Carnivore, SpeciesType.Tyrannosaurus)
        };

        _cageRules
            .Setup(rule => rule.IsPoweredOn(cageUnderTest))
            .Throws<InvalidOperationException>();
        _cageRules.Setup(rule => rule.IsCageAtCapacity(cageUnderTest));
        _cageRules.Setup(rule => rule.IsDinoValidForCage(cageUnderTest, dinoUnderTest));

        // Goal is to get the cage value that was pushed into the repo
        //  we want to validat the the provided ino was added correctly
        ICage passedCage = new Cage();
        _cageRepository
            .Setup(x => x.Update(It.IsAny<ICage>()))
            .Callback(new InvocationAction(i => passedCage = (ICage)i.Arguments[0]));

        Assert.ThrowsAsync<InvalidOperationException>(
            async () => await _cageService.AssociateDinosaurAsync(cageUnderTest, dinoUnderTest)
        );
    }
}
