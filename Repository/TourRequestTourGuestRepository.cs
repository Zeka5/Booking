using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.Repository
{
    public class TourRequestTourGuestRepository : ITourRequestTourGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequestTourGuest.csv";

        private readonly Serializer<TourRequestTourGuest> serializer;

        private List<TourRequestTourGuest> tourRequestTourGuests;

        public Subject TourRequestTourGuesySubject;
        public TourRequestTourGuestRepository()
        {
            serializer = new Serializer<TourRequestTourGuest>();
            tourRequestTourGuests = serializer.FromCSV(FilePath);
            TourRequestTourGuesySubject = new Subject();
        }
        public List<TourRequestTourGuest> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }
        public TourRequestTourGuest GetById(int id)
        {
            tourRequestTourGuests = serializer.FromCSV(FilePath);
            return tourRequestTourGuests.Find(t => t.Id == id);
        }
        public int NextId()
        {
            tourRequestTourGuests = serializer.FromCSV(FilePath);
            if (tourRequestTourGuests.Count < 1)
            {
                return 1;
            }
            return tourRequestTourGuests.Max(t => t.Id) + 1;
        }

        public TourRequestTourGuest Add(TourRequestTourGuest tourRequestTourGuest)
        {
            tourRequestTourGuests = serializer.FromCSV(FilePath);
            tourRequestTourGuest.Id = NextId();
            tourRequestTourGuests.Add(tourRequestTourGuest);
            serializer.ToCSV(FilePath, tourRequestTourGuests);
            TourRequestTourGuesySubject.NotifyObservers();
            return tourRequestTourGuest;
        }

        public void Delete(TourRequestTourGuest tourRequestTourGuest)
        {
            tourRequestTourGuests = serializer.FromCSV(FilePath);
            TourRequestTourGuest founded = tourRequestTourGuests.Find(t => t.Id == tourRequestTourGuest.Id);
            tourRequestTourGuests.Remove(founded);
            serializer.ToCSV(FilePath, tourRequestTourGuests);
            TourRequestTourGuesySubject.NotifyObservers();
        }
        public List<TourRequestTourGuest> GetByTourRequestId(int tourRequestId)
        {
            tourRequestTourGuests = serializer.FromCSV(FilePath);
            return tourRequestTourGuests.FindAll(t => t.TourRequestId == tourRequestId);
        }
        public void Subscribe(IObserver observer)
        {
            TourRequestTourGuesySubject.Subscribe(observer);
        }

        public TourRequestTourGuest Update(TourRequestTourGuest tourRequestTourGuest)
        {
            tourRequestTourGuests = serializer.FromCSV(FilePath);
            TourRequestTourGuest current = tourRequestTourGuests.Find(t => t.Id == tourRequestTourGuest.Id);
            int index = tourRequestTourGuests.IndexOf(current);
            tourRequestTourGuests.Remove(current);
            tourRequestTourGuests.Insert(index, tourRequestTourGuest);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, tourRequestTourGuests);
            TourRequestTourGuesySubject.NotifyObservers();
            return tourRequestTourGuest;
        }
    }
}
