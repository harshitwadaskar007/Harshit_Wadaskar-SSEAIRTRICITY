using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System.Drawing;
using OpenQA.Selenium.Edge;
using SSEAIRTRICITY.Hooks;

namespace SSEAIRTRICITY.Drivers
{
    public class DriverHelper
    {
        public IWebDriver? driver { get; set; }
        public DateTime date;
        private static Size currentSize;
        private static readonly Size ScreenShotScreenSize = new Size(1280, 1920);
 
        public void LaunchBrowser()
        {
            switch (SpecHooks._config?.BrowserType?.ToUpper())
            {
                case "IE":
                    LaunchIE();
                    break;

                case "CHROME":
                    LaunchChrome();
                    break;

                case "FIREFOX":
                    LaunchFirefox();
                    break;

                case "EDGE":
                    LaunchEdge();
                    break;
            }
            driver?.Manage().Window.Maximize();
            currentSize = driver.Manage().Window.Size;
        }

        private void LaunchChrome()
        {
                var options = new ChromeOptions();
                driver = new ChromeDriver(options);
        }

        private void LaunchIE()
        {
                var options = new InternetExplorerOptions();
                driver = new InternetExplorerDriver(options);
        }

        private void LaunchEdge()
        {
                EdgeOptions options = new EdgeOptions();
                driver = new EdgeDriver(options);
        }

        private void LaunchFirefox()
        {
                FirefoxOptions options = new FirefoxOptions();
                driver = new FirefoxDriver(options);
        }

        readonly bool isFullPageScreenshot = true; 
        public MediaEntityModelProvider CaptureScreenshot(string name)
        {
            if (isFullPageScreenshot)
            {
                driver.Manage().Window.Size = ScreenShotScreenSize;
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
                driver.Manage().Window.Size = currentSize;
                return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name).Build();
            }
            else
            {
                var screenshot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;
                return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name).Build();
            }
        }

        public void CaptureScreenshotAsFile(string fileName, string userInfo)
        {
            Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
            string strFilePath = Directory.GetCurrentDirectory().Replace(@"\bin\Debug\net5.0", "") + @"\Report\";
            image.SaveAsFile(strFilePath + @"\" + fileName + "_" + userInfo + ".Png");
        }
    }
}