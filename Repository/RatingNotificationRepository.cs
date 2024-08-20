    using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class RatingNotificationRepository : IRatingNotificationRepository
    {
        private const string filePath = "../../../Resources/Data/ratingNotifications.csv";
        private readonly Serializer<RatingNotification> serializer;
        private readonly List<RatingNotification> ratingNotifications;
        public Subject ratingNotificationsSubject;

        public RatingNotificationRepository()
        {
            serializer = new Serializer<RatingNotification>();
            ratingNotifications = serializer.FromCSV(filePath);
            ratingNotificationsSubject = new Subject();
        }

        private int GenerateId()
        {
            if (ratingNotifications.Count == 0) return 1;
            else return ratingNotifications[^1].Id + 1;
        }

        public void Add(RatingNotification ratingNotification)
        {
            ratingNotification.Id = GenerateId();
            ratingNotifications.Add(ratingNotification);
            serializer.ToCSV(filePath, ratingNotifications);
            ratingNotificationsSubject.NotifyObservers();
        }

        public void Update(RatingNotification notification)
        {
            RatingNotification oldNotification = GetById(notification.Id);
            if (oldNotification is null) return;
            oldNotification.Deleted = notification.Deleted;
            serializer.ToCSV(filePath, ratingNotifications);
            ratingNotificationsSubject.NotifyObservers();
        }

        public void Remove(int id)
        {
            RatingNotification notification = GetById(id);
            if (notification is null) return;
            ratingNotifications.Remove(notification);
            serializer.ToCSV(filePath, ratingNotifications);
            ratingNotificationsSubject.NotifyObservers();
        }

        public RatingNotification? GetById(int id)
        {
            return ratingNotifications.Find(x => x.Id == id);
        }

        public List<RatingNotification> GetAll()
        {
            return ratingNotifications;
        }

        public bool ExistsWithReservationId(int id)
        {
            return ratingNotifications.Any(notification => notification.ReservationId == id);
        }

        public void Subscribe(IObserver observer)
        {
            ratingNotificationsSubject.Subscribe(observer);
        }

        public void Notify()
        {
            ratingNotificationsSubject.NotifyObservers();
        }
    }
}
