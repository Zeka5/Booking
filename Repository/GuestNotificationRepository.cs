using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class GuestNotificationRepository : IGuestNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/guestNotification.csv";
        private readonly Serializer<GuestNotification> serializer;
        private List<GuestNotification> guestNotifications;
        public Subject GuestNotificationSubject;
        public GuestNotificationRepository()
        {
            serializer = new Serializer<GuestNotification>();
            guestNotifications = serializer.FromCSV(FilePath);
            GuestNotificationSubject = new Subject();
        }
        public List<GuestNotification> GetAll()
        {
            return guestNotifications;
        }
        public List<GuestNotification> GetAllNotRead() {
            return guestNotifications.FindAll(x => x.IsRead == false);          
        }
        public void Subscribe(IObserver observer)
        {
            GuestNotificationSubject.Subscribe(observer);
        }

        public int GenerateId()
        {
            guestNotifications = serializer.FromCSV(FilePath);
            if (guestNotifications.Count < 1)
            {
                return 1;
            }
            return guestNotifications.Max(guestNotification => guestNotification.Id) + 1;
        }

        public GuestNotification Add(GuestNotification guestNotification)
        {

            if (File.ReadLines(FilePath).Count() != 0)
            {
                guestNotifications = serializer.FromCSV(FilePath);

            }
            else
            {
                guestNotifications = new List<GuestNotification>();
            }
            guestNotification.Id = GenerateId();
            guestNotifications.Add(guestNotification);
            serializer.ToCSV(FilePath, guestNotifications);
            GuestNotificationSubject.NotifyObservers();
            return guestNotification;
        }
        public void Delete(GuestNotification guestNotification)
        {
            GuestNotification? founded = guestNotifications.Find(ch => ch.Id == guestNotification.Id);
            int index = guestNotifications.IndexOf(founded);
            GuestNotification changed = founded;
            changed.IsRead = true;
            guestNotifications.Remove(founded);
            guestNotifications.Insert(index, changed);
            serializer.ToCSV(FilePath, guestNotifications);
            GuestNotificationSubject.NotifyObservers();
        }
    }
}
