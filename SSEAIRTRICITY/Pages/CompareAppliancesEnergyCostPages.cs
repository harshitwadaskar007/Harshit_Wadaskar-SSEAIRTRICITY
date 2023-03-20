using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using SSEAIRTRICITY.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Diagnostics.Metrics;
using System.Reflection.Emit;

namespace SSEAIRTRICITY.Pages
{
    public class CompareAppliancesEnergyCostPages
    {
        private readonly IWebDriver _driver;
        private IWebElement CountryChangeLnk => _driver.FindElement(By.XPath("//span[contains(text(),'This advice applies to')]/child::a"));
        private IWebElement ApplianceLst => _driver.FindElement(By.Id("appliance"));
        private By Hours => By.Id("hours");
        private By Mins => By.Id("mins");
        private IWebElement HoursTxt => _driver.FindElement(By.Id("hours"));
        private IWebElement MinsTxt => _driver.FindElement(By.Id("mins"));
        private IWebElement FrequencyLst => _driver.FindElement(By.Id("frequency"));
        private IWebElement AveragekwhCostTxt => _driver.FindElement(By.Id("kwhcost"));
        private IWebElement AddApplianceBth => _driver.FindElement(By.Id("submit"));
        private string SelectCountryLnk => "//a[@class='btn btn-small'][contains(text(),'[COUNTRY]')]";
        private string Appliance => "//div[@id='appliance_running']//td[contains(text(),'[APPLIANCE]')]";
        private string CostOfAppaliances => "//div[@id='appliance_running']//td[contains(text(),'[APPLIANCE]')]/parent::tr/following-sibling::tr[[INDEX]]/td[[OCCURANCETYPE]]";
        private string errorMsgRelativeLocatorText => "//p[@class='location-switcher__text']";
        public CompareAppliancesEnergyCostPages(IWebDriver driver) => _driver = driver;

        public void SelectCountry(string country)
        {
            CountryChangeLnk.ClickOnElement();
            SelectCountryLnk.Replace("[COUNTRY]", country).GetElementByXpath(_driver).ClickOnElement();
        }

        public void AddAppliances(string averageRate, string duration, Table table)
        {
            FrequencyLst.SelectByValue(duration);
            AveragekwhCostTxt.SendKeys(averageRate);
            var appliancesUsageData = table.CreateSet<AppliancesDetails>().ToList();
            for (int count = 0; count < appliancesUsageData.Count(); count++)
            {
                string appliance = appliancesUsageData[count].Appliances;
                ApplianceLst.SelectByText(appliancesUsageData[count].Appliances);
                Hours.WaitForElementToBeVisible(_driver);
                HoursTxt.SetInput(appliancesUsageData[count].Hours);
                Mins.WaitForElementToBeVisible(_driver);
                MinsTxt.SetInput(appliancesUsageData[count].Minutes);
                AddApplianceBth.Click();
            }
        }

        public string[,] GetCostOfUseResultTable(Table table)
        {
            var appliancesUsageData = table.CreateSet<AppliancesDetails>().ToList();
            string[,] costDataFetched = new string[appliancesUsageData.Count() + 1, 5];
            costDataFetched[0, 0] = "Applainces";
            costDataFetched[0, 1] = "Daily";
            costDataFetched[0, 2] = "Weekly";
            costDataFetched[0, 3] = "Monthly";
            costDataFetched[0, 4] = "Yearly";
            for (int count = 0; count < appliancesUsageData.Count(); count++)
            {
                costDataFetched[count + 1, 0] = Appliance.Replace("[APPLIANCE]", appliancesUsageData[count].Appliances).GetElementByXpath(_driver).GetText();
                costDataFetched[count + 1, 1] = CostOfAppaliances.Replace("[APPLIANCE]", appliancesUsageData[count].Appliances).Replace("[INDEX]", "1").Replace("[OCCURANCETYPE]", "2").GetElementByXpath(_driver).GetText();
                costDataFetched[count + 1, 2] = CostOfAppaliances.Replace("[APPLIANCE]", appliancesUsageData[count].Appliances).Replace("[INDEX]", "2").Replace("[OCCURANCETYPE]", "2").GetElementByXpath(_driver).GetText();
                costDataFetched[count + 1, 3] = CostOfAppaliances.Replace("[APPLIANCE]", appliancesUsageData[count].Appliances).Replace("[INDEX]", "3").Replace("[OCCURANCETYPE]", "2").GetElementByXpath(_driver).GetText();
                costDataFetched[count + 1, 4] = CostOfAppaliances.Replace("[APPLIANCE]", appliancesUsageData[count].Appliances).Replace("[INDEX]", "4").Replace("[OCCURANCETYPE]", "2").GetElementByXpath(_driver).GetText();
            }
            return costDataFetched;
        }

        public string VerifyAdviceResultMessage()
        {
            IWebElement element = errorMsgRelativeLocatorText.GetElementByXpath(_driver);
            return element.FindElement(RelativeBy.WithLocator(By.TagName("p")).Below(element)).GetText();
        }

    }
}
