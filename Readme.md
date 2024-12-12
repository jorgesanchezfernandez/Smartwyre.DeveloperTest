# Smartwyre Developer Test

## Description
This project implements a solution to calculate rebates based on different types of incentives. The solution is designed to be extensible, making it easy to add new incentive types in the future.

## What Has Been Implemented
1. **Extensible and Modular Design**:
   - The **Strategy** pattern was used to encapsulate the calculation logic for each incentive type (`FixedCashAmount`, `FixedRateRebate`, `AmountPerUom`).
   - The **Factory** pattern simplifies the selection of the appropriate strategy at runtime.
   - The **Facade** pattern was implemented to simplify interactions with multiple data sources (`RebateDataStore`, `ProductDataStore`, `CalculateRebateResultFactory`) and to centralize orchestration logic for the `RebateService`, improving readability and maintainability.
   - **Dependency Injection** was implemented to decouple classes and improve testability. This approach ensures that components like `RebateService` and `RebateServiceFacade` can be easily replaced or mocked, facilitating unit testing and enhancing maintainability.

2. **Unit Tests**:
   - Unit tests were developed for critical classes, including:
     - `RebateService`.
     - Incentive calculation strategies.
     - `RebateServiceFacade`.
   - These tests ensure the system behaves correctly in the main scenarios.

3. **Extensibility**:
   - New incentive types can be added by creating a new strategy that implements `IRebateCalculationStrategy`.
   - The registration logic is clear and easy to maintain.

4. **Workflow Documentation**:
   - The code includes comments to facilitate understanding and maintenance.

## How to Run the Project
1. Build the project:
   ```bash
   dotnet build
   ```
2. Run the tests:
   ```bash
   dotnet test
   ```
3. Execute the program:
   ```bash
   dotnet run
   ```

## Note About Data
This project does not include valid data for `RebateDataStore` or `ProductDataStore`, as per the exercise's specifications. As a result:
- The program will not produce meaningful results.
- The focus of the exercise is on code structure, extensibility, and meeting the functional requirements.

Despite this limitation:
- The code compiles successfully.
- All unit tests pass.
- The solution adheres to the exercise's requirements and is ready for future extensions.

## Future Possible Improvements
- Add integration tests to validate the complete interaction between classes.
- Incorporate more robust input validation in the executable program.
- Improve test coverage for additional scenarios.