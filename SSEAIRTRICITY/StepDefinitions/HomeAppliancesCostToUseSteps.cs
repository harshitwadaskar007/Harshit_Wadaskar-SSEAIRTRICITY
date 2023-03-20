using AventStack.ExtentReports.Gherkin.Model;
using NUnit.Framework;
using SSEAIRTRICITY.Drivers;
using SSEAIRTRICITY.Hooks;
using SSEAIRTRICITY.Pages;
using TechTalk.SpecFlow;

namespace SSEAIRTRICITY.StepDefinitions
{
    [Binding]
    public class HomeAppliancesCostToUseSteps
    {
        private readonly ScenarioContext _context;
        private readonly DriverHelper _driverHelper;
        private readonly CompareAppliancesEnergyCostPages _CompareAppliancesEnergyCostPages;

        public HomeAppliancesCostToUseSteps(ScenarioContext context, DriverHelper driverHelper)
        {
            _context = context;
            _driverHelper = driverHelper;
            _CompareAppliancesEnergyCostPages = new CompareAppliancesEnergyCostPages(driverHelper.driver);
        }

        [Given(@"I navigate to electrical appliances cost compare website")]
        public void GivenINavigateToElectricalAppliancesCostCompareWebsite()
        {
            _driverHelper.driver?.Navigate().GoToUrl(SpecHooks._config?.Url);
        }

        [When(@"I am a resident from '([^']*)'")]
        public void WhenIAmAResidentFrom(string country)
        {
            _CompareAppliancesEnergyCostPages.SelectCountry(country);
        }

        [When(@"I add the list of appliances and its average usage and the average rate is '([^']*)' for '([^']*)'")]
        public void WhenIAddTheListOfAppliancesAndItsAverageUsageAndTheAverageRateIsFor(string averageRate, string duration, Table table)
        {
            _context["Table"] = table;
            _CompareAppliancesEnergyCostPages.AddAppliances(averageRate, duration, table);
        }

        [Then(@"I should get the results table with daily, weekly, monthly, and yearly cost")]
        public void ThenIShouldGetTheResultsTableWithDailyWeeklyMonthlyAndYearlyCost()
        {
            Table table = (Table)_context["Table"];
            SpecHooks.costToUseData = _CompareAppliancesEnergyCostPages.GetCostOfUseResultTable(table);
        }

        [Then(@"I should get the results message as '([^']*)'")]
        public void ThenIShouldGetTheResultsMessageAsTheAdviceOnThisWebsiteDoesnTCoverNorthernIreland(string expectedErrorMsg)
        {
            string actualErrorMessage = _CompareAppliancesEnergyCostPages.VerifyAdviceResultMessage();
            Assert.IsTrue(actualErrorMessage.Contains(expectedErrorMsg), "Error message not displayed");
        }
    }
}
