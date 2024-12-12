using Xunit;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Strategy;

namespace Smartwyre.DeveloperTest.Tests;

public class FixedRateRebateStrategyTests
{
    [Fact]
    public void IsApplicable_ShouldReturnTrue_WhenValidConditionsMet()
    {
        // Arrange
        var rebate = new Rebate
        {
            Incentive = IncentiveType.FixedRateRebate,
            Percentage = 0.1m
        };
        var product = new Product
        {
            SupportedIncentives = SupportedIncentiveType.FixedRateRebate,
            Price = 200
        };
        var request = new CalculateRebateRequest { Volume = 10 };

        var strategy = new FixedRateRebateStrategy();

        // Act
        var result = strategy.IsApplicable(rebate, product, request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalculateRebateAmount_ShouldReturnCorrectAmount()
    {
        // Arrange
        var rebate = new Rebate { Percentage = 0.1m };
        var product = new Product { Price = 200 };
        var request = new CalculateRebateRequest { Volume = 5 };

        var strategy = new FixedRateRebateStrategy();

        // Act
        var amount = strategy.CalculateRebateAmount(rebate, product, request);

        // Assert
        Assert.Equal(100, amount);
    }
}
