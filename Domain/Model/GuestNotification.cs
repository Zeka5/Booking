using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class GuestNotification : ISerializable
    {
        public int Id { get; set; }
        public int ReservationChangeRequestId { get; set; }
        public DateTime DateTime { get; set; }
        public int AccommodationReservationId { get; set; }
        public bool IsRead { get; set; }
        public GuestNotification() { }
        public GuestNotification(ReservationChangeRequest request)
        {
            ReservationChangeRequestId = request.Id;
            AccommodationReservationId = request.AccommodationReservationId;
            DateTime = DateTime.Now;
            IsRead = false;
        }
        public GuestNotification(int id,int reservationChangeRequestId, int accommodationReservationId, DateTime dateTime) { 
            Id= id;
            ReservationChangeRequestId = reservationChangeRequestId;
            AccommodationReservationId= accommodationReservationId;
            DateTime = dateTime;
            IsRead = false;
        }
        public string[] ToCSV()
        {
            string[] values = { Id.ToString(),ReservationChangeRequestId.ToString() ,DateTime.ToString("dd/MM/yyyy HH:mm") ,AccommodationReservationId.ToString(),IsRead.ToString() };
            return values;
        }

        public void FromCSV(string[] values)
        {
            if (values.Count() == 1) return;
            Id = Convert.ToInt32(values[0]);
            ReservationChangeRequestId = Convert.ToInt32(values[1]);
            string[] dateTime = values[2].Split('/');
            string[] time = dateTime[2].Split(' ');
            string[] hoursMinutes = time[1].Split(':');
            DateTime = new DateTime(Convert.ToInt32(time[0]), Convert.ToInt32(dateTime[1]), Convert.ToInt32(dateTime[0]), Convert.ToInt32(hoursMinutes[0]), Convert.ToInt32(hoursMinutes[1]),0);
            AccommodationReservationId = Convert.ToInt32(values[3]);
            IsRead = bool.Parse(values[4]);
        }
    }
}
