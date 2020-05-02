using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Frisco_automated_shopping.Extensions
{
    public static class WebDriverExtensions
    {

        public static bool RetryingFindClick(this IWebDriver driver, By by)
        {
            bool result = false;
            int attempts = 0;
            while (attempts < 3)
            {
                try
                {
                    driver.FindElement(by).Click();
                    result = true;
                    break;
                }
                catch (Exception ex)
                {
                    if (ex is NoSuchElementException || ex is StaleElementReferenceException)
                    {

                    } else {
                        throw;
                    }
                }
                attempts++;
            }
            return result;
        }

        public static void RetryClickIfPageHasChanged(this IWebDriver driver, By by)
        {
            bool staleElement = true;
            while (staleElement)
            {
                try
                {
                    driver.FindElement(by, 10).Click();
                    staleElement = false;

                }
                catch (StaleElementReferenceException)
                {
                    staleElement = true;
                }
            }
        }

        public static IWebElement FindElement(this IWebDriver driver, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
               
                return wait.Until(drv => drv.FindElement(by));
            }
            return driver.FindElement(by);
        }

        public static void PerformClick(this IWebDriver driver, By by)
        {
            new Actions(driver).MoveToElement(driver.FindElement(by)).Click().Perform();
        }

        public static void PerformClick(this IWebDriver driver, IWebElement element)
        {
            new Actions(driver).MoveToElement(element).Click().Perform();
        }

        public static IWebElement WaitUntilVisible(
            this IWebDriver driver,
            By itemSpecifier,
            int secondsTimeout = 10)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, secondsTimeout));
            var element = wait.Until(driver =>
            {
                try
                {
                    var elementToBeDisplayed = driver.FindElement(itemSpecifier);
                    if (elementToBeDisplayed.Displayed)
                    {
                        return elementToBeDisplayed;
                    }
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }

            });
            return element;
        }
    }
}
