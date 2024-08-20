using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourGuest : ISerializable
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Years { get; set; }
        public int TourReservationId { get; set; }
        public string CheckPointName { get; set; }
        public bool ArrivalNotification { get; set; }
        public TourGuest()
        {

        }
        public TourGuest(string fullName, int years)
        {
            FullName = fullName;
            Years = years;
            CheckPointName = "Not arrived";
            ArrivalNotification = false;
        }
        public TourGuest(string fullName, int years, int tourReservationId, bool arrivalNotification = false)
        {
            FullName = fullName;
            Years = years;
            TourReservationId = tourReservationId;
            CheckPointName = "Not arrived";
            ArrivalNotification = arrivalNotification;
        }
        public TourGuest(int id, string fullName, int years, int tourReservationId, string checkPointName, bool arrivalNotification = false)
        {
            Id = id;
            FullName = fullName;
            Years = years;
            TourReservationId = tourReservationId; 
            CheckPointName = checkPointName;
            ArrivalNotification = arrivalNotification;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            FullName = values[1];
            Years = Convert.ToInt32(values[2]);
            TourReservationId = Convert.ToInt32(values[3]);
            CheckPointName = values[4];
            ArrivalNotification = Convert.ToBoolean(values[5]);
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), FullName, Years.ToString(),TourReservationId.ToString(), CheckPointName, ArrivalNotification.ToString() };
            return csvValues;
        }
    }
}
