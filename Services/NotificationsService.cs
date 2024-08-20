using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.WPF.View;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class NotificationsService
    {
        private IAccommodationRepository accommodationRepository;
        private IAccommodationReservationRepository accommodationReservationRepository;
        private IRatingNotificationRepository ratingNotificationRepository;
        private IImageRepository imageRepository;
        private IUserRepository userRepository;

        private const int deadline = 5;
        private int ownerId;
        private const string defaultImagePath = "../../../Resources/Images/account.jpg";

        public NotificationsService(int ownerId)
        {
            accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            ratingNotificationRepository = Injector.CreateInstance<IRatingNotificationRepository>();
            imageRepository = Injector.CreateInstance<IImageRepository>();
            userRepository = Injector.CreateInstance<IUserRepository>();
            this.ownerId = ownerId;

            RemoveOldRatingNotifications();
            GenerateNewRatingNotifications();
        }

        private RatingNotificationDto? ToDto(RatingNotification notification)
        {
            AccommodationReservation? accommodationReservation = accommodationReservationRepository.GetById(notification.ReservationId);
            Accommodation accommodation = accommodationRepository.GetById(accommodationReservation.AccommodationId);
            if (accommodation.OwnerId != ownerId) return null;

            Image? image = imageRepository.GetFirstByEntityAndType(accommodation.Id, ResourceType.Accommodation);
            string guestUsername = userRepository.GetById(accommodationReservation.UserId).Username;
            int daysLeft = deadline - (DateTime.Today - accommodationReservation.LastDay).Days;
            return new RatingNotificationDto(notification.Id, accommodationReservation.UserId, notification.ReservationId,
                (image is null) ? defaultImagePath : image.Path, guestUsername, accommodation.Name, daysLeft);
        }

        private void RemoveOldRatingNotifications()
        {
            List<RatingNotification> notificationsToDelete = new List<RatingNotification>();
            foreach (var notification in ratingNotificationRepository.GetAll())
                if ((DateTime.Today - accommodationReservationRepository.GetById(notification.ReservationId).LastDay).Days >= deadline)
                    notificationsToDelete.Add(notification);
            foreach (var notification in notificationsToDelete) ratingNotificationRepository.Remove(notification.Id);
        }

        private void GenerateNewRatingNotifications()
        {
            List<Accommodation> accommodations = accommodationRepository.GetByOwnerId(ownerId);
            List<AccommodationReservation> reservations = accommodationReservationRepository.GetByAccommodationIds(
                accommodations.Select(acc => acc.Id).ToList());
            foreach (var reservation in reservations)
            {
                if (ratingNotificationRepository.ExistsWithReservationId(reservation.Id)) continue;
                else if (DateTime.Today < reservation.LastDay || DateTime.Today >= reservation.LastDay.AddDays(deadline)) continue;
                ratingNotificationRepository.Add(new RatingNotification(reservation.Id, false));
            }
        }

        public void Update(ObservableCollection<RatingNotificationDto> ratingNotifications)
        {
            ratingNotifications.Clear();
            foreach (var notification in ratingNotificationRepository.GetAll())
            {
                if (notification.Deleted) continue;
                var notificationDto = ToDto(notification);
                if (notificationDto is null) continue;
                ratingNotifications.Add(notificationDto);
            }
        }

        public void RemoveRatingNotification(RatingNotificationDto notification)
        {
            RatingNotification ratingNotification = ratingNotificationRepository.GetById(notification.Id);
            ratingNotification.Deleted = true;
            ratingNotificationRepository.Update(ratingNotification);
        }

        public void SubscribeRatingNotifications(IObserver observer)
        {
            ratingNotificationRepository.Subscribe(observer);
        }
    }
}
