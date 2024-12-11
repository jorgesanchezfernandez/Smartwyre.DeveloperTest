using Smartwyre.DeveloperTest.Strategy;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Factory;

public interface IRebateCalculationFactory
{
    IRebateCalculationStrategy GetStrategy(IncentiveType incentiveType);
}

