using Microsoft.AspNetCore.Mvc;

using PrizePicks.API.Models;
using PrizePicks.API.Services;

namespace PrizePicks.WebAPI.Controllers;

[TestFixture]
public class DinosaursControllerTests
{
    private readonly Mock<ILogger<DinosaursController>> _loggerMock =
        new();
    private Mock<IDinosaurService> _dinosaurServiceMock = new();

    private DinosaursController _dinosaurController;

    [SetUp]
    public void Setup()
    {
        _dinosaurServiceMock = new Mock<IDinosaurService>();

        _dinosaurController = new DinosaursController(
            _loggerMock.Object,
            _dinosaurServiceMock.Object
        );
    }

    [Test]
    public async Task GetAllAsync_WillInvokeUnderlyingServiceAsExpected()
    {
        await _dinosaurController.GetAllAsync();

        _dinosaurServiceMock.Verify(x => x.DinosaursAsync(), Times.Once());
    }

    [Test]
    public async Task GetAllAsync_WhenServiceInvoked_WillOkResultWithFoundCages()
    {
        var dinosUnderMock = new List<IDinosaur>
        {
            new Dinosaur()
            {
                Id = 100,
                Name = "Fred",
                Species = new Species()
            }
        };

        _dinosaurServiceMock
            .Setup(x => x.DinosaursAsync().Result)
            .Returns(dinosUnderMock as IEnumerable<IDinosaur>);

        ActionResult<IEnumerable<IDinosaur>> invokeResult = await _dinosaurController.GetAllAsync();

        Assert.IsInstanceOf<OkObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task GetSingleAsync_WillInvokeUnderlyingServiceAsExpected()
    {
        var idUnderTest = 1;

        await _dinosaurController.GetSingleAsync(idUnderTest);

        _dinosaurServiceMock.Verify(x => x.DinosaurAsync(idUnderTest), Times.Once());
    }

    [Test]
    public async Task GetSingleAsync_WhenIdProvided_WillInvokeUnderlyingServiceAsExpected()
    {
        var idUnderTest = 1;
        var dinoUnderMock = new Dinosaur
        {
            Id = 100,
            Name = "Fred",
            Species = new Species()
        };

        _dinosaurServiceMock.Setup(x => x.DinosaurAsync(idUnderTest).Result).Returns(dinoUnderMock);

        ActionResult<IDinosaur> invokeResult = await _dinosaurController.GetSingleAsync(
            idUnderTest
        );

        Assert.IsInstanceOf<OkObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task GetSingleAsync_WhenInvalidIdProvided_WillReturn404Result()
    {
        var idUnderTest = 1;
        var dinoUnderMock = new Dinosaur
        {
            Id = 100,
            Name = "Fred",
            Species = new Species()
        };

        // force the known exception that will be thrown
        _dinosaurServiceMock
            .Setup(x => x.DinosaurAsync(idUnderTest).Result)
            .Throws<KeyNotFoundException>();

        ActionResult<IDinosaur> invokeResult = await _dinosaurController.GetSingleAsync(
            idUnderTest
        );

        Assert.IsInstanceOf<NotFoundObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task CreateAsync_WillInvokeUnderlyingServiceAsExpected()
    {
        var dinoUnderTest = new Dinosaur { Name = "Fred", Species = new Species() };

        await _dinosaurController.CreateAsync(dinoUnderTest);

        _dinosaurServiceMock.Verify(x => x.CreateAsync(dinoUnderTest), Times.Once());
    }

    [Test]
    public async Task CreateAsync_WhenValidProvided_WillInvokeUnderlyingServiceAsExpected()
    {
        var idUnderTest = 1;

        var dinoUnderTest = new Dinosaur { Name = "Fred", Species = new Species() };
        var dinoUnderMock = new Dinosaur
        {
            Id = idUnderTest,
            Name = "Fred",
            Species = new Species()
        };

        _dinosaurServiceMock.Setup(x => x.CreateAsync(dinoUnderTest).Result).Returns(dinoUnderMock);

        ActionResult<IDinosaur> invokeResult = await _dinosaurController.CreateAsync(dinoUnderTest);

        Assert.IsInstanceOf<OkObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task CreatAsync_WhenInvalidIdProvided_WillReturn404Result()
    {
        var dinoUnderTest = new Dinosaur { Name = "Fred", Species = new Species() };

        // force the known exception that will be thrown
        _dinosaurServiceMock
            .Setup(x => x.CreateAsync(dinoUnderTest).Result)
            .Throws<InvalidOperationException>();

        ActionResult<IDinosaur> invokeResult = await _dinosaurController.CreateAsync(dinoUnderTest);

        Assert.IsInstanceOf<BadRequestObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task UpdateAsync_WillInvokeUnderlyingServiceAsExpected()
    {
        var idUnderTest = 1;
        var dinoUnderTest = new Dinosaur
        {
            Id = idUnderTest,
            Name = "Fred",
            Species = new Species()
        };

        await _dinosaurController.UpdateAsync(dinoUnderTest);

        _dinosaurServiceMock.Verify(x => x.UpdateAsync(dinoUnderTest), Times.Once());
    }

    [Test]
    public async Task UpdateAsync_WhenValidProvided_WillReturnValidAsExpected()
    {
        var idUnderTest = 1;
        var dinoUnderTest = new Dinosaur
        {
            Id = idUnderTest,
            Name = "Fred",
            Species = new Species()
        };
        var dinoUnderMock = new Dinosaur
        {
            Id = idUnderTest,
            Name = "Fred",
            Species = new Species()
        };

        _dinosaurServiceMock.Setup(x => x.UpdateAsync(dinoUnderTest).Result).Returns(dinoUnderMock);

        ActionResult<IDinosaur> invokeResult = await _dinosaurController.CreateAsync(dinoUnderTest);

        Assert.IsInstanceOf<OkObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task UpdateAsync_WhenInvalidObjectProvided_WillReturn404Result()
    {
        var idUnderTest = 1;
        var dinoUnderTest = new Dinosaur
        {
            Id = idUnderTest,
            Name = "Fred",
            Species = new Species()
        };

        // force the known exception that will be thrown
        _dinosaurServiceMock
            .Setup(x => x.UpdateAsync(dinoUnderTest).Result)
            .Throws<InvalidOperationException>();

        ActionResult<IDinosaur> invokeResult = await _dinosaurController.UpdateAsync(dinoUnderTest);

        Assert.IsInstanceOf<BadRequestObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task UpdateAsync_WhenInvalidIdProvided_WillReturn404Result()
    {
        var idUnderTest = 1;
        var dinoUnderTest = new Dinosaur
        {
            Id = idUnderTest,
            Name = "Fred",
            Species = new Species()
        };

        // force the known exception that will be thrown
        _dinosaurServiceMock
            .Setup(x => x.UpdateAsync(dinoUnderTest).Result)
            .Throws<KeyNotFoundException>();

        var invokeResult = await _dinosaurController.UpdateAsync(dinoUnderTest);

        Assert.IsInstanceOf<NotFoundObjectResult>(invokeResult.Result);
    }
}
