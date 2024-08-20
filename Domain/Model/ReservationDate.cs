using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class ReservationDate
    {
        public DateTime FirstDay { get; set; }

        public DateTime LastDay { get; set; }
        public string Date { get; set; }

        public ReservationDate() { }
        public ReservationDate(DateTime firstDay, DateTime lastDay)
        {
            FirstDay = firstDay;
            LastDay = lastDay;
            Date = firstDay.ToString("dd/MM/yyyy") + "-" + lastDay.ToString("dd/MM/yyyy");
        }
    }
}
