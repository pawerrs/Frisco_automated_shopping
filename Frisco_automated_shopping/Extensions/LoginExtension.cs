using OpenQA.Selenium;

namespace Frisco_automated_shopping.Extensions
{
    public static class LoginExtension
    {
        public static IWebDriver Login(this IWebDriver driver, string email, string password)
        {
            driver.FindElement(By.XPath("//*[@id='wrapper']/div[9]/div/div/a[2]/img")).Click();
            driver.FindElement(By.XPath("//*[@id='header']/div/div[1]/div/div[3]/div/a[1]")).Click();

            return driver.EnterEmailAndAddress(email, password).ClickLoginButton();
        }
        public static IWebDriver EnterEmailAndAddress(this IWebDriver driver, string email, string password)
        {
            
            driver.FindElement(By.XPath("/ html / body / div[1] / div / div[2] / div / div[2] / div / form / div / div[1] / div / input")).SendKeys(email);
            driver.FindElement(By.XPath("//*[@id='loginPassword']")).SendKeys(password);
            return driver;
        }

        public static IWebDriver ClickLoginButton(this IWebDriver driver)
        {
            driver.FindElement(By.XPath("//*[@id='container']/div/div[2]/div/div[2]/div/form/section/input")).Click();
            return driver;
        }
    }
}
