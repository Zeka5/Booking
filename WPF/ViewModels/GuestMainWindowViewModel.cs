using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels
{
    public class GuestMainWindowViewModel : IObserver
    {
        public ObservableCollection<GuestNotificationDto> Notifications { get; set; }
        public GuestNotificationService GuestNotificationService { get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }
        public NavigationService NavigationService { get; set; }
        public MyICommand NavigateToHomePageCommand { get; set; }
        public MyICommand NavigateBackCommand { get; set; }
        public MyICommand<Grid> OpenMenuCommand { get; set; }
        public MyICommand<Grid> CloseMenuCommand { get; set; }
        public MyICommand<Grid> OpenNotificationsCommand { get; set; }
        public MyICommand<Grid> CloseNotificationsCommand { get; set; }
        public MyICommand<Grid> ViewChangesCommand { get; set; }
        public MyICommand<GuestNotificationDto> DeleteNotificationCommand { get; set; }
        public MyICommand<Grid> OpenMyReservationsCommand { get; set; }
        public MyICommand<Grid> OpenPersonalDataCommand { get; set; }
        private User user;
        public GuestMainWindowViewModel(User user, NavigationService navigationService) {
            NavigationService = navigationService;
            OpenMyReservationsCommand = new MyICommand<Grid>(ExecuteNavigationToMyReservations);
            OpenPersonalDataCommand = new MyICommand<Grid>(ExecuteNavigationToPersonalData);
            DeleteNotificationCommand = new MyICommand<GuestNotificationDto>(ExecuteNotificationDeleting);
            ViewChangesCommand = new MyICommand<Grid>(ExecuteNavigationToMyRequests);
            NavigateToHomePageCommand = new MyICommand(ExecuteNavigationToHomePage);
            NavigateBackCommand = new MyICommand(ExecuteNavigationBack);
            OpenMenuCommand = new MyICommand<Grid>(ExecuteMenuOpening);
            CloseMenuCommand = new MyICommand<Grid>(ExecuteMenuClosing);
            OpenNotificationsCommand = new MyICommand<Grid>(ExecuteNotificationsOpening);
            CloseNotificationsCommand = new MyICommand<Grid>(ExecuteNotificationsClosing);
            AccommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>(), new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(), new LocationService(Injector.CreateInstance<ILocationRepository>()), new ImageService(Injector.CreateInstance<IImageRepository>())));
            GuestNotificationService = new GuestNotificationService(Injector.CreateInstance<IGuestNotificationRepository>(), AccommodationReservationService);
            Notifications = new ObservableCollection<GuestNotificationDto>();
            this.user = user;
            GuestNotificationService.Subscribe(this);
            Update();
        }
        public void Update()
        {
            Notifications.Clear();
            List<GuestNotification> guestNotifications = GuestNotificationService.GetAllNotReadByUser(user);
            foreach (GuestNotification guestNotification in guestNotifications) {
                AccommodationReservation? accommodationReservation = AccommodationReservationService.GetById(guestNotification.AccommodationReservationId);
                Accommodation? accommodation = AccommodationReservationService.GetAccommodationById(accommodationReservation.AccommodationId);
                Notifications.Add(new GuestNotificationDto(guestNotification, accommodation));
            }
        }
        private void ExecuteNotificationDeleting(GuestNotificationDto guestNotificationDto) {
            GuestNotificationService.Delete(guestNotificationDto.ToGuestNotification());
        }
        private void ExecuteNavigationToHomePage() {
            NavigationService.Navigate(new MainPage(user, NavigationService));
        }
        private void ExecuteNavigationBack()
        {
            if (NavigationService.CanGoBack) NavigationService.GoBack();
        }
        private void ExecuteMenuOpening(Grid menuGrid) {
            menuGrid.Visibility = Visibility.Visible;
        }
        private void ExecuteMenuClosing(Grid menuGrid)
        {
            menuGrid.Visibility = Visibility.Collapsed;
        }
        private void ExecuteNotificationsOpening(Grid notificationsGrid)
        {
            notificationsGrid.Visibility = Visibility.Visible;
        }
        private void ExecuteNotificationsClosing(Grid notificatiosGrid)
        {
            notificatiosGrid.Visibility = Visibility.Collapsed;
        }
        private void ExecuteNavigationToMyRequests(Grid notificationsGrid)
        {
            notificationsGrid.Visibility = Visibility.Collapsed;
            NavigationService.Navigate(new MyReservations(user, 2, NavigationService));
        }
        private void ExecuteNavigationToMyReservations(Grid notificationsGrid) {
            notificationsGrid.Visibility = Visibility.Collapsed;
            NavigationService.Navigate(new MyReservations(user, 0, NavigationService));
        }
        private void ExecuteNavigationToPersonalData(Grid grid) {
            grid.Visibility = Visibility.Collapsed;
            NavigationService.Navigate(new GuestPersonalData(user,NavigationService));
        }


    }
}
