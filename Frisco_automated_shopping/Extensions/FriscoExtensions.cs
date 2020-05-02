
using Frisco_automated_shopping.Models;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Frisco_automated_shopping.Extensions
{
    public static class FriscoExtensions
    {
        public static void Begin(this IWebDriver driver, DeliveryCriteria deliveryCriteria, string email, string password)
        {
            try
            {
                driver.Login(email, password);

                bool isReservationCreated = driver.DoReservation(deliveryCriteria, true);

                while (!isReservationCreated)
                {
                    Thread.Sleep(new TimeSpan(0, 0, 5));
                    isReservationCreated = driver.DoReservation(deliveryCriteria);
                };
                Console.WriteLine("Rezerwacja została utworzona");
            }
            catch (Exception ex)
            {
                ConsoleUtilities.DisplayError(ex.Message);
            }
            finally {
                driver.CloseApplication();
            }
        }
        private static bool DoReservation(this IWebDriver driver, DeliveryCriteria deliveryCriteria, bool firstTime = false)
        {
            return driver.HandleReservation(deliveryCriteria, firstTime);
        }

        private static void CloseApplication(this IWebDriver driver)
        {
            driver.Quit();
            Environment.Exit(0);
        }
    }
}
