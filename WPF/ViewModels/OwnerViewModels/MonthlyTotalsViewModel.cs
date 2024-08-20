using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class MonthlyTotalsViewModel : ViewModelBase
    {
        public int Month { get; set; }
        public int Reservations { get; set; }
        public int CancelledReservations { get; set; }
        public int ModifiedReservations { get; set; }
        public int RenovationRecommendations { get; set; }
        public int DaysSum { get; set; }
        public double OcuppancyRate { get; set; }
        public bool HighestOccupancyRate { get; set; }
        public bool LowestOccupancyRate { get; set; }

        public MonthlyTotalsViewModel() { }
        public MonthlyTotalsViewModel(int month, int reservations, int cancelledReservations, int modifiedReservations,
            int renovationRecommendations, int daysSum)
        {
            Month = month;
            Reservations = reservations;
            CancelledReservations = cancelledReservations;
            ModifiedReservations = modifiedReservations;
            RenovationRecommendations = renovationRecommendations;
            DaysSum = daysSum;

            HighestOccupancyRate = false;
            LowestOccupancyRate = false;
        }
    }
}
