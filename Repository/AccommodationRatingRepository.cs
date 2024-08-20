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
    public class AccommodationRatingRepository : IAccommodationRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationRatings.csv";
        private readonly Serializer<AccommodationRating> serializer;
        private List<AccommodationRating> accommodationRatings;
        public Subject AccommodationRatingSubject;

        public AccommodationRatingRepository() {
            serializer = new Serializer<AccommodationRating>();
            accommodationRatings = serializer.FromCSV(FilePath);
            AccommodationRatingSubject = new Subject();
        }
        private int GenerateId()
        {
            if (accommodationRatings.Count == 0) return 1;
            else return accommodationRatings[^1].Id + 1;
        }
        public void Add(AccommodationRating accommodationRating)
        {
            accommodationRating.Id = GenerateId();
            accommodationRatings.Add(accommodationRating);
            serializer.ToCSV(FilePath, accommodationRatings);
            AccommodationRatingSubject.NotifyObservers();
        }
        public List<AccommodationRating> GetAll() {
            return accommodationRatings;
        }
        public bool GetIsRateable(AccommodationReservation accommodationReservation)
        {
            List<AccommodationRating> accommodationRatings = GetAll();
            if (!accommodationRatings.Any(x => x.AccommodationReservationId == accommodationReservation.Id) && accommodationReservation.LastDay > DateTime.Today.AddDays(-5))
            {
                return true;
            }
            return false;
        }   

        public void Subscribe(IObserver observer)
        {
            AccommodationRatingSubject.Subscribe(observer);
        }
    }
}
