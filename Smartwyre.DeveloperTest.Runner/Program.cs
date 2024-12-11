using Microsoft.Extensions.DependencyInjection;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Facade;
using Smartwyre.DeveloperTest.Factory;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Strategy;
using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        //Registering the dependency injection
        var serviceProvider = new ServiceCollection()
            .AddTransient<IRebateDataStore, RebateDataStore>()
            .AddTransient<IProductDataStore, ProductDataStore>()
            .AddTransient<IRebateServiceFacade, RebateServiceFacade>()
            .AddTransient<ICalculateRebateResultFactory, CalculateRebateResultFactory>()
            .AddSingleton<IRebateCalculationFactory, RebateCalculationFactory>()
            .AddTransient<IRebateService, RebateService>()
            .AddSingleton<IRebateCalculationStrategy, FixedCashAmountStrategy>()
            .AddSingleton<IRebateCalculationStrategy, FixedRateRebateStrategy>()
            .AddSingleton<IRebateCalculationStrategy, AmountPerUomStrategy>()
            .BuildServiceProvider();

        // Get the rebate service
        var rebateService = serviceProvider.GetService<IRebateService>();

        // Request data from the user
        Console.WriteLine("Enter the rebate identifier:");
        string rebateIdentifier = Console.ReadLine();

        Console.WriteLine("Enter the product identifier:");
        string productIdentifier = Console.ReadLine();

        Console.WriteLine("Enter the volume:");
        if (!decimal.TryParse(Console.ReadLine(), out decimal volume))
        {
            Console.WriteLine("The volume must be a valid decimal number.");
            return;
        }

        // Create the rebate request
        var request = new CalculateRebateRequest
        {
            RebateIdentifier = rebateIdentifier,
            ProductIdentifier = productIdentifier,
            Volume = volume
        };

        // Calculate the rebate result
        var result = rebateService.Calculate(request);

        // Show the results
        if (result.Success)
        {
            Console.WriteLine("The calculation was successful!");
        }
        else
        {
            Console.WriteLine("The calculation failed. Please check the data entered.");
        }
    }
}