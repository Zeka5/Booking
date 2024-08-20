using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class AnnualDataViewModel : ViewModelBase
    {
        private List<MonthlyTotalsViewModel> annualDataList;

        public ObservableCollection<MonthlyTotalsViewModel> AnnualData { get; private set; }

        public AnnualDataViewModel()
        {
            annualDataList = new List<MonthlyTotalsViewModel>();
        }

        private MonthlyTotalsViewModel? GetMonthTotals(int month)
        {
            return annualDataList.FirstOrDefault(monthTotal => monthTotal.Month == month);
        }

        public void AddData(AccommodationReservation reservation, int requests, int suggestions)
        {
            var month = GetMonthTotals(reservation.FirstDay.Month);
            if (month is null)
                annualDataList.Add(new MonthlyTotalsViewModel(reservation.FirstDay.Month, 1,
                    (reservation.Status == ReservationStatus.Canceled) ? 1 : 0, requests, suggestions,
                    (reservation.Status != ReservationStatus.Canceled)
                        ? (reservation.LastDay - reservation.FirstDay).Days : 0));
            else
            {
                month.Reservations += 1;
                if (reservation.Status == ReservationStatus.Canceled) month.CancelledReservations += 1;
                month.ModifiedReservations += requests;
                month.RenovationRecommendations += suggestions;
                month.DaysSum += (reservation.Status != ReservationStatus.Canceled)
                    ? (reservation.LastDay - reservation.FirstDay).Days : 0;
            }
        }

        public void Sort()
        {
            AnnualData = new ObservableCollection<MonthlyTotalsViewModel>(annualDataList.OrderBy(totals => totals.Month));
        }

        public void CalculateOccupancyRate()
        {
            foreach (var totals in AnnualData) totals.OcuppancyRate = (double)(totals.DaysSum / 31.0);
        }

        public void CalculateHighestAndLowestRate()
        {
            var highest = AnnualData.OrderByDescending(totals => totals.OcuppancyRate).FirstOrDefault();
            if (highest != null) highest.HighestOccupancyRate = true;
            if (AnnualData.Count <= 1) return;
            var lowest = AnnualData.OrderBy(totals => totals.OcuppancyRate).FirstOrDefault();
            if (lowest != null) lowest.LowestOccupancyRate = true;
        }
    }
}
