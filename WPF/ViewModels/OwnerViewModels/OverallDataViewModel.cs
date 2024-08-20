using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class OverallDataViewModel : ViewModelBase
    {
        private List<AnnualTotalsViewModel> overallDataList;

        public ObservableCollection<AnnualTotalsViewModel> OverallData { get; private set; }

        public OverallDataViewModel()
        {
            overallDataList = new List<AnnualTotalsViewModel>();
        }

        private AnnualTotalsViewModel? GetYearTotals(int year)
        {
            return overallDataList.FirstOrDefault(yearStat => yearStat.Year == year);
        }

        public void AddData(AccommodationReservation reservation, int requests, int suggestions)
        {
            var year = GetYearTotals(reservation.FirstDay.Year);
            if (year is null)
                overallDataList.Add(new AnnualTotalsViewModel(reservation.FirstDay.Year, 1,
                    (reservation.Status == ReservationStatus.Canceled) ? 1 : 0, requests, suggestions,
                    (reservation.Status != ReservationStatus.Canceled)
                        ? (reservation.LastDay - reservation.FirstDay).Days : 0));
            else
            {
                year.Reservations += 1;
                if (reservation.Status == ReservationStatus.Canceled) year.CancelledReservations += 1;
                year.ModifiedReservations += requests;
                year.RenovationRecommendations += suggestions;
                year.DaysSum += (reservation.Status != ReservationStatus.Canceled)
                    ? (reservation.LastDay - reservation.FirstDay).Days : 0;
            }
        }

        public void Sort()
        {
            OverallData = new ObservableCollection<AnnualTotalsViewModel>(overallDataList.OrderByDescending(totals => totals.Year));
        }

        public void CalculateOccupancyRate()
        {
            foreach (var totals in OverallData) totals.OcuppancyRate = (double)(totals.DaysSum / 365.0);
        }

        public void CalculateHighestAndLowestRate()
        {
            var highest = OverallData.OrderByDescending(totals => totals.OcuppancyRate).FirstOrDefault();
            if (highest != null) highest.HighestOccupancyRate = true;
            if (OverallData.Count <= 1) return;
            var lowest = OverallData.OrderBy(totals => totals.OcuppancyRate).FirstOrDefault();
            if (lowest != null) lowest.LowestOccupancyRate = true;
        }
    }
}
