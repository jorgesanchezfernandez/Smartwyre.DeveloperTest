using Xunit;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Strategy;

namespace Smartwyre.DeveloperTest.Tests;

public class AmountPerUomStrategyTests
{
    [Fact]
    public void IsApplicable_ShouldReturnTrue_WhenValidConditionsMet()
    {
        // Arrange
        var rebate = new Rebate
        {
            Incentive = IncentiveType.AmountPerUom,
            Amount = 50
        };
        var product = new Product
        {
            SupportedIncentives = SupportedIncentiveType.AmountPerUom
        };
        var request = new CalculateRebateRequest { Volume = 3 };

        var strategy = new AmountPerUomStrategy();

        // Act
        var result = strategy.IsApplicable(rebate, product, request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalculateRebateAmount_ShouldReturnCorrectAmount()
    {
        // Arrange
        var rebate = new Rebate { Amount = 50 };
        var product = new Product();
        var request = new CalculateRebateRequest { Volume = 4 };

        var strategy = new AmountPerUomStrategy();

        // Act
        var amount = strategy.CalculateRebateAmount(rebate, product, request);

        // Assert
        Assert.Equal(200, amount);
    }
}