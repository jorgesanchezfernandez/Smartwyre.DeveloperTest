using Xunit;
using Smartwyre.DeveloperTest.Types;
using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Facade;
using Smartwyre.DeveloperTest.Factory;

namespace Smartwyre.DeveloperTest.Tests;

public class RebateServiceFacadeTests
{
    [Fact]
    public void GetRebate_ShouldReturnRebate()
    {
        // Arrange
        var dataStoreMock = new Mock<IRebateDataStore>();
        var productStoreMock = new Mock<IProductDataStore>();
        var factoryMock = new Mock<ICalculateRebateResultFactory>();

        var rebate = new Rebate();
        dataStoreMock.Setup(ds => ds.GetRebate(It.IsAny<string>())).Returns(rebate);

        var facade = new RebateServiceFacade(dataStoreMock.Object, productStoreMock.Object, factoryMock.Object);

        // Act
        var result = facade.GetRebate("REBATE123");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(rebate, result);
    }

    [Fact]
    public void CreateResult_ShouldReturnSuccessResult()
    {
        // Arrange
        var dataStoreMock = new Mock<IRebateDataStore>();
        var productStoreMock = new Mock<IProductDataStore>();
        var factoryMock = new Mock<ICalculateRebateResultFactory>();

        factoryMock.Setup(f => f.Create(true)).Returns(new CalculateRebateResult { Success = true });

        var facade = new RebateServiceFacade(dataStoreMock.Object, productStoreMock.Object, factoryMock.Object);

        // Act
        var result = facade.CreateResult(true);

        // Assert
        Assert.True(result.Success);
    }
}