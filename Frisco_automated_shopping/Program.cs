using Frisco_automated_shopping.Configuration;
using Frisco_automated_shopping.Extensions;
using Frisco_automated_shopping.Models;
using System;
using System.Globalization;

namespace Frisco_automated_shopping
{

    class Program
    {
        static void Main(string[] args)
        {
            var deliveryCriteria = GetDeliveryCriteria();
            BaseConfiguration.ConfigureApplication(deliveryCriteria);
        }

        private static DeliveryCriteria GetDeliveryCriteria()
        {
            ConsoleUtilities.SetConsoleColor(ConsoleColor.Green);
            Console.WriteLine("Wpisz ostatni dzień w jaki chcesz uzyskać dostawę w formacie dzień/miesiąc");
            var lastDate = DateTime.Now.AddDays(60);
            //var lastDate = DateTime.ParseExact(Console.ReadLine() + "/2020", "d/M/yyyy", CultureInfo.InvariantCulture);

            Console.WriteLine("Teraz wybierz godziny dostawy. Możesz wybrać godziny 6-22");
            Console.WriteLine("Wpisz godzinę od której mógłbyś/mogłabyś przyjąć zamówienie.");
            //var fromHour = int.Parse(Console.ReadLine());
            var fromHour = 6;

            Console.WriteLine("Wpisz godzinę do której mógłbyś/mogłabyś przyjąć zamówienie");
            //var toHour = int.Parse(Console.ReadLine());
            var toHour = 22;

            ConsoleUtilities.SetConsoleColor(ConsoleColor.White);

            return new DeliveryCriteria(lastDate, fromHour, toHour);
        }
    }
}
