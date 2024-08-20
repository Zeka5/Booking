using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels
{
    public class ModifyReservationViewModel
    {  
        public ReservationChangeRequestDto ReservationChangeRequestDto { get; set; }
        public ReservationChangeRequestService ReservationChangeRequestService { get; set; } 
        public AccommodationReservationService AccommodationReservationService { get; set; }
        public NavigationService NavigationService { get; set; }
        public MyICommand SendRequestCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        private User user;
        
        public ModifyReservationViewModel(User user, NavigationService navigationService, int accommodationReservationId) {
            this.user = user;
            NavigationService = navigationService;
            SendRequestCommand = new MyICommand(ExecuteSendingRequest);
            CancelCommand = new MyICommand(ExecuteCanceling);
            ReservationChangeRequestDto = new ReservationChangeRequestDto();
            ReservationChangeRequestService = new ReservationChangeRequestService(Injector.CreateInstance<IReservationChangeRequestRepository>(),
               new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>(),
               new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(), new LocationService(Injector.CreateInstance<ILocationRepository>()), new ImageService(Injector.CreateInstance<IImageRepository>()))));
            AccommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>(),
                new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(), new LocationService(Injector.CreateInstance<ILocationRepository>()), new ImageService(Injector.CreateInstance<IImageRepository>())));
            Update(accommodationReservationId);
        }
        private void Update(int accommodationReservationId)
        {
            ReservationChangeRequestDto.AccommodationReservationId = accommodationReservationId;
            ReservationChangeRequestDto.Status = Status.Waiting;
            AccommodationReservation accommodationReservation = AccommodationReservationService.GetById(ReservationChangeRequestDto.AccommodationReservationId);
            ReservationChangeRequestDto.FirstDay = accommodationReservation.FirstDay;
            ReservationChangeRequestDto.LastDay = accommodationReservation.LastDay;
        }
        private void ExecuteSendingRequest() {
            ReservationChangeRequest reservationChangeRequest = ReservationChangeRequestDto.ToReservationChangeRequest();
            ReservationChangeRequestService.Add(reservationChangeRequest);
            MessageBox.Show("Request sent succesfully");
            NavigationService.Navigate(new MyReservations(user, 1,NavigationService));
        }
        private void ExecuteCanceling() {
            NavigationService.GoBack();
        }
    }
}
