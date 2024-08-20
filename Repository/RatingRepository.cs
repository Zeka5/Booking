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
    internal class RatingRepository : IRatingRepository
    {
        private const string filePath = "../../../Resources/Data/ratings.csv";
        private readonly Serializer<Rating> serializer;
        private readonly List<Rating> ratings;
        public Subject RatingSubject;

        public RatingRepository()
        {
            serializer = new Serializer<Rating>();
            ratings = serializer.FromCSV(filePath);
            RatingSubject = new Subject();
        }

        private int GenerateId()
        {
            if (ratings.Count == 0) return 1;
            else return ratings[^1].Id + 1;
        }

        public void Add(Rating rating)
        {
            rating.Id = GenerateId();
            ratings.Add(rating);
            serializer.ToCSV(filePath, ratings);
            RatingSubject.NotifyObservers();
        }

        public List<Rating> GetAll()
        {
            return ratings;
        }

        public Rating? GetByReservationId(int id)
        {
            return ratings.FirstOrDefault(rating => rating.ReservationId == id);
        }

        public void Subscribe(IObserver observer)
        {
            RatingSubject.Subscribe(observer);
        }
    }
}
