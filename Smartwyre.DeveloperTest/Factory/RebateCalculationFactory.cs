using Smartwyre.DeveloperTest.Strategy;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Smartwyre.DeveloperTest.Factory;


// Centralises strategy selection, ensuring the system is easily extensible for new incentive types.
public class RebateCalculationFactory(IEnumerable<IRebateCalculationStrategy> strategies) : IRebateCalculationFactory
{
    public IRebateCalculationStrategy GetStrategy(IncentiveType incentiveType)
    {
        return strategies.FirstOrDefault(strategy =>
            strategy.GetType().Name.Contains(incentiveType.ToString(), StringComparison.OrdinalIgnoreCase));
    }
}