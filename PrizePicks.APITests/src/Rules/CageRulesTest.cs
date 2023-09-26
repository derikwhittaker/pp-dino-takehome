using Microsoft.Extensions.Logging;

using Moq;

using PrizePicks.API.Models;
using PrizePicks.API.Rules;

namespace PrizePicks.APITests.Rules;

[TestFixture]
public class CageRulesTests
{
    private readonly Mock<ILogger<CageRulesTests>> _loggerMock =
        new Mock<ILogger<CageRulesTests>>();
    private ICageRules _cageRules = new CageRules(new Mock<ILogger<CageRules>>().Object);

    [Test]
    public void IsCageAtCapacity_When_CageCapacity_HasNotBeenReached_WillPass()
    {
        var dinosaurs = new List<IDinosaur> { new Dinosaur { Id = 1 } };

        var cageUnderTest = new Cage(dinosaurs) { Id = 1, Capacity = 2 };

        // throws an exception if something is wrong, so we can just call
        _cageRules.AssertCageNotAtCapacity(cageUnderTest);
    }

    [Test]
    public void IsCageAtCapacity_When_CageCapacity_HasBeenReached_WillPass()
    {
        var dinosaurs = new List<IDinosaur> { new Dinosaur { Id = 1 } };

        var cageUnderTest = new Cage(dinosaurs) { Id = 1, Capacity = 1 };

        // throws an exception if something is wrong
        Assert.Throws<InvalidOperationException>(
            () => _cageRules.AssertCageNotAtCapacity(cageUnderTest)
        );
    }

    [Test]
    public void IsPoweredOn_When_CagePowerStatus_IsActive_WillPass()
    {
        var cageUnderTest = new Cage { Id = 1, PowerStatus = PowerStatusType.Active };

        // throws an exception if something is wrong, so we can just call
        _cageRules.AssertCageIsPoweredOn(cageUnderTest);
    }

    [Test]
    public void IsPoweredOn_When_CagePowerStatus_IsDown_WillFalse()
    {
        var cageUnderTest = new Cage { Id = 1, PowerStatus = PowerStatusType.Down };

        // throws an exception if something is wrong
        Assert.Throws<InvalidOperationException>(
            () => _cageRules.AssertCageIsPoweredOn(cageUnderTest)
        );
    }

    [Test]
    public void IsDinoValidForCage_When_CageIsEmpty_WillPass()
    {
        var dinosaurs = new List<IDinosaur>();
        var dinoUnderTest = new Dinosaur
        {
            Id = 1,
            Species = new Species(FoodType.Carnivore, SpeciesType.Tyrannosaurus)
        };
        var cageUnderTest = new Cage(dinosaurs) { Id = 1, PowerStatus = PowerStatusType.Active };

        // throws an exception if something is wrong, so we can just call
        _cageRules.AssertDinoValidForCage(cageUnderTest, dinoUnderTest);
    }

    [Test]
    public void IsDinoValidForCage_When_CageHasExisting_ButOfSameFoodType_WillPass()
    {
        var dinosaurs = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 2,
                Species = new Species(FoodType.Herbivore, SpeciesType.Triceratops)
            }
        };

        var dinoUnderTest = new Dinosaur
        {
            Id = 1,
            Species = new Species(FoodType.Herbivore, SpeciesType.Ankylosaurus)
        };
        var cageUnderTest = new Cage(dinosaurs) { Id = 1, PowerStatus = PowerStatusType.Active };

        // throws an exception if something is wrong, so we can just call
        _cageRules.AssertDinoValidForCage(cageUnderTest, dinoUnderTest);
    }

    [Test]
    public void IsDinoValidForCage_When_CageHasExisting_ButDifferentFoodType_WillFail()
    {
        var dinosaurs = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 2,
                Species = new Species(FoodType.Herbivore, SpeciesType.Triceratops)
            }
        };

        var dinoUnderTest = new Dinosaur
        {
            Id = 1,
            Species = new Species(FoodType.Carnivore, SpeciesType.Tyrannosaurus)
        };
        var cageUnderTest = new Cage(dinosaurs) { Id = 1, PowerStatus = PowerStatusType.Active };

        // throws an exception if something is wrong, so we can just call
        Assert.Throws<InvalidOperationException>(
            () => _cageRules.AssertDinoValidForCage(cageUnderTest, dinoUnderTest)
        );
    }

    [Test]
    public void IsDinoValidForCage_When_CageHasExistingCarnivore_NewIsSameSpeciesAsExisting_WillPass()
    {
        var dinosaurs = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 2,
                Species = new Species(FoodType.Carnivore, SpeciesType.Tyrannosaurus)
            }
        };

        var dinoUnderTest = new Dinosaur
        {
            Id = 1,
            Species = new Species(FoodType.Carnivore, SpeciesType.Tyrannosaurus)
        };
        var cageUnderTest = new Cage(dinosaurs) { Id = 1, PowerStatus = PowerStatusType.Active };

        // throws an exception if something is wrong, so we can just call
        _cageRules.AssertDinoValidForCage(cageUnderTest, dinoUnderTest);
    }

    [Test]
    public void IsDinoValidForCage_When_CageHasExistingCarnivore_NewIsDifferentSpeciesAsExisting_WillFail()
    {
        var dinosaurs = new List<IDinosaur>
        {
            new Dinosaur
            {
                Id = 2,
                Species = new Species(FoodType.Carnivore, SpeciesType.Tyrannosaurus)
            }
        };

        var dinoUnderTest = new Dinosaur
        {
            Id = 1,
            Species = new Species(FoodType.Carnivore, SpeciesType.Velociraptor)
        };
        var cageUnderTest = new Cage(dinosaurs) { Id = 1, PowerStatus = PowerStatusType.Active };

        // throws an exception if something is wrong, so we can just call
        Assert.Throws<InvalidOperationException>(
            () => _cageRules.AssertDinoValidForCage(cageUnderTest, dinoUnderTest)
        );
    }
}
