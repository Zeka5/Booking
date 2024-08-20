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
    public class TourRatingRepository:ITourRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRating.csv";

        private readonly Serializer<TourRating> serializer;

        private List<TourRating> tourRatings;

        public Subject TourRatingSubject;


        public TourRatingRepository()
        {
            serializer = new Serializer<TourRating>();
            tourRatings = serializer.FromCSV(FilePath);
            TourRatingSubject = new Subject();
        }
        public List<TourRating> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }
        public TourRating GetById(int id)
        {
            tourRatings = serializer.FromCSV(FilePath);
            return tourRatings.Find(tr => tr.Id == id);
        }
        public TourRating GetLast()
        {
            tourRatings = serializer.FromCSV(FilePath);
            return tourRatings.LastOrDefault();
        }

        public int NextId()
        {
            tourRatings = serializer.FromCSV(FilePath);
            if (tourRatings.Count < 1)
            {
                return 1;
            }
            return tourRatings.Max(t => t.Id) + 1;
        }

        public TourRating Add(TourRating tourRating)
        {
            tourRatings = serializer.FromCSV(FilePath);
            tourRating.Id = NextId();
            tourRatings.Add(tourRating);
            serializer.ToCSV(FilePath, tourRatings);
            return tourRating;
        }
        public TourRating Update(TourRating tourRating)
        {
            tourRatings = serializer.FromCSV(FilePath);
            TourRating current = tourRatings.Find(tg => tg.Id == tourRating.Id);
            int index = tourRatings.IndexOf(current);
            tourRatings.Remove(current);
            tourRatings.Insert(index, tourRating);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, tourRatings);
            TourRatingSubject.NotifyObservers();
            return tourRating;
        }


        public void Delete(TourRating tourRating)
        {
            tourRatings = serializer.FromCSV(FilePath);
            TourRating founded = tourRatings.Find(t => t.Id == tourRating.Id);
            tourRatings.Remove(founded);
            serializer.ToCSV(FilePath, tourRatings);
        }

        public void Subscribe(IObserver observer)
        {
            TourRatingSubject.Subscribe(observer);
        }
    }
}
