using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.WPF.ViewModels
{
    public class NotificationsViewModel : IObserver
    {
        private RatingNotificationService ratingNotificationService;
        private UserService userService;

        private int ownerId;

        public ObservableCollection<RatingNotificationDto> RatingNotifications { get; set; }

        public NotificationsViewModel(int ownerId)
        {
            this.ownerId = ownerId;
            ratingNotificationService =
                new RatingNotificationService
                (
                    Injector.CreateInstance<IRatingNotificationRepository>(),
                    new AccommodationReservationService
                    (
                        Injector.CreateInstance<IAccommodationReservationRepository>(),
                        new AccommodationService
                        (
                            Injector.CreateInstance<IAccommodationRepository>(),
                            new LocationService(Injector.CreateInstance<ILocationRepository>()),
                            new ImageService(Injector.CreateInstance<IImageRepository>())
                        )
                    ),
                    ownerId
                );
            userService = new UserService(Injector.CreateInstance<IUserRepository>());

            ratingNotificationService.RemoveOldRatingNotifications();
            ratingNotificationService.GenerateNewRatingNotifications();

            ratingNotificationService.RatingsSubscribe(this);
            RatingNotifications = new ObservableCollection<RatingNotificationDto>();
            Update();
        }

        public void Update()
        {
            ratingNotificationService.GenerateNewRatingNotifications();
            RatingNotifications.Clear();
            foreach (var notification in ratingNotificationService.GetAllUnrated())
            {
                if (!ratingNotificationService.BelongsToOwner(notification.ReservationId, ownerId)) continue;
                var reservation = ratingNotificationService.GetReservationById(notification.ReservationId);
                var accommodation = ratingNotificationService.GetAccommodationByReservationId(notification.ReservationId);

                Domain.Model.Image image = ratingNotificationService.GetImageByEntityIdAndType(accommodation.Id,ResourceType.Accommodation);
                string guestUsername = userService.GetById(reservation.UserId).Username;
                int daysLeft = RatingNotification.GetDaysLeft((DateTime.Today - reservation.LastDay).Days);
                RatingNotifications.Add(new RatingNotificationDto(notification.Id, reservation.UserId, reservation.Id,
                    (image is null) ? Domain.Model.Image.defaultAccommodationImagePath : image.Path,
                    guestUsername, accommodation.Name, daysLeft));
            }
        }

        public void Rate(object sender)
        {
            Button button = (Button)sender;
            var selectedNotification = button.DataContext as RatingNotificationDto;
            var rateGuest = new RateGuest(selectedNotification);
            rateGuest.ShowDialog();
        }
    }
}
