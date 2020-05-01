
using OpenQA.Selenium;

namespace Frisco_automated_shopping.Extensions
{
    public static class ReservationExtensions
    {
        public static void HandleReservation(this IWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@id='header']/div/div[1]/div/div[4]"), 10).Click();
            driver.FindElement(By.XPath("//*[@id='wrapper']/span/div/div/div/div[2]/div[2]/div[2]")).Click();
        }
    }
}
