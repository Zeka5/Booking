using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Renovation : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Renovation()
        {
            
        }

        public Renovation(int id, int accommodationId, DateTime startDate, DateTime endDate)
        {
            Id = id;
            AccommodationId = accommodationId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationId.ToString(),
                StartDate.ToString("dd/MM/yyyy"),
                EndDate.ToString("dd/MM/yyyy")
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            StartDate = DateTime.ParseExact(values[2], "dd/MM/yyyy", null);
            EndDate = DateTime.ParseExact(values[3], "dd/MM/yyyy", null);
        }
    }
}
