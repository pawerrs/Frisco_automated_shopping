using Frisco_automated_shopping.Models;
using HtmlAgilityPack;
using OpenQA.Selenium;
using System;
using System.Linq;
using System.Threading;


namespace Frisco_automated_shopping.Extensions
{
    public static class ReservationExtensions
    {
        public static bool HandleReservation(this IWebDriver driver, DeliveryCriteria deliveryCriteria, bool firstTime)
        {
            if (firstTime)
            {
                Thread.Sleep(6000);
            }
            else
            {
                driver.WaitUntilVisible(By.XPath("//*[@id='header']/div/div[1]/div/div[4]")).Click();
            }

            driver.WaitUntilVisible(By.XPath("//*[@id='header']/div/div[1]/div/div[4]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//*[@id='wrapper']/span/div/div/div/div[2]/div[2]/div[2]"), 10).Click();

            if (driver.CheckFirstAvailableDeliveryDate(deliveryCriteria.LastPreferredDate))
            {
                driver.GetCalendar(deliveryCriteria);
                driver.MakeAReservation();
                return true;
            }
            else
            {
                driver.FindElement(By.XPath("//*[@id='wrapper']/span/div/div/div/div[2]/div[2]/div[2]/div/div/div[3]/div[2]/div/div[1]"));
                return false;
            }
        }

        private static bool CheckFirstAvailableDeliveryDate(this IWebDriver driver, DateTime lastDateOfDelivery)
        {
            driver.FindElement(By.XPath("//*[@id='wrapper']/span/div/div/div/div[2]/div[2]/div[2]/div/div/div[1]/div"))
                .GetAttribute("innerText")
                .Split(new string[] { "\r\n" }, StringSplitOptions.None)
                .FirstOrDefault()
                .ConvertStringToMonthNumber(out int month);
                
            if(month <= lastDateOfDelivery.Month)
            {
                var firstAvailableDeliveryDay = driver.FindElement(By.XPath("//*[@id='wrapper']/span/div/div/div/div[2]/div[2]/div[2]/div/div/div[2]/div/div[2]/div"))
                    .FindElement(By.XPath("//div[@class='day active']"))
                    .GetAttribute("innerText")
                    .Split(new string[] { "\r\n" }, StringSplitOptions.None)
                    .FirstOrDefault();

                var result = int.TryParse(firstAvailableDeliveryDay, out int firstDayAvailable);

                return month == lastDateOfDelivery.Month 
                    ?  result ? firstDayAvailable < lastDateOfDelivery.Day : false : true;
            }
            else
            {
                return false;
            }
        }

        private static void MakeAReservation(this IWebDriver driver)
        {
            driver.PerformClick(By.XPath("//*[@id='wrapper']/span/div/div/div/div[2]/div[2]/div[2]/div/div/div[3]/div[2]/div/div[2]"));
        }

        private static void GetCalendar(this IWebDriver driver, DeliveryCriteria deliveryCriteria)
        {
            var html = driver.FindElement(By.XPath("/html/body/div[1]/div/div/span/div/div/div/div[2]/div[2]/div[2]/div/div/div[3]/div[1]/div"), 10).GetAttribute("outerHTML");

            var document = new HtmlDocument();
            document.LoadHtml(html);
            var availableReservations = document.DocumentNode.SelectNodes("//div[@class='calendar_column-day available']");

            foreach (var (availableReservation, index) in availableReservations.WithIndex())
            {
                document.LoadHtml(availableReservation.InnerHtml);
                var availableHours = document.DocumentNode.SelectSingleNode("//span[@class='hours']").InnerText;
                var hoursAndMinutes = availableHours.GetHours();

                if(hoursAndMinutes.HourFrom <= deliveryCriteria.FromHour && deliveryCriteria.ToHour >= hoursAndMinutes.HourTo)
                {
                    var pickedElement = driver.FindElements(By.XPath("//div[@class='calendar_column-day available']"))[index];
                    driver.PerformClick(pickedElement);
                    return;
                }
            }
        }

    }
}
