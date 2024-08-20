using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class RenovateSuggestion : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationReservationId { get; set; }
        public int RenovationLevel { get; set; }

        public RenovateSuggestion() { }
        public RenovateSuggestion(int accommodaitonReservationId, int renovationLevel) {
            AccommodationReservationId = accommodaitonReservationId;
            RenovationLevel = renovationLevel;
        }
        public RenovateSuggestion(int id, int accommodaitonReservationId, int renovationLevel)
        {
            Id = id;
            AccommodationReservationId = accommodaitonReservationId;
            RenovationLevel = renovationLevel;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationReservationId.ToString(),
                RenovationLevel.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationReservationId = int.Parse(values[1]);
            RenovationLevel = int.Parse(values[2]);
        }
    }
}
