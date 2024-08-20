using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.Repository
{
    public class RenovationRepository : IRenovationRepository
    {
        private const string FilePath = "../../../Resources/Data/renovations.csv";
        private readonly Serializer<Renovation> serializer;
        private List<Renovation> renovations;
        public Subject RenovationSubject { get; private set; }

        public RenovationRepository()
        {
            serializer = new Serializer<Renovation>();
            renovations = serializer.FromCSV(FilePath);
            RenovationSubject = new Subject();
        }

        private int GenerateId()
        {
            if (renovations.Count == 0) return 1;
            return renovations[^1].Id + 1;
        }

        public void Add(Renovation renovation)
        {
            renovation.Id = GenerateId();
            renovations.Add(renovation);
            serializer.ToCSV(FilePath, renovations);
            RenovationSubject.NotifyObservers();
        }

        public void Delete(int id)
        {
            var renovation = GetById(id);
            if (renovation is null) return;
            renovations.Remove(renovation);
            serializer.ToCSV(FilePath, renovations);
            RenovationSubject.NotifyObservers();
        }

        public Renovation? GetById(int id)
        {
            return renovations.FirstOrDefault(renovation => renovation.Id == id);
        }

        public List<Renovation> GetAll()
        {
            return renovations;
        }

        public void Subscribe(IObserver observer)
        {
            RenovationSubject.Subscribe(observer);
        }
    }
}
