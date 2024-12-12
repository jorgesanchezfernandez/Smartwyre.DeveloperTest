using Xunit;
using Moq;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Facade;
using Smartwyre.DeveloperTest.Factory;
using Smartwyre.DeveloperTest.Strategy;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceTests
{
    [Fact]
    public void Calculate_ShouldReturnSuccess_WhenConditionsAreMet()
    {
        // Arrange
        var facadeMock = new Mock<IRebateServiceFacade>();
        var factoryMock = new Mock<IRebateCalculationFactory>();
        var strategyMock = new Mock<IRebateCalculationStrategy>();

        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount };
        var product = new Product();
        var request = new CalculateRebateRequest();
        var result = new CalculateRebateResult { Success = true };

        facadeMock.Setup(f => f.GetRebate(It.IsAny<string>())).Returns(rebate);
        facadeMock.Setup(f => f.GetProduct(It.IsAny<string>())).Returns(product);
        facadeMock.Setup(f => f.CreateResult(It.IsAny<bool>())).Returns(result);

        strategyMock.Setup(s => s.IsApplicable(rebate, product, request)).Returns(true);
        strategyMock.Setup(s => s.CalculateRebateAmount(rebate, product, request)).Returns(100);

        factoryMock.Setup(f => f.GetStrategy(IncentiveType.FixedCashAmount)).Returns(strategyMock.Object);

        var service = new RebateService(facadeMock.Object, factoryMock.Object);

        // Act
        var calculationResult = service.Calculate(request);

        // Assert
        Assert.NotNull(calculationResult);
        Assert.True(calculationResult.Success);
        facadeMock.Verify(f => f.StoreCalculationResult(rebate, 100), Times.Once);
    }

    [Fact]
    public void Calculate_ShouldReturnFailure_WhenStrategyNotFound()
    {
        // Arrange
        var facadeMock = new Mock<IRebateServiceFacade>();
        var factoryMock = new Mock<IRebateCalculationFactory>();

        var rebate = new Rebate { Incentive = IncentiveType.FixedCashAmount };
        var product = new Product();
        var request = new CalculateRebateRequest();
        var result = new CalculateRebateResult { Success = false };

        facadeMock.Setup(f => f.GetRebate(It.IsAny<string>())).Returns(rebate);
        facadeMock.Setup(f => f.GetProduct(It.IsAny<string>())).Returns(product);
        facadeMock.Setup(f => f.CreateResult(It.IsAny<bool>())).Returns(result);

        factoryMock.Setup(f => f.GetStrategy(IncentiveType.FixedCashAmount)).Returns((IRebateCalculationStrategy)null);

        var service = new RebateService(facadeMock.Object, factoryMock.Object);

        // Act
        var calculationResult = service.Calculate(request);

        // Assert
        Assert.NotNull(calculationResult);
        Assert.False(calculationResult.Success);
        facadeMock.Verify(f => f.StoreCalculationResult(It.IsAny<Rebate>(), It.IsAny<decimal>()), Times.Never);
    }
}
