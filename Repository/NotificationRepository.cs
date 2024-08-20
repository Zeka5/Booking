using BookingApp.Domain.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class NotificationRepository
    {
        private const string filePath = "../../../Resources/Data/notifications.csv";
        private readonly Serializer<Notification> serializer;
        private readonly List<Notification> notifications;
        public Subject NotificationSubject;

        public NotificationRepository()
        {
            serializer = new Serializer<Notification>();
            notifications = serializer.FromCSV(filePath);
            this.NotificationSubject = new Subject();

            FilterByLastDay();
        }

        private void FilterByLastDay()
        {
            //notifications.Where(x => x.ReservationLastDay.Date > DateTime.Today).ToList();
            serializer.ToCSV(filePath, notifications);
        }

        private int GenerateId()
        {
            if (notifications.Count == 0) return 1;
            else return notifications[^1].Id + 1;
        }

        public void Add(Notification notification)
        {
            notification.Id = GenerateId();
            notifications.Add(notification);
            serializer.ToCSV(filePath, notifications);
            this.NotificationSubject.NotifyObservers();
        }

        public void Update(Notification notification)
        {
            Notification? oldNotification = GetById(notification.Id);
            if (oldNotification is null) return;

            //oldNotification.GuestRated = notification.GuestRated;
            //oldNotification.ReservationLastDay = notification.ReservationLastDay;
            serializer.ToCSV(filePath, notifications);
            this.NotificationSubject.NotifyObservers();
        }

        public void Remove(int id)
        {
            Notification? notification = GetById(id);
            if (notification is null) return;

            notifications.Remove(notification);
            serializer.ToCSV(filePath, notifications);
            this.NotificationSubject.NotifyObservers();
        }

        public List<Notification> GetAll()
        {
            return notifications;
        }

        public List<Notification> GetByOwnerId(int id)
        {
            //return notifications.Where(x => x.OwnerId == id).ToList();
            return null;
        }

        private Notification? GetById(int id)
        {
            return notifications.FirstOrDefault(x => x.Id == id);
        }

        public bool NotificationExists(int ownerId, int guestId, DateTime reservationLastDay)
        {
            //return notifications.Any(notification =>
            //    notification.OwnerId == ownerId &&
            //    notification.GuestId == guestId &&
            //    notification.ReservationLastDay == reservationLastDay
            //    );
            return false;
        }

        public void Subscribe(IObserver observer)
        {
            NotificationSubject.Subscribe(observer);
        }
    }
}
