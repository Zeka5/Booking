using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class RatingNotificationService
    {
        private int ownerId;

        private IRatingNotificationRepository ratingNotificationRepository;
        private AccommodationReservationService accommodationReservationService;

        public RatingNotificationService(IRatingNotificationRepository ratingNotificationRepository)
        {
            this.ratingNotificationRepository = ratingNotificationRepository;
        }

        public RatingNotificationService(IRatingNotificationRepository ratingNotificationRepository,
            AccommodationReservationService accommodationReservationService,
            int ownerId)
        {
            this.ratingNotificationRepository = ratingNotificationRepository;
            this.accommodationReservationService = accommodationReservationService;
            this.ownerId = ownerId;
        }

        public RatingNotification? GetById(int id)
        {
            return ratingNotificationRepository.GetById(id);
        }

        /// <summary>
        /// logicko brisanje
        /// </summary>
        public void Delete(int id)
        {
            RatingNotification? notification = GetById(id);
            notification.Deleted = true;
            ratingNotificationRepository.Update(notification);
        }

        /// <summary>
        /// fizicko brisanje
        /// </summary>
        public void Remove(int id)
        {
            ratingNotificationRepository.Remove(id);
        }

        public void RatingsSubscribe(IObserver observer)
        {
            ratingNotificationRepository.Subscribe(observer);
        }

        public void RemoveOldRatingNotifications()
        {
            List<RatingNotification> notificationsToDelete = new List<RatingNotification>();
            foreach (var notification in ratingNotificationRepository.GetAll())
                if (notification.IsExpired(accommodationReservationService.GetById(notification.ReservationId).LastDay))
                    notificationsToDelete.Add(notification);

            foreach (var notification in notificationsToDelete) ratingNotificationRepository.Remove(notification.Id);
        }

        public void GenerateNewRatingNotifications()
        {
            foreach (var reservation in accommodationReservationService.GetActiveReservationsByOwnerId(ownerId))
            {
                if (ratingNotificationRepository.ExistsWithReservationId(reservation.Id)) continue;
                else if (RatingNotification.IsRequired(reservation.LastDay))
                    ratingNotificationRepository.Add(new RatingNotification(reservation.Id, false));
            }
        }

        public IEnumerable<RatingNotification> GetAllUnrated()
        {
            return ratingNotificationRepository.GetAll().Where(notification => notification.Deleted == false).ToList();
        }

        public bool BelongsToOwner(int reservationId, int ownerId)
        {
            return accommodationReservationService.BelongsToOwner(reservationId, ownerId);
        }

        public Image GetImageByEntityIdAndType(int id, ResourceType type)
        {
            return accommodationReservationService.GetImageByEntityIdAndType(id, type);
        }

        public Accommodation GetAccommodationByReservationId(int id)
        {
            return accommodationReservationService.GetAccommodationByReservationId(id);
        }

        public AccommodationReservation GetReservationById(int id)
        {
            return accommodationReservationService.GetById(id);
        }

        public void NotifyRatingNotifications()
        {
            ratingNotificationRepository.Notify();
        }
    }
}
