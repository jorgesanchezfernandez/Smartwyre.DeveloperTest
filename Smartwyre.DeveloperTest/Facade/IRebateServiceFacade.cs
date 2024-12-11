using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Facade;

public interface IRebateServiceFacade
{
    Rebate GetRebate(string rebateIdentifier);
    Product GetProduct(string productIdentifier);
    void StoreCalculationResult(Rebate rebate, decimal rebateAmount);
    CalculateRebateResult CreateResult(bool success);
}

