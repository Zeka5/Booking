using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using BookingApp.Services;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.WPF.ViewModels.TourGuestViewModels
{
    public class FinishedToursViewModel
    {
        public TourRealizationService tourRealizationService;
        public TourReservationService tourReservationService;
        public TourService tourService;
        public LocationService locationService;
        public LanguageService languageService;
        public static ObservableCollection<TourRealizationDto> TourRealizationList { get; set; }
        public TourRealizationDto? SelectedTourRealization { get; set; }
        public FinishedToursViewModel()
        {
            tourRealizationService = new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), new TourService(Injector.CreateInstance<ITourRepository>()), new LocationService(Injector.CreateInstance<ILocationRepository>()), new LanguageService(Injector.CreateInstance<ILanguageRepository>()), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>()));
            tourReservationService = new TourReservationService(Injector.CreateInstance<ITourReservationRepository>());
            tourService = new TourService(Injector.CreateInstance<ITourRepository>());
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());

            TourRealizationList = new ObservableCollection<TourRealizationDto>();

            LoadFinishedTourRealizations();
        }
        public void LoadFinishedTourRealizations()
        {
            TourRealizationList = tourRealizationService.LoadFinishedTourRealizations();
        }
        public TourRealizationDto RateTour()
        {
            if (SelectedTourRealization == null)
            {
                MessageBox.Show("Select a tour to rate!");
                return null;
            }
            return SelectedTourRealization;
        }
    }
}
