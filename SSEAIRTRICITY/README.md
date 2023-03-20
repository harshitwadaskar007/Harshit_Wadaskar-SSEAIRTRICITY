
# SSE Airtricity

<img src="https://user-images.githubusercontent.com/50833271/226240958-3b55451a-5fd8-49e8-a199-727b29b29f15.png" align="right" height="200" style="float:right; height:200px;">

This project contains the automation framework for testing the home appliances cost estimator on the Citizens Advice website. The purpose of this project is to verify the functionality of the home appliances cost estimator for residents of England, Scotland, and Wales, as well as the error message displayed for residents of Northern Ireland.

## User Story

As a resident from England, Scotland or Wales, I want to estimate how much electrical appliances cost to run, so that I can reduce energy costs and save money.

## Acceptance Criteria

### England
Given I am a resident from England, when I add the list of appliances (at least 8 appliances), their average usage, and the national average rate (34 kWh), then I should get the results table with daily, weekly, monthly, and yearly cost.

### Scotland
Given I am a resident from Scotland, when I add the list of appliances (at least 10 appliances), their average usage, and the average rate (67 kWh), then I should get the results table with daily, weekly, monthly, and yearly cost.

### Wales
Given I am a resident from Wales, when I add the list of appliances (at least 5 appliances), their average usage, and the average rate (67 kWh), then I should get the results table with daily, weekly, monthly, and yearly cost.

### Northern Ireland
Given I am a resident from Northern Ireland, when I select "This advice applies to Northern Ireland", then I should get the results message as "The advice on this website doesn't cover Northern Ireland".

## Exercise Requirements
- The test suite must be in the BDD format adhering to all BDD best practices.
- Must use selenium automation framework.
- Test coverage mentioned in the acceptance criteria must be - clearly shown in the test suite.
- Tests must be able to run on Chrome and Edge.
- Instructions to execute the tests must be provided on submission.
- Must get result in Tabular format.


## Framework and Tools Used

| Usage |
| ------------- | 
|Selenium WebDriver |
|C# Bindings|
|NUnit testing framework for unit testing |
| SpecFlow for BDD scenarios |
|ExtentReports for reporting | 
| Azure DevOps for CI/CD pipeline implementation | 
|POM design pattern|
| Visual Studio IDE|

## Framework Details
The framework is built using Selenium WebDriver, C#, NUnit, and SpecFlow. The solution contains the following projects:

- **'HomeAppliancesCostToUse'**: Contains the core functionality of the automation framework.
- **'HomeAppliancesCostToUseSteps'**: Contains the SpecFlow feature files and step definitions for the test scenarios.
- **'Utilities'**: Contains utility classes and methods used throughout the framework.
- **'Configuration'**: Contains configuration files for the project.
- **'Driver'**: Contains driver files for the project.

## How to Execute the Tests
#### To execute the tests, follow the below steps:

    1. Clone the repository: git clone https://github.com/harshitwadaskar007/Harshit_Wadaskar-SSEAIRTRICITY.git
    2. Open the solution in Visual Studio.
    3. Build the solution.
    4. Open the Test Explorer in Visual Studio.
    5. Select the tests to run.
    6. Right-click on the tests and select "Run Selected Tests".
    7. The tests will run on the selected browser (Chrome or Edge) and the results will be displayed in the Test Explorer.
    8. To access the test report, open the .html file (filename - BrowserName + Date + Time .html) in a web browser.

## Test Coverage

The acceptance criteria mentioned in the user story have been covered in the automation tests. The test coverage for each criterion is as follows:

- England: 8 appliances with the national average rate (34 kWh).
- Scotland: 10 appliances with the average rate (67 kWh).
- Wales: 5 appliances with the average rate (67 kWh).
- Northern Ireland: A message is displayed informing that the advice on the website does not cover Northern Ireland.

## CI/CD Pipeline

The project has been integrated into the Azure DevOps CI/CD pipeline to automate the build and deployment processes. The pipeline includes the following stages:

- **Build**: The build stage compiles the source code, runs the unit tests, and packages the application into a deployable artifact.
- **Test**: The test stage deploys the application to a testing environment and runs the automated tests against it.
## Local Parallel Execution

To speed up the execution of the test suite, I have implemented the parellel execution capability to the framework. I have annotated the test classes with the Parallelizable attribute and specifed the number of threads required. For example, [Parallelizable(ParallelScope.Fixtures)] will run all tests within the class in parallel.

By running tests in parallel, I have significantly reduce the time it takes to execute the test suite.

```bash
  using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.Fixtures)]
[assembly: LevelOfParallelism(2)]
```
## Compatibility Testing

The tests have been designed to run on Chrome and Edge browsers.

The following browsers have been tested for compatibility:

- Chrome Version 111.0.5563.65 
- Edge version 111.0.1661.44

To ensure compatibility, the tests should be run on the latest version of these browsers.

Another way to add compatibility testing to the pipeline is to use a cloud-based testing service, such as **Sauce Labs** or **BrowserStack**. These services allows us to run automated tests on a variety of browsers and operating systems without the need to set up and maintain a large test infrastructure by ourself.
## Test Results

The test results are generated in the form of an HTML report using the ExtentReports library. The report contains a detailed summary of the test results, including the test status, execution time, and screenshots for failed tests.

You can find the Extent Report for the latest test run in the ./SSEAIRTRICITY/Report directory. This report provides detailed information about the tests that were run and their results. Here's a summary of the report:

| Summary||
| ------------- | ------------- |
|Total number of tests run | 4|
|Number of passed tests |4|
| Number of failed tests  | 0  |
|Pass percentage | 100%|
| Pass percentage | 0%|

#### Overview
![Overview](https://github.com/harshitwadaskar007/Screenshots/blob/main/Extent1.png?raw=true)

#### ExtentReport all AC'screenshots
![allACs](https://github.com/harshitwadaskar007/Screenshots/blob/main/Extent2.png?raw=true)

#### England - cost of use in **Tabular format** (As mentioned in the AC)
![allACs](https://github.com/harshitwadaskar007/Screenshots/blob/main/Extent3.png?raw=true)

#### Scotland - cost of use in **Tabular format** (As mentioned in the AC)
![allACs](https://github.com/harshitwadaskar007/Screenshots/blob/main/Extent4.png?raw=true)

#### Wales & Northen Ireland 
![allACs](https://github.com/harshitwadaskar007/Screenshots/blob/main/Extent5.png?raw=true)





The report also includes screenshots of the test runs for each step, which can be useful in identifying and troubleshooting issues.
## Conclusion

This automation project has successfully tested the "Home Appliances Cost to Use" feature of the Citizens Advice website, covering the acceptance criteria mentioned in the user story. The integration with the Azure DevOps CI/CD pipeline and the use of an HTML report for test results can help streamline the build and deployment processes and provide a comprehensive overview of the test results.
## Deployment

To deploy this project run

```bash
  npm run deploy
```

