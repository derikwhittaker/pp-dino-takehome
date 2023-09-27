using Microsoft.AspNetCore.Mvc;

using PrizePicks.API.Models;
using PrizePicks.API.Services;

using PrizePicks.WebAPI.Controllers;

namespace PrizePicks.WebAPITests.Controllers;

[TestFixture]
public class CageControllerTests
{
    private readonly Mock<ILogger<CagesController>> _loggerMock = new();
    private Mock<ICageService> _cageServiceMock = new();

    private CagesController _cageController;

    [SetUp]
    public void Setup()
    {
        _cageServiceMock = new Mock<ICageService>();

        _cageController = new CagesController(_loggerMock.Object, _cageServiceMock.Object);
    }

    [Test]
    public async Task GetAllAsync_WillInvokeUnderlyingServiceAsExpected()
    {
        await _cageController.GetAllAsync();

        _cageServiceMock.Verify(x => x.CagesAsync(), Times.Once());
    }

    [Test]
    public async Task GetAllAsync_WhenServiceInvoked_WillOkResultWithFoundCages()
    {
        var cagesUnderMock = new List<ICage>
        {
            new Cage { Id = 1, Capacity = 1, }
        };

        _cageServiceMock
            .Setup(x => x.CagesAsync().Result)
            .Returns(cagesUnderMock as IEnumerable<ICage>);

        ActionResult<IEnumerable<ICage>> invokeResult = await _cageController.GetAllAsync();

        Assert.IsInstanceOf<OkObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task GetSingleAsync_WillInvokeUnderlyingServiceAsExpected()
    {
        var idUnderTest = 1;

        await _cageController.GetSingleAsync(idUnderTest);

        _cageServiceMock.Verify(x => x.CageAsync(idUnderTest), Times.Once());
    }

    [Test]
    public async Task GetSingleAsync_WhenIdProvided_WillInvokeUnderlyingServiceAsExpected()
    {
        var idUnderTest = 1;
        var cageUnderMock = new Cage { Id = 1, Capacity = 1, };

        _cageServiceMock.Setup(x => x.CageAsync(idUnderTest).Result).Returns(cageUnderMock);

        ActionResult<ICage> invokeResult = await _cageController.GetSingleAsync(idUnderTest);

        Assert.IsInstanceOf<OkObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task GetSingleAsync_WhenInvalidIdProvided_WillReturn404Result()
    {
        var idUnderTest = 1;
        var cageUnderMock = new Cage { Id = 1, Capacity = 1, };

        // force the known exception that will be thrown
        _cageServiceMock
            .Setup(x => x.CageAsync(idUnderTest).Result)
            .Throws<KeyNotFoundException>();

        ActionResult<ICage> invokeResult = await _cageController.GetSingleAsync(idUnderTest);

        Assert.IsInstanceOf<NotFoundObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task CreateAsync_WillInvokeUnderlyingServiceAsExpected()
    {
        var idUnderTest = 1;
        var cageUnderTest = new Cage { Id = idUnderTest, Capacity = 1, };

        await _cageController.CreateAsync(cageUnderTest);

        _cageServiceMock.Verify(x => x.CreatAsync(cageUnderTest), Times.Once());
    }

    [Test]
    public async Task CreateAsync_WhenValidProvided_WillInvokeUnderlyingServiceAsExpected()
    {
        var idUnderTest = 1;
        var cageUnderTest = new Cage { Capacity = 1, };
        var cageUnderMock = new Cage { Id = idUnderTest, Capacity = 1, };

        _cageServiceMock.Setup(x => x.CreatAsync(cageUnderTest).Result).Returns(cageUnderMock);

        ActionResult<ICage> invokeResult = await _cageController.CreateAsync(cageUnderTest);

        Assert.IsInstanceOf<OkObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task CreatAsync_WhenInvalidIdProvided_WillReturn404Result()
    {
        var idUnderTest = 1;
        var cageUnderTest = new Cage { Capacity = 1, };
        var cageUnderMock = new Cage { Id = idUnderTest, Capacity = 1, };

        // force the known exception that will be thrown
        _cageServiceMock
            .Setup(x => x.CreatAsync(cageUnderTest).Result)
            .Throws<InvalidOperationException>();

        ActionResult<ICage> invokeResult = await _cageController.CreateAsync(cageUnderTest);

        Assert.IsInstanceOf<BadRequestObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task UpdateAsync_WillInvokeUnderlyingServiceAsExpected()
    {
        var idUnderTest = 1;
        var cageUnderTest = new Cage { Id = idUnderTest, Capacity = 1, };

        await _cageController.UpdateAsync(cageUnderTest);

        _cageServiceMock.Verify(x => x.UpdateAsync(cageUnderTest), Times.Once());
    }

    [Test]
    public async Task UpdateAsync_WhenValidProvided_WillReturnValidAsExpected()
    {
        var idUnderTest = 1;
        var cageUnderTest = new Cage { Id = idUnderTest, Capacity = 1, };
        var cageUnderMock = new Cage { Id = idUnderTest, Capacity = 1, };

        _cageServiceMock.Setup(x => x.UpdateAsync(cageUnderTest).Result).Returns(cageUnderMock);

        ActionResult<ICage> invokeResult = await _cageController.CreateAsync(cageUnderTest);

        Assert.IsInstanceOf<OkObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task UpdateAsync_WhenInvalidObjectProvided_WillReturn404Result()
    {
        var idUnderTest = 1;
        var cageUnderTest = new Cage { Id = idUnderTest, Capacity = 1, };

        // force the known exception that will be thrown
        _cageServiceMock
            .Setup(x => x.UpdateAsync(cageUnderTest).Result)
            .Throws<InvalidOperationException>();

        ActionResult<ICage> invokeResult = await _cageController.UpdateAsync(cageUnderTest);

        Assert.IsInstanceOf<BadRequestObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task UpdateAsync_WhenInvalidIdProvided_WillReturn404Result()
    {
        var idUnderTest = 1;
        var cageUnderTest = new Cage { Id = idUnderTest, Capacity = 1, };

        // force the known exception that will be thrown
        _cageServiceMock
            .Setup(x => x.UpdateAsync(cageUnderTest).Result)
            .Throws<KeyNotFoundException>();

        var invokeResult = await _cageController.UpdateAsync(cageUnderTest);

        Assert.IsInstanceOf<NotFoundObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task PowerDownAsync_WillInvokeUnderlyingServiceAsExpected()
    {
        var idUnderTest = 1;
        var cageUnderTest = new Cage { Id = idUnderTest, Capacity = 1, };

        await _cageController.PowerDownAsync(idUnderTest);

        _cageServiceMock.Verify(
            x => x.UpdatePowerStatusAsync(idUnderTest, PowerStatusType.Down),
            Times.Once()
        );
    }

    [Test]
    public async Task PowerDownAsync_WhenValidProvided_WillReturnValidAsExpected()
    {
        var idUnderTest = 1;
        var cageUnderMock = new Cage { Id = idUnderTest, Capacity = 1, };

        _cageServiceMock
            .Setup(x => x.UpdatePowerStatusAsync(idUnderTest, PowerStatusType.Down).Result)
            .Returns(cageUnderMock);

        var invokeResult = await _cageController.PowerDownAsync(idUnderTest);

        Assert.IsInstanceOf<OkObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task PowerDownAsync_WhenInValidProvided_ThrowsInvalidOperation_WillReturnAsExpected()
    {
        var idUnderTest = 1;
        var cageUnderMock = new Cage { Id = idUnderTest, Capacity = 1, };

        // force the known exception that will be thrown
        _cageServiceMock
            .Setup(x => x.UpdatePowerStatusAsync(idUnderTest, PowerStatusType.Down).Result)
            .Throws<InvalidOperationException>();

        var invokeResult = await _cageController.PowerDownAsync(idUnderTest);

        Assert.IsInstanceOf<BadRequestObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task PowerDownAsync_WhenInValidProvided_ThrowsKeyNotFound_WillReturnAsExpected()
    {
        var idUnderTest = 1;
        var cageUnderMock = new Cage { Id = idUnderTest, Capacity = 1, };

        // force the known exception that will be thrown
        _cageServiceMock
            .Setup(x => x.UpdatePowerStatusAsync(idUnderTest, PowerStatusType.Down).Result)
            .Throws<KeyNotFoundException>();

        var invokeResult = await _cageController.PowerDownAsync(idUnderTest);

        Assert.IsInstanceOf<NotFoundObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task PowerUpAsync_WillInvokeUnderlyingServiceAsExpected()
    {
        var idUnderTest = 1;
        var cageUnderTest = new Cage { Id = idUnderTest, Capacity = 1, };

        await _cageController.PowerUpAsync(idUnderTest);

        _cageServiceMock.Verify(
            x => x.UpdatePowerStatusAsync(idUnderTest, PowerStatusType.Active),
            Times.Once()
        );
    }

    [Test]
    public async Task PowerUpAsyncc_WhenValidProvided_WillReturnValidAsExpected()
    {
        var idUnderTest = 1;
        var cageUnderMock = new Cage { Id = idUnderTest, Capacity = 1, };

        _cageServiceMock
            .Setup(x => x.UpdatePowerStatusAsync(idUnderTest, PowerStatusType.Active).Result)
            .Returns(cageUnderMock);

        var invokeResult = await _cageController.PowerUpAsync(idUnderTest);

        Assert.IsInstanceOf<OkObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task PowerUpAsync_WhenInValidProvided_ThrowsKeyNotFound_WillReturnAsExpected()
    {
        var idUnderTest = 1;

        // force the known exception that will be thrown
        _cageServiceMock
            .Setup(x => x.UpdatePowerStatusAsync(idUnderTest, PowerStatusType.Active).Result)
            .Throws<KeyNotFoundException>();

        var invokeResult = await _cageController.PowerUpAsync(idUnderTest);

        Assert.IsInstanceOf<NotFoundObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task AssociateDinosaurAsync_WillInvokeUnderlyingServiceAsExpected()
    {
        var cageIdUnderTest = 1;
        var dinoIdUnderTest = 10;

        await _cageController.AssociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest);

        _cageServiceMock.Verify(
            x => x.AssociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest),
            Times.Once()
        );
    }

    [Test]
    public async Task AssociateDinosaurAsync_WhenValidProvided_WillReturnAsExpected()
    {
        var cageIdUnderTest = 1;
        var dinoIdUnderTest = 10;

        _cageServiceMock
            .Setup(x => x.AssociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest).Result)
            .Returns(new Cage());

        var invokeResult = await _cageController.AssociateDinosaurAsync(
            cageIdUnderTest,
            dinoIdUnderTest
        );

        Assert.IsInstanceOf<OkObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task AssociateDinosaurAsync_WhenInvalidIdProvided_WillThrowKeyNotFoundException_WillReturnAsExpected()
    {
        var cageIdUnderTest = 1;
        var dinoIdUnderTest = 10;

        _cageServiceMock
            .Setup(x => x.AssociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest).Result)
            .Throws<KeyNotFoundException>();

        var invokeResult = await _cageController.AssociateDinosaurAsync(
            cageIdUnderTest,
            dinoIdUnderTest
        );

        Assert.IsInstanceOf<NotFoundObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task AssociateDinosaurAsync_WhenInvalidStateProvided_WillThrowInvalidOperationException_WillReturnAsExpected()
    {
        var cageIdUnderTest = 1;
        var dinoIdUnderTest = 10;

        _cageServiceMock
            .Setup(x => x.AssociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest).Result)
            .Throws<InvalidOperationException>();

        var invokeResult = await _cageController.AssociateDinosaurAsync(
            cageIdUnderTest,
            dinoIdUnderTest
        );

        Assert.IsInstanceOf<BadRequestObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task UnassociateDinosaurAsync_WillInvokeUnderlyingServiceAsExpected()
    {
        var cageIdUnderTest = 1;
        var dinoIdUnderTest = 10;

        await _cageController.UnassociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest);

        _cageServiceMock.Verify(
            x => x.UnassociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest),
            Times.Once()
        );
    }

    [Test]
    public async Task UnassociateDinosaurAsync_WhenValidProvided_WillReturnAsExpected()
    {
        var cageIdUnderTest = 1;
        var dinoIdUnderTest = 10;

        _cageServiceMock
            .Setup(x => x.UnassociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest).Result)
            .Returns(new Cage());

        var invokeResult = await _cageController.UnassociateDinosaurAsync(
            cageIdUnderTest,
            dinoIdUnderTest
        );

        Assert.IsInstanceOf<OkObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task UnassociateDinosaurAsync_WhenInvalidIdProvided_WillThrowKeyNotFoundException_WillReturnAsExpected()
    {
        var cageIdUnderTest = 1;
        var dinoIdUnderTest = 10;

        _cageServiceMock
            .Setup(x => x.UnassociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest).Result)
            .Throws<KeyNotFoundException>();

        var invokeResult = await _cageController.UnassociateDinosaurAsync(
            cageIdUnderTest,
            dinoIdUnderTest
        );

        Assert.IsInstanceOf<NotFoundObjectResult>(invokeResult.Result);
    }

    [Test]
    public async Task UnassociateDinosaurAsync_WhenInvalidStateProvided_WillThrowInvalidOperationException_WillReturnAsExpected()
    {
        var cageIdUnderTest = 1;
        var dinoIdUnderTest = 10;

        _cageServiceMock
            .Setup(x => x.UnassociateDinosaurAsync(cageIdUnderTest, dinoIdUnderTest).Result)
            .Throws<InvalidOperationException>();

        var invokeResult = await _cageController.UnassociateDinosaurAsync(
            cageIdUnderTest,
            dinoIdUnderTest
        );

        Assert.IsInstanceOf<BadRequestObjectResult>(invokeResult.Result);
    }
}
