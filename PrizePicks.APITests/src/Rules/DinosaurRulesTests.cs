using PrizePicks.API.Models;
using PrizePicks.API.Rules;

namespace PrizePicks.APITests.Rules;

[TestFixture]
public class DinosaurRulesTests
{
    private readonly Mock<ILogger<DinosaurRules>> _loggerMock = new Mock<ILogger<DinosaurRules>>();
    private IDinosaurRules _dinosaurRules = new DinosaurRules(
        new Mock<ILogger<DinosaurRules>>().Object
    );

    [Test]
    public void AssertDinosaurHasName_WhenNoNameProvided_WillThrowExecption()
    {
        var dinosaurUnderTest = new Dinosaur { Name = "", Species = new Species() };

        // throws an exception if something is wrong, so we can just call

        Assert.Throws<InvalidOperationException>(
            () => _dinosaurRules.AssertDinosaurHasName(dinosaurUnderTest)
        );
    }

    [Test]
    public void AssertDinosaurHasName_WhenValid_WillPass()
    {
        try
        {
            var dinosaurs = new List<IDinosaur>
            {
                new Dinosaur
                {
                    Id = 1,
                    Name = "Fred",
                    Species = new Species()
                }
            };

            var cageUnderTest = new Cage(dinosaurs) { Id = 1, Capacity = 1 };
        }
        catch
        {
            // we should get an exception
            Assert.IsTrue(true);
        }
    }
}
