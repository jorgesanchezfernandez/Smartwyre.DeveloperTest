using Microsoft.Extensions.DependencyInjection;
using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Facade;
using Smartwyre.DeveloperTest.Factory;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Strategy;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests.Integration_tests;

 public class CalculateRebateIntegrationTests
{
    [Fact]
    public void FullIntegrationTest_ShouldCalculateRebateSuccessfully()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .AddTransient<IRebateDataStore, RebateDataStore>() 
            .AddTransient<IProductDataStore, ProductDataStore>() 
            .AddTransient<IRebateServiceFacade, RebateServiceFacade>()
            .AddTransient<ICalculateRebateResultFactory, CalculateRebateResultFactory>()
            .AddSingleton<IRebateCalculationFactory, RebateCalculationFactory>()
            .AddTransient<IRebateService, RebateService>()
            .AddSingleton<IRebateCalculationStrategy, FixedCashAmountStrategy>()
            .BuildServiceProvider();

        var rebateService = serviceProvider.GetService<IRebateService>();

        // Datos simulados
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = "REBATE1",
            ProductIdentifier = "PRODUCT1",
            Volume = 5
        };

        // Mock de Facade para datos específicos
        var rebate = new Rebate
        {
            Identifier = "REBATE1",
            Incentive = IncentiveType.FixedCashAmount,
            Amount = 100
        };

        var product = new Product
        {
            Identifier = "PRODUCT1",
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };

        var rebateDataStoreMock = new Mock<IRebateDataStore>();
        rebateDataStoreMock.Setup(ds => ds.GetRebate("REBATE1")).Returns(rebate);

        var productDataStoreMock = new Mock<IProductDataStore>();
        productDataStoreMock.Setup(ds => ds.GetProduct("PRODUCT1")).Returns(product);

        var facade = new RebateServiceFacade(rebateDataStoreMock.Object, productDataStoreMock.Object, new CalculateRebateResultFactory());

        var factory = new RebateCalculationFactory(new[] { new FixedCashAmountStrategy() });

        var service = new RebateService(facade, factory);

        // Act
        var result = service.Calculate(request);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Success);

        rebateDataStoreMock.Verify(ds => ds.GetRebate("REBATE1"), Times.Once);
        productDataStoreMock.Verify(ds => ds.GetProduct("PRODUCT1"), Times.Once);
    }

}
