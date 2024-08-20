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
    public class TourRealizationRepository: ITourRealizationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourrealization.csv";

        private readonly Serializer<TourRealization> serializer;

        private List<TourRealization> tourRealizations;

        public Subject TourRealizationSubject;


        public TourRealizationRepository()
        {
            serializer = new Serializer<TourRealization>();
            tourRealizations = serializer.FromCSV(FilePath);
            TourRealizationSubject = new Subject();
        }
        public List<TourRealization> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }
        public TourRealization GetById(int id)
        {
            tourRealizations = serializer.FromCSV(FilePath);
            return tourRealizations.Find(tr => tr.Id == id);
        }

        public int NextId()
        {
            tourRealizations = serializer.FromCSV(FilePath);
            if (tourRealizations.Count < 1)
            {
                return 1;
            }
            return tourRealizations.Max(tr => tr.Id) + 1;
        }

        public TourRealization Add(TourRealization tourRealization)
        {
            tourRealizations = serializer.FromCSV(FilePath);
            tourRealization.Id = NextId();
            tourRealizations.Add(tourRealization);
            serializer.ToCSV(FilePath, tourRealizations);
            TourRealizationSubject.NotifyObservers();
            return tourRealization;
        }

        public TourRealization Update(TourRealization tourRealization)
        {
            tourRealizations = serializer.FromCSV(FilePath);
            TourRealization current = tourRealizations.Find(t => t.Id == tourRealization.Id);
            int index = tourRealizations.IndexOf(current);
            tourRealizations.Remove(current);
            tourRealizations.Insert(index, tourRealization);       // keep ascending order of ids in file 
            serializer.ToCSV(FilePath, tourRealizations);
            TourRealizationSubject.NotifyObservers();
            return tourRealization;
        }

        public void Delete(TourRealization tourRealization)
        {
            tourRealizations = serializer.FromCSV(FilePath);
            TourRealization founded = tourRealizations.Find(tr => tr.Id == tourRealization.Id);
            tourRealizations.Remove(founded);
            serializer.ToCSV(FilePath, tourRealizations);
            TourRealizationSubject.NotifyObservers();
        }

        public List<TourRealization> GetByTourId(int id)
        {
            tourRealizations = serializer.FromCSV(FilePath);
            return tourRealizations.FindAll(tst => tst.TourId == id);
        }

        public void Subscribe(IObserver observer)
        {
            TourRealizationSubject.Subscribe(observer);
        }
    }
}
