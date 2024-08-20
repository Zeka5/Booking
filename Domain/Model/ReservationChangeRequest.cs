using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace BookingApp.Domain.Model
{
    public enum Status { Waiting, Accepted, Declined };
    public class ReservationChangeRequest : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationReservationId { get; set; }
        public DateTime FirstDay { get; set; }
        public DateTime LastDay { get; set; }
        public Status Status { get; set; }
        public string Comment { get; set; }

        public ReservationChangeRequest() { }
        public ReservationChangeRequest(int id, int accommodationReservationId, DateTime firstDay, DateTime lastDay, Status status,
            string comment)
        {
            Id = id;
            AccommodationReservationId = accommodationReservationId;
            FirstDay = firstDay;
            LastDay = lastDay;
            Status = status;
            Comment = comment;
        }

        public bool IsWaiting()
        {
            return this.Status == Status.Waiting;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationReservationId.ToString(),
                FirstDay.ToString("dd/MM/yyyy"),
                LastDay.ToString("dd/MM/yyyy"),
                Status.ToString(),
                Comment
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            if (values.Length == 1) return;
            Id = int.Parse(values[0]);
            AccommodationReservationId = int.Parse(values[1]);
            string[] firstDay = values[2].Split('/');
            string[] lastDay = values[3].Split('/');
            FirstDay = new DateTime(Convert.ToInt32(firstDay[2]), Convert.ToInt32(firstDay[1]), Convert.ToInt32(firstDay[0]));
            LastDay = new DateTime(Convert.ToInt32(lastDay[2]), Convert.ToInt32(lastDay[1]), Convert.ToInt32(lastDay[0]));
            Status = Enum.TryParse(values[4], out Status status) ? status : Status.Waiting;
            Comment = values[5];
        }
    }
}
