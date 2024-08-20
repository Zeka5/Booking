using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourRequestNotificationRepository:ITourRequestNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequestsNotifications.csv";

        private readonly Serializer<TourRequestNotification> serializer;

        private List<TourRequestNotification> tourRequestNotifications;

        public TourRequestNotificationRepository()
        {
            serializer = new Serializer<TourRequestNotification>();
            tourRequestNotifications = serializer.FromCSV(FilePath);
        }
        public List<TourRequestNotification> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }
        public TourRequestNotification GetById(int id)
        {
            tourRequestNotifications = serializer.FromCSV(FilePath);
            return tourRequestNotifications.Find(trn => trn.Id == id);
        }
        public int NextId()
        {
            tourRequestNotifications = serializer.FromCSV(FilePath);
            if (tourRequestNotifications.Count < 1)
            {
                return 1;
            }
            return tourRequestNotifications.Max(trn => trn.Id) + 1;
        }

        public TourRequestNotification Add(TourRequestNotification tourRequestNotification)
        {
            tourRequestNotifications = serializer.FromCSV(FilePath);
            tourRequestNotification.Id = NextId();
            tourRequestNotifications.Add(tourRequestNotification);
            serializer.ToCSV(FilePath, tourRequestNotifications);
            return tourRequestNotification;
        }
        public TourRequestNotification Update(TourRequestNotification tourRequestNotification)
        {
            tourRequestNotifications = serializer.FromCSV(FilePath);
            TourRequestNotification current = tourRequestNotifications.Find(trn => trn.Id == tourRequestNotification.Id);
            int index = tourRequestNotifications.IndexOf(current);
            tourRequestNotifications.Remove(current);
            tourRequestNotifications.Insert(index, tourRequestNotification);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, tourRequestNotifications);
            return tourRequestNotification;
        }


        public void Delete(TourRequestNotification tourRequestNotification)
        {
            tourRequestNotifications = serializer.FromCSV(FilePath);
            TourRequestNotification founded = tourRequestNotifications.Find(trn => trn.Id == tourRequestNotification.Id);
            tourRequestNotifications.Remove(founded);
            serializer.ToCSV(FilePath, tourRequestNotifications);
        }
    }
}
