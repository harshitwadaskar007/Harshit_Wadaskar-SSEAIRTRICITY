using OpenQA.Selenium;
using System;
using System.Threading;

namespace SSEAIRTRICITY.Utilities
{
    public class DriverExtensions
    {
        private IWebDriver _driver;
        public DriverExtensions(IWebDriver driver) => _driver = driver;
        private int IMPLICIT_TIMEOUT_MilliSEC = 20000;
        public static string gbValue = null;

        public string GetTitle()
        {
            try
            {
                return _driver.Title;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occured: {ex.Message}");
                return null;
            }
        }

        public void SwitchTab(int index)
        {
            var newTab = _driver.WindowHandles[index];
            _driver.SwitchTo().Window(newTab);
        }

        public void WaitSync()
        {
            Thread.Sleep(IMPLICIT_TIMEOUT_MilliSEC);
        }

    }
}
