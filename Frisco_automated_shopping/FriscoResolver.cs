using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Frisco_automated_shopping.Extensions;
using Frisco_automated_shopping.Models;
using System.Drawing;
using System.IO;

namespace Frisco_automated_shopping
{
    public class FriscoResolver
    {
        private readonly SecureSettings secureSettings;
        private readonly AppSettings appSettings;

        public FriscoResolver(IOptions<SecureSettings> secureSettings, IOptions<AppSettings> appSettings)
        {
            this.secureSettings = secureSettings.Value;
            this.appSettings = appSettings.Value;
        }

        public void InitializeDriver(DeliveryCriteria deliveryCriteria)
        {
            ChromeOptions options = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.None
            };
            IWebDriver driver = new ChromeDriver($"{Directory.GetCurrentDirectory()}\\ChromeDriver", options)
            {
                Url = appSettings.Url
            };

            driver.Manage().Window.Size = new Size(1920, 1080);
            driver.Manage().Window.Maximize();

            driver.SwitchTo().Window(driver.WindowHandles[0]);
            driver.Begin(deliveryCriteria, secureSettings.Email, secureSettings.Password);
        }
    }
}
