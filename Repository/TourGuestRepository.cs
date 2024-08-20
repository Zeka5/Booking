using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourGuestRepository:ITourGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourGuests.csv";

        private readonly Serializer<TourGuest> serializer;

        private List<TourGuest> tourGuests;

        public Subject TourGuestSubject;


        public TourGuestRepository()
        {
            serializer = new Serializer<TourGuest>();
            tourGuests = serializer.FromCSV(FilePath);
            TourGuestSubject = new Subject();
        }
        public List<TourGuest> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }
        public TourGuest GetById(int id)
        {
            tourGuests = serializer.FromCSV(FilePath);
            return tourGuests.Find(t => t.Id == id);
        }
        public List<TourGuest> GetByTourReservationId(int tourReservationId)
        {
            List<TourGuest> tourGuests = serializer.FromCSV(FilePath);
            return tourGuests.FindAll(t => t.TourReservationId == tourReservationId);
        }

        public int NextId()
        {
            tourGuests = serializer.FromCSV(FilePath);
            if (tourGuests.Count < 1)
            {
                return 1;
            }
            return tourGuests.Max(t => t.Id) + 1;
        }

        public TourGuest Add(TourGuest tourGuest)
        {
            tourGuests = serializer.FromCSV(FilePath);
            tourGuest.Id = NextId();
            tourGuests.Add(tourGuest);
            serializer.ToCSV(FilePath, tourGuests);
            return tourGuest;
        }
        public TourGuest Update(TourGuest tourGuest)
        {
            tourGuests = serializer.FromCSV(FilePath);
            TourGuest current = tourGuests.Find(tg => tg.Id == tourGuest.Id);
            int index = tourGuests.IndexOf(current);
            tourGuests.Remove(current);
            tourGuests.Insert(index, tourGuest);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, tourGuests);
            TourGuestSubject.NotifyObservers();
            return tourGuest;
        }


        public void Delete(TourGuest tourGuest)
        {
            tourGuests = serializer.FromCSV(FilePath);
            TourGuest founded = tourGuests.Find(t => t.Id == tourGuest.Id);
            tourGuests.Remove(founded);
            serializer.ToCSV(FilePath, tourGuests);
        }

        public void Subscribe(IObserver observer)
        {
            TourGuestSubject.Subscribe(observer);
        }
    }
}

