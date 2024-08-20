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
    public class TourReservationRepository:ITourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReservation.csv";

        private readonly Serializer<TourReservation> serializer;

        private List<TourReservation> tourReservations;

        public TourReservationRepository()
        {
            serializer = new Serializer<TourReservation>();
            tourReservations = serializer.FromCSV(FilePath);
        }
        public List<TourReservation> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }
        public TourReservation GetById(int id)
        {
            tourReservations = serializer.FromCSV(FilePath);
            return tourReservations.Find(t => t.Id == id);
        }
        public TourReservation GetLast()
        {
            tourReservations = serializer.FromCSV(FilePath);
            return tourReservations.LastOrDefault();
        }

        public TourReservation GetByTourStartTimeId(int id)
        {
            tourReservations = serializer.FromCSV(FilePath);
            return tourReservations.Find(tr => tr.TourRealizationId == id);
        }

        public List<TourReservation> GetAllByTourStartTimeId(int id)
        {
            tourReservations = serializer.FromCSV(FilePath);
            return tourReservations.FindAll(tr => tr.TourRealizationId == id);
        }

        public TourReservation GetByTourRelizationIdAndUserId(int tourRealizationId, int userId)
        {
            tourReservations = serializer.FromCSV(FilePath);
            return tourReservations.Find(tr => tr.TourRealizationId == tourRealizationId && tr.UserId == userId);
        }

        public int NextId()
        {
            tourReservations = serializer.FromCSV(FilePath);
            if (tourReservations.Count < 1)
            {
                return 1;
            }
            return tourReservations.Max(t => t.Id) + 1;
        }

        public TourReservation Add(TourReservation tourReservation)
        {
            tourReservations = serializer.FromCSV(FilePath);
            tourReservation.Id = NextId();
            tourReservations.Add(tourReservation);
            serializer.ToCSV(FilePath, tourReservations);
            return tourReservation;
        }
        public void Delete(TourReservation tourReservation)
        {
            tourReservations = serializer.FromCSV(FilePath);
            TourReservation founded = tourReservations.Find(t => t.Id == tourReservation.Id);
            tourReservations.Remove(founded);
            serializer.ToCSV(FilePath, tourReservations);
        }
        public void Update(TourReservation tourReservation)
        {
            tourReservations = serializer.FromCSV(FilePath);
            TourReservation current = tourReservations.Find(tg => tg.Id == tourReservation.Id);
            int index = tourReservations.IndexOf(current);
            tourReservations.Remove(current);
            tourReservations.Insert(index, tourReservation);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, tourReservations);
            //TourGuestSubject.NotifyObservers();
            //return tourReservation;
        }
    }
}
