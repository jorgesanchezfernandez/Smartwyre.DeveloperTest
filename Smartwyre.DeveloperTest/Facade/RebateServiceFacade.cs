using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factory;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Facade;

public class RebateServiceFacade(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, ICalculateRebateResultFactory resultFactory) : IRebateServiceFacade
{
    public Rebate GetRebate(string rebateIdentifier)
    {
        return rebateDataStore.GetRebate(rebateIdentifier);
    }

    public Product GetProduct(string productIdentifier)
    {
        return productDataStore.GetProduct(productIdentifier);
    }

    public void StoreCalculationResult(Rebate rebate, decimal rebateAmount)
    {
        rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
    }

    public CalculateRebateResult CreateResult(bool success)
    {
        return resultFactory.Create(success);
    }
}
