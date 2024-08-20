using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.Domain.Model
{
    public class RatingNotification : ISerializable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public bool Deleted { get; set; }
        private const int deadline = 5;

        public RatingNotification()
        {
            
        }

        public RatingNotification(int reservationId, bool deleted)
        {
            ReservationId = reservationId;
            Deleted = deleted;
        }

        public bool IsExpired(DateTime lastDay)
        {
            if ((DateTime.Today - lastDay).Days > deadline) return true;
            return false;
        }

        public static bool IsRequired(DateTime lastDay)
        {
            if (DateTime.Today <= lastDay || DateTime.Today > lastDay.AddDays(deadline)) return false;
            return true;
        }

        public static int GetDaysLeft(int daysPast)
        {
            return deadline - daysPast + 1;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReservationId.ToString(),
                Deleted.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            Deleted = bool.Parse(values[2]);
        }
    }
}
