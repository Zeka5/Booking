using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class RenovateSuggestionService
    {
        private IRenovateSuggestionRepository renovateSuggestionRepository;
        public RenovateSuggestionService(IRenovateSuggestionRepository renovateSuggestionRepository)
        {
            this.renovateSuggestionRepository = renovateSuggestionRepository;
        }
        public void Add(RenovateSuggestion renovateSuggestion)
        {
            renovateSuggestionRepository.Add(renovateSuggestion);
        }

        public int GetRenovationSuggestionCount(int reservationId)
        {
            return renovateSuggestionRepository.GetRenovateSuggestionCount(reservationId);
        }
    }
}
