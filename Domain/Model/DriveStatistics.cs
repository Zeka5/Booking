using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class DriveStatistics
    {
        public int Month { get; set; }
        public int NumberOfDrives { get; set; }
        public string AverageTime { get; set; }
        public double AveragePrice { get; set; }

        public DriveStatistics(int month, int numberOfDrives, string averageTime, double averagePrice)
        {
            Month = month;
            NumberOfDrives = numberOfDrives;
            AverageTime = averageTime;
            AveragePrice = averagePrice;
        }

        public DriveStatistics() { }
    }
}
