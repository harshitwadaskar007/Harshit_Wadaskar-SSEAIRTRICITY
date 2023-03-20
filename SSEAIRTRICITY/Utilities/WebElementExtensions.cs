using System.Collections.ObjectModel;
using System.Globalization;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SSEAIRTRICITY.Utilities
{
    public static class WebElementExtensions
    {
        /// <summary>
        /// Extension Method for Displayed
        /// </summary>
        /// <param name="element"></param>
        /// <returns> bool </returns>
        public static bool IsDisplayed(this IWebElement element)
        {
            return element.Displayed;
        }

        public static bool IsDisplayed(this By element, IWebDriver driver)
        {
            element.WaitForElementToBeVisible(driver);
            return driver.FindElement(element).Displayed;
        }

        /// <summary>
        /// Extension Method for Enabled
        /// </summary>
        /// <param name="element"></param>
        /// <returns> bool </returns>
        public static bool IsEnabled(this IWebElement element)
        {
            return element.Enabled;
        }

        /// <summary>
        ///  Extension Method for SENDKEYS
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetInput(this IWebElement element, string value)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    element.Clear();
                    element.SendKeys((value));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occured: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        ///  Extension Method for CLICK
        /// </summary>
        /// <param name="element"></param>
        public static void ClickOnElement(this IWebElement element)
        {
            try
            {
                element.Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occured: {ex.Message}");
                throw;
            }
        }

        public static void ClickOnElement(this By element, IWebDriver driver)
        {
            try
            {
                element.WaitForElementToBeVisible(driver);
                driver.FindElement(element).Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occured: {ex.Message}");
                throw;
            }
        }

        public static IWebElement GetLinkWebElement(this string linkName, IWebDriver driver)
        {
            try
            {

                // return driver.FindElement(By.LinkText(linkName));
                return driver.FindElement(By.PartialLinkText(linkName));


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occured: {ex.Message}");
                throw;
            }
        }
        public static IWebElement GetLinkWebElement_(this string linkName, IWebDriver driver)
        {
            try
            {
                if (linkName.Contains("more about this service"))
                {
                    return driver.FindElement(By.XPath("//a[normalize-space()='more about this service']"));
                }
                else
                {
                    return driver.FindElement(By.XPath("//a[contains(text(),'about coronavirus (COVID-19), testing and vaccinat')]"));
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occured: {ex.Message}");
                throw;
            }
        }

        public static string GetText(this IWebElement element)
        {
            try
            {
                return element.Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occured: {ex.Message}");
                throw;
            }
        }

        public static string GetText(this By element, IWebDriver driver)
        {
            try
            {
                element.WaitForElementToBeVisible(driver);
                return driver.FindElement(element).Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occured: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        ///  Extenstion method to move the cursor to an element using Actions method
        /// </summary>
        /// <param name="element"></param>
        /// <param name="browserBase"></param>
        public static void MoveToElementByAction(this IWebElement element, IWebDriver Driver)
        {
            var action = new Actions(Driver);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            action.MoveToElement(element).Click().Build().Perform();
        }

        public static bool WaitForElementToBeVisible(this By byElement, IWebDriver Driver, int waitTimeInSec = 30)
        {
            bool isVisible = false;
            Thread.Sleep(500);
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitTimeInSec));
                wait.Until(e => e.FindElement(byElement));
                if (Driver.FindElement(byElement).Displayed)
                {
                    isVisible = true;
                }
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"Exception Occured on Page Load : {ex.Message}");
                throw;
            }
            return isVisible;
        }

        public static bool WaitForElementToBeEnabled(By byElement, IWebDriver Driver, int waitTimeInSec = 30)
        {
            bool isEnable = false;
            try
            {
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(waitTimeInSec));
                wait.Until(e => e.FindElement(byElement));
                if (Driver.FindElement(byElement).Enabled)
                {
                    isEnable = true;
                }
            }
            catch (NoSuchElementException ex)
            {
                Console.WriteLine($"Exception Occured on Page Load : {ex.Message}");
                throw;
            }
            return isEnable;
        }

        public static string GetPopUpWindowTitle(this string PageName, IWebDriver driver)
        {
            string BaseWindow = driver.CurrentWindowHandle;

            ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
            string popupHandle = "";

            foreach (string handle in windowHandles)
            {
                if (handle != BaseWindow)
                {
                    popupHandle = handle; break;
                }
            }

            driver.SwitchTo().Window(popupHandle);
            string NewWindow = driver.CurrentWindowHandle;
            Console.WriteLine(driver.Title);
            return PageName = driver.Title;
        }

        public static bool TimeDifferenceInHours(this string startDate, String expiryDate, int hours)
        {
            CultureInfo provider = new CultureInfo("en-GB");
            DateTime start_Date = DateTime.Parse(startDate, provider, DateTimeStyles.AdjustToUniversal);
            DateTime expiry_Date = DateTime.Parse(expiryDate.Substring(0, 12), provider, DateTimeStyles.AdjustToUniversal);
            TimeSpan ts = expiry_Date - start_Date;
            double hrs = ts.TotalHours;
            if (ts.TotalHours <= hours)
                return true;
            else
                return false;
        }

        public static void SelectByValue(this IWebElement element, String textValue)
        {
            SelectElement dropDown = new SelectElement(element);
            dropDown.SelectByValue(textValue);
        }

        public static void SelectByIndex(this IWebElement element, int index)
        {
            SelectElement dropDown = new SelectElement(element);
            dropDown.SelectByIndex(index);
        }

        public static void SelectByText(this IWebElement element, String textValue)
        {
            SelectElement dropDown = new SelectElement(element);
            dropDown.SelectByText(textValue);
        }


        public static IWebElement GetElementByXpath(this string element, IWebDriver driver)
        {
            try
            {
                return driver.FindElement(By.XPath(element));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occured: {ex.Message}");
                throw;
            }
        }

    }
}