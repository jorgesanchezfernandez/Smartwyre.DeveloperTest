using Xunit;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Strategy;

namespace Smartwyre.DeveloperTest.Tests;

public class FixedCashAmountStrategyTests
{
    [Fact]
    public void IsApplicable_ShouldReturnTrue_WhenValidConditionsMet()
    {
        // Arrange
        var rebate = new Rebate
        {
            Amount = 100,
            Incentive = IncentiveType.FixedCashAmount
        };
        var product = new Product
        {
            SupportedIncentives = SupportedIncentiveType.FixedCashAmount
        };
        var request = new CalculateRebateRequest();

        var strategy = new FixedCashAmountStrategy();

        // Act
        var result = strategy.IsApplicable(rebate, product, request);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CalculateRebateAmount_ShouldReturnCorrectAmount()
    {
        // Arrange
        var rebate = new Rebate
        {
            Amount = 100
        };
        var product = new Product();
        var request = new CalculateRebateRequest();

        var strategy = new FixedCashAmountStrategy();

        // Act
        var amount = strategy.CalculateRebateAmount(rebate, product, request);

        // Assert
        Assert.Equal(100, amount);
    }
}

