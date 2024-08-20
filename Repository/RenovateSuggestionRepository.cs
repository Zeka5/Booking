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
    public class RenovateSuggestionRepository : IRenovateSuggestionRepository
    {
        private const string FilePath = "../../../Resources/Data/renovateSuggestions.csv";
        private readonly Serializer<RenovateSuggestion> serializer;
        private List<RenovateSuggestion> renovateSuggestions;
        public Subject RenovateSuggestionSubject;

        public RenovateSuggestionRepository()
        {
            serializer = new Serializer<RenovateSuggestion>();
            renovateSuggestions = serializer.FromCSV(FilePath);
            RenovateSuggestionSubject = new Subject();
        }
        private int GenerateId()
        {
            if (renovateSuggestions.Count == 0) return 1;
            else return renovateSuggestions[^1].Id + 1;
        }
        public void Add(RenovateSuggestion renovateSuggestion)
        {
            renovateSuggestion.Id = GenerateId();
            renovateSuggestions.Add(renovateSuggestion);
            serializer.ToCSV(FilePath, renovateSuggestions);
            RenovateSuggestionSubject.NotifyObservers();
        }
        public void Subscribe(IObserver observer) {
            RenovateSuggestionSubject.Subscribe(observer);
        }

        public List<RenovateSuggestion> GetAll()
        {
           return renovateSuggestions;
        }

        public int GetRenovateSuggestionCount(int reservationId)
        {
            return renovateSuggestions.Count(suggestion => suggestion.AccommodationReservationId == reservationId);
        }
    }
}
