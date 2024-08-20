using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.TourGuestViewModels
{
    public class ActiveToursViewModel
    {
        public TourRealizationService tourRealizationService;
        public static ObservableCollection<TourRealizationDto> TourRealizationList { get; set; }
        public TourRealizationDto? SelectedTourRealization { get; set; }
        public ActiveToursViewModel()
        {
            tourRealizationService = new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), new TourService(Injector.CreateInstance<ITourRepository>()), new LocationService(Injector.CreateInstance<ILocationRepository>()), new LanguageService(Injector.CreateInstance<ILanguageRepository>()), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>()));

            TourRealizationList = new ObservableCollection<TourRealizationDto>();

            LoadAcitveTourRealizations();
        }
        private void LoadAcitveTourRealizations()
        {
            TourRealizationList = tourRealizationService.LoadAcitveTourRealizations();
        }
    }
}
