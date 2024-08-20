using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModels.TourGuestViewModels
{
    public class TourRequestStatisticsViewModel : INotifyPropertyChanged
    {
        public TourRequestService tourRequestService;
        public List<int> Years { get; set; }
        public int SelectedYear { get; set; }
        public TourRequestStatisticsViewModel()
        {
            tourRequestService = new TourRequestService(Injector.CreateInstance<ITourRequestRepository>(), new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), new TourService(Injector.CreateInstance<ITourRepository>()), new LocationService(Injector.CreateInstance<ILocationRepository>()), new LanguageService(Injector.CreateInstance<ILanguageRepository>()), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>())));
            
            Years = new List<int>();
            CountOveralAverage();
            LoadAvailableYears();
        }
        public void ComboBoxSelectionChanged()
        {
            double acceptedTourRequest = 0;
            double tourRequests = 0;

            foreach (var tourRequest in tourRequestService.GetAll())
            {
                if(tourRequest.StartDate.Year == SelectedYear)
                {
                    tourRequests++;

                    if(tourRequest.Status == STATUS.Accepted)
                    {
                        acceptedTourRequest++;
                    }
                }
            }

            YearlyAverage = Math.Round(acceptedTourRequest / tourRequests * 100, 2);
        }
        public void LoadAvailableYears()
        {
            Years.Clear();

            foreach (var tourRequest in tourRequestService.GetAll())
            {
                Years.Add(tourRequest.StartDate.Year);
            }
            Years = Years.Distinct().ToList();
        }
        public void CountOveralAverage()
        {
            double acceptedTourRequest = 0;
            double tourRequests = 0;

            foreach(var tourRequest in tourRequestService.GetAll())
            {
                if(tourRequest.Status == Domain.Model.STATUS.Accepted)
                {
                    acceptedTourRequest++;
                }
                tourRequests++;
            }

            OveralAverage = Math.Round(acceptedTourRequest / tourRequests * 100, 2);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public double overalAverage;
        public double OveralAverage
        {
            get { return overalAverage; }
            set
            {
                if (value != overalAverage)
                {
                    overalAverage = value;
                    OnPropertyChanged("OveralAverage");
                }
            }
        }
        public double yearlyAverage;
        public double YearlyAverage
        {
            get { return yearlyAverage; }
            set
            {
                if (value != yearlyAverage)
                {
                    yearlyAverage = value;
                    OnPropertyChanged("YearlyAverage");
                }
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
