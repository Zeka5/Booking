using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.TourGuestViewModels
{
    public class MyTourRequestsViewModel
    {
        public TourRequestService tourRequestService;
        public LocationService locationService;
        public LanguageService languageService;
        public static List<TourRequestDto> TourRequestList { get; set; }
        public MyTourRequestsViewModel()
        {
            tourRequestService = new TourRequestService(Injector.CreateInstance<ITourRequestRepository>(), new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), new TourService(Injector.CreateInstance<ITourRepository>()), new LocationService(Injector.CreateInstance<ILocationRepository>()), new LanguageService(Injector.CreateInstance<ILanguageRepository>()), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>())));
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            TourRequestList = new List<TourRequestDto>();

            LoadTourRequests();
        }
        private void LoadTourRequests()
        {
            TourRequestList.Clear();

            foreach(var tourRequest in tourRequestService.GetAll())
            {
                TourRequestList.Add(new TourRequestDto(tourRequest, locationService.GetById(tourRequest.LocationId), languageService.GetById(tourRequest.LanguageId)));
            }
        }
    }
}
