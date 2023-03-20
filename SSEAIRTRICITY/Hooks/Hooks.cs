using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SSEAIRTRICITY.Configuration;
using SSEAIRTRICITY.Drivers;
using TechTalk.SpecFlow;

namespace SSEAIRTRICITY.Hooks
{

    [Binding]
    public sealed class SpecHooks
    {
        public static AppConfigSettings? _config;
        private DriverHelper _driverHelper;
        private static FeatureContext? _featureContext;
        private static ScenarioContext? _scenarioContext;
        private static ExtentReports? _extentReport;
        private static ExtentTest? _featureName;
        public static ExtentTest? _scenarioName;
        static string configPath = Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net6.0", "") + @"\Appsettings.json";
        static string reportPath = Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net6.0", "") + @"\Report\TestResult.html";
        public static string[,]? costToUseData;


        public SpecHooks(DriverHelper driverHelper, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _featureContext = featureContext;
            _driverHelper = driverHelper;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void InitializeSettings()
        {
            _config = new AppConfigSettings();
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(configPath);
            IConfigurationRoot configuration = builder.Build();
            configuration.Bind(_config);
            var htmlReporter = new ExtentHtmlReporter(reportPath);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            _extentReport = new ExtentReports();
            _extentReport.AddSystemInfo("AUT URL", _config.Url);
            _extentReport.AddSystemInfo("Browser", _config.BrowserType);
            _extentReport.AttachReporter(htmlReporter);
        }

        [AfterTestRun]
        public static void TearDownReport()
        {
            _extentReport?.Flush();
            string path = Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net6.0", "") + @"\Report\";
            File.Move(path + "index.html", path + "Report_" + _config?.BrowserType?.ToString() + "_" + DateTime.Now.ToString("MMddyyyy_HHmmss") + ".html");
        }

        [BeforeFeature]
        public static void BeforeFeatureStart(FeatureContext featureContext)
        {
            _featureContext = featureContext;
            if (null != _featureContext)
            {
                _featureName = _extentReport?.CreateTest<Feature>(_featureContext.FeatureInfo.Title,
                    _featureContext.FeatureInfo.Description);
            }
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext, TestContext testContext)
        {
            _scenarioContext = scenarioContext;
            if (null != _scenarioContext && null != _featureName)
            {
                _scenarioName = _featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title,
                   _scenarioContext.ScenarioInfo.Description);
            }
            _driverHelper.LaunchBrowser();
        }

        [AfterStep]
        public void AfterStep()
        {
            ScenarioBlock scenarioBlock = _scenarioContext.CurrentScenarioBlock;
            var exception = _scenarioContext.TestError;

            if (null == exception)
            {
                var CostToUseData = MarkupHelper.CreateTable(costToUseData);
                var mediaEntity = _driverHelper.CaptureScreenshot(_scenarioContext.ScenarioInfo.Title.Trim());
                switch (scenarioBlock)
                {
                    case ScenarioBlock.Given:
                        _scenarioName?.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case ScenarioBlock.When:
                        _scenarioName?.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case ScenarioBlock.Then:
                        if (_scenarioContext.StepContext.StepInfo.Text == "I should get the results table with daily, weekly, monthly, and yearly cost")
                        {
                            _scenarioName?.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text)
                            .Log(Status.Pass, CostToUseData);
                        }
                        else
                        {
                            _scenarioName?.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                        }
                        break;
                    default:
                        _scenarioName?.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
                        break;
                }
            }
            else if (null != exception)
            {
                var mediaEntity = _driverHelper.CaptureScreenshot(_scenarioContext.ScenarioInfo.Title.Trim());
                switch (scenarioBlock)
                {
                    case ScenarioBlock.Given:
                        _scenarioName?.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
                        break;

                    case ScenarioBlock.When:
                        _scenarioName?.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
                        break;

                    case ScenarioBlock.Then:
                        _scenarioName?.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
                        break;

                    default:
                        _scenarioName?.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message, mediaEntity);
                        break;
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driverHelper.driver.Quit();
        }
    }
}
