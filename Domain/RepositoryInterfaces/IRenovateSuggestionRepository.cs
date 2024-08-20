using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRenovateSuggestionRepository
    {
        void Add(RenovateSuggestion renovateSuggestion);
        List<RenovateSuggestion> GetAll();
        //void Subscribe(IObserver observer);
        int GetRenovateSuggestionCount(int reservationId);
    }
}
