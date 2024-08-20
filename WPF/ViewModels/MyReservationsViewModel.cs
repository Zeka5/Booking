using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels
{
    public class MyReservationsViewModel : IObserver
    {
        public ObservableCollection<AccommodationReservationDto> PastReservations { get; set; }
        public ObservableCollection<AccommodationReservationDto> UpcomingReservations { get; set; }
        public ObservableCollection<ReservationChangeRequestDto> Requests {  get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }
        public ReservationChangeRequestService ReservationChangeRequestService { get; set; }
        public AccommodationService AccommodationService { get; set; }
        public MyICommand<AccommodationReservationDto> NavigationToRateCommand { get; set; }
        public MyICommand<AccommodationReservationDto> CancelReservationCommand { get; set; }
        public MyICommand<AccommodationReservationDto> NavigationToModifyReservationCommand {  get; set; }
        public NavigationService NavigationService { get; set; }
        public AccommodationRatingService AccommodationRatingService { get; set; }
        private User user;
        public MyReservationsViewModel(User user ,NavigationService navigationService) {
            NavigationService = navigationService;
            UpcomingReservations = new ObservableCollection<AccommodationReservationDto>();
            PastReservations = new ObservableCollection<AccommodationReservationDto>();
            Requests = new ObservableCollection<ReservationChangeRequestDto>();
            NavigationToRateCommand = new MyICommand<AccommodationReservationDto>(ExecuteNavigationToRating,CanExecuteNavigationToRating);
            CancelReservationCommand = new MyICommand<AccommodationReservationDto>(ExecuteCancelingReservation);
            NavigationToModifyReservationCommand = new MyICommand<AccommodationReservationDto>(ExecuteNavigationToModifying, CanExecuteNavigationToModifying);
            AccommodationRatingService = new AccommodationRatingService(Injector.CreateInstance<IAccommodationRatingRepository>());
            AccommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(), new LocationService(Injector.CreateInstance<ILocationRepository>()), new ImageService(Injector.CreateInstance<IImageRepository>()));
            AccommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>(),AccommodationService);
            ReservationChangeRequestService = new ReservationChangeRequestService(Injector.CreateInstance<IReservationChangeRequestRepository>(),AccommodationReservationService);
            this.user = user;
            AccommodationReservationService.Subscribe(this);
            Update();
            UpdatePastReservations();
            UpdateRequests();
        }
        public void Update()
        {
            UpcomingReservations.Clear();
            List<AccommodationReservation> accommodationReservations = AccommodationReservationService.GetUpcomingReservations(user);
            foreach (AccommodationReservation accommodationReservation in accommodationReservations)
            {
                Accommodation? accommodation = AccommodationReservationService.GetAccommodationById(accommodationReservation.AccommodationId);
                Location? location = AccommodationService.GetLocationById(accommodation.LocationId);
                UpcomingReservations.Add(new AccommodationReservationDto(accommodationReservation, location, accommodation));
            }
            //  ReservationChangeRequestService.UpdateIsClickable(UpcomingReservations);
        }
        private void UpdatePastReservations()
        {
            PastReservations.Clear(); 
            List<AccommodationReservation> accommodationReservations = AccommodationReservationService.GetPastReservations(user);
            foreach (AccommodationReservation accommodationReservation in accommodationReservations)
            {
                Accommodation? accommodation = AccommodationReservationService.GetAccommodationById(accommodationReservation.AccommodationId);
                Location? location = AccommodationService.GetLocationById(accommodation.LocationId);
                PastReservations.Add(new AccommodationReservationDto(accommodationReservation, location, accommodation));
            }
        }
        private void UpdateRequests()
        {
            List<ReservationChangeRequest> reservationChangeRequests = ReservationChangeRequestService.GetAllByUser(user);
            foreach (ReservationChangeRequest request in reservationChangeRequests)
            {
                ReservationDate reservationDate = new ReservationDate(request.FirstDay, request.LastDay);
                AccommodationReservation? accommodationReservation = AccommodationReservationService.GetById(request.AccommodationReservationId);
                Accommodation accommodation = AccommodationReservationService.GetAccommodationById(accommodationReservation.AccommodationId);
                Requests.Add(new ReservationChangeRequestDto(request, reservationDate, accommodation));
            }
        }
        private void ExecuteCancelingReservation(AccommodationReservationDto accommodationReservationDto) {
            MessageBoxResult result = MessageBox.Show("Are you sure that you want to cancel this reservation?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;
            AccommodationReservationService.CancelReservationClick(accommodationReservationDto.ToAccommodationReservation());
        }
        public bool CanExecuteCancelingReservation(AccommodationReservationDto accommodationReservationDto) {
            return !AccommodationService.GetIsCancelable(accommodationReservationDto.ToAccommodationReservation());
        }
        private void ExecuteNavigationToRating(AccommodationReservationDto accommodationReservationDto) {
            NavigationService.Navigate(new RateOwnerPage(user,NavigationService,accommodationReservationDto.Id,accommodationReservationDto.AccommodationId));
        }
        private bool CanExecuteNavigationToRating(AccommodationReservationDto accommodationReservationDto) {
            return AccommodationRatingService.GetIsRateable(accommodationReservationDto.ToAccommodationReservation());
        }
        private void ExecuteNavigationToModifying(AccommodationReservationDto accommodationReservationDto)
        {
            NavigationService.Navigate(new ModifyReservation(user,NavigationService ,accommodationReservationDto.Id));
        }
        private bool CanExecuteNavigationToModifying(AccommodationReservationDto accommodationReservationDto)
        {
           return !ReservationChangeRequestService.IsClickable(accommodationReservationDto.Id);
        }
    }
}
