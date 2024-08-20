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
    public class AccommodationRepository : IAccommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";
        private readonly Serializer<Accommodation> serializer;
        private List<Accommodation> accommodations;
        public Subject AccommodationSubject;

        public AccommodationRepository() {
            serializer = new Serializer<Accommodation>();
            accommodations = serializer.FromCSV(FilePath);
            AccommodationSubject = new Subject();
        }

        private int GenerateId()
        {
            if (accommodations.Count == 0) return 1;
            else return accommodations[^1].Id + 1;
        }

        public void Add(Accommodation accommodation)
        {
            accommodation.Id = GenerateId();
            accommodations.Add(accommodation);
            serializer.ToCSV(FilePath, accommodations);
            AccommodationSubject.NotifyObservers();
        }

        public List<Accommodation> GetAll() {
            return accommodations;
        }

        public Accommodation GetLast()
        {
            return accommodations[^1];
        }
        public List<Accommodation> GetByOwnerId(int id)
        {
            return accommodations.Where(x => x.OwnerId == id).ToList();
        }
        public Accommodation? GetById(int id)
        {
            return accommodations.Find(x => x.Id == id);
        }

        public bool GetIsCancelable(AccommodationReservation accommodationReservation) {
            Accommodation? accommodation = GetById(accommodationReservation.AccommodationId);
            if (accommodationReservation.FirstDay < DateTime.Today.AddDays(accommodation.CancellationDays))
            {
                return false;
            }
            return true;
        }
        public void Subscribe(IObserver observer) { 
            AccommodationSubject.Subscribe(observer);   
        }
    }
}
