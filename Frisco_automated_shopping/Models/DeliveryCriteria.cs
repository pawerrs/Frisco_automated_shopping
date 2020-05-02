
using System;

namespace Frisco_automated_shopping.Models
{
    public class DeliveryCriteria
    {
        public DateTime LastPreferredDate { get; set; }
        public int FromHour { get; set; }
        public int ToHour { get; set; }

        public DeliveryCriteria(DateTime lastPreferredDate, int fromHour, int toHour)
        {
            LastPreferredDate = lastPreferredDate;
            FromHour = fromHour;
            ToHour = toHour;
        }
    }
}
