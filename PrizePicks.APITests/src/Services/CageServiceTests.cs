using Microsoft.Extensions.Logging;

using Moq;

using PrizePicks.API.Data;
using PrizePicks.API.Models;
using PrizePicks.API.Rules;
using PrizePicks.API.Services;

[TestFixture]
public class CageServiceTests
{
    private readonly Mock<ILogger<CageService>> _loggerMock = new Mock<ILogger<CageService>>();
    private Mock<ICageRepository> _cageRepository = new Mock<ICageRepository>();
    private Mock<ICageRules> _cageRules = new Mock<ICageRules>();

    private ICageService _cageService;

    [SetUp]
    public void Setup()
    {
        _cageService = new CageService(
            _loggerMock.Object,
            _cageRules.Object,
            _cageRepository.Object
        );
    }
}
