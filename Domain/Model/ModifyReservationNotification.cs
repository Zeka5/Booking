using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    internal class ModifyReservationNotification : ISerializable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public DateTime FirstDay { get; set; }
        public DateTime LastDay { get; set; }

        public ModifyReservationNotification()
        {
            
        }

        public ModifyReservationNotification(int id, int reservationId, DateTime firstDay, DateTime lastDay)
        {
            Id = id;
            ReservationId = reservationId;
            FirstDay = firstDay;
            LastDay = lastDay;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReservationId.ToString(),
                FirstDay.ToString("dd/MM/yyyy"),
                LastDay.ToString("dd/MM/yyyy")
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            FirstDay = DateTime.ParseExact(values[2], "dd/MM/yyyy", null);
            LastDay = DateTime.ParseExact(values[3], "dd/MM/yyyy", null);
        }
    }
}
