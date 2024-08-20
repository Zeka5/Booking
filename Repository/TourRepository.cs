using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class TourRepository:ITourRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> serializer;

        private List<Tour> tours;

        public Subject TourSubject;

        public TourRepository()
        {
            serializer = new Serializer<Tour>();
            tours = serializer.FromCSV(FilePath);
            TourSubject = new Subject();
        }
        public List<Tour> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }
        public Tour GetById(int id)
        {
            tours = serializer.FromCSV(FilePath);
            return tours.Find(t => t.Id == id);
        }
        public int NextId()
        {
            tours = serializer.FromCSV(FilePath);
            if (tours.Count < 1)
            {
                return 1;
            }
            return tours.Max(t => t.Id) + 1;
        }

        public Tour Add(Tour tour)
        {
            tours = serializer.FromCSV(FilePath);
            tour.Id = NextId();
            tours.Add(tour);
            serializer.ToCSV(FilePath, tours);
            TourSubject.NotifyObservers();
            return tour;
        }

        public void Delete(Tour tour)
        {
            tours = serializer.FromCSV(FilePath);
            Tour founded = tours.Find(t => t.Id == tour.Id);
            tours.Remove(founded);
            serializer.ToCSV(FilePath, tours);
            TourSubject.NotifyObservers();
        }

        public Tour GetLast()
        {
            return tours[^1];
        }
        public Tour Update(Tour tour)
        {
            tours = serializer.FromCSV(FilePath);
            Tour current = tours.Find(t => t.Id == tour.Id);
            int index = tours.IndexOf(current);
            tours.Remove(current);
            tours.Insert(index, tour);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, tours);
            TourSubject.NotifyObservers();
            return tour;
        }
        public void Subscribe(IObserver observer)
        {
            TourSubject.Subscribe(observer);
        }
    }
}
