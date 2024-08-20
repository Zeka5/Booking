using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class DriveReservation : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DriverId { get; set; }
        public int StartAddressId { get; set; }
        public int EndAddressId { get; set; }
        public DateTime DepartureTime { get; set; }
        public bool IsFastReservation { get; set; }
        public DateTime ReservationTime { get; set; }
        public bool IsTourGuestLate { get; set; }
        public bool IsDriverLate { get; set; }
        public int OriginalDriverId { get; set; } //stores an id for a break, so if it is not accepted, id returns to the original one

        public DriveReservation() { }

        public DriveReservation(int userId, int startAdressId, int endAdressId, int driverId, string departureTime, bool isFastReservation = false)
        {
            UserId = userId;
            StartAddressId = startAdressId;
            EndAddressId = endAdressId;
            DriverId = driverId;
            DepartureTime = DateTime.Parse(departureTime);
            IsFastReservation = isFastReservation;
            ReservationTime = DateTime.Now;
            IsTourGuestLate = false;
            IsDriverLate = false;
            OriginalDriverId = 0;
        }
        public DriveReservation(int id, int userId, int startAdressId, int endAdressId, int driverId, string departureTime, bool isFastReservation = false)
        {
            Id = id;
            UserId = userId;
            StartAddressId = startAdressId;
            EndAddressId = endAdressId;
            DriverId = driverId;
            DepartureTime = DateTime.Parse(departureTime);
            IsFastReservation = isFastReservation;
            ReservationTime = DateTime.Now;
            IsTourGuestLate = false;
            IsDriverLate = false;
            OriginalDriverId = 0;
        }
        public DriveReservation(int id, int userId, int startAdressId, int endAdressId, int driverId, string departureTime, bool isFastReservation, DateTime reservationTime, bool isTourGuestLate = false)
        {
            Id = id;
            UserId = userId;
            StartAddressId = startAdressId;
            EndAddressId = endAdressId;
            DriverId = driverId;
            DepartureTime = DateTime.ParseExact(departureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            IsFastReservation = isFastReservation;
            ReservationTime = reservationTime;
            IsTourGuestLate = isTourGuestLate;
            IsDriverLate = false;
            OriginalDriverId = 0;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            DriverId = Convert.ToInt32(values[2]);
            StartAddressId = Convert.ToInt32(values[3]);
            EndAddressId = Convert.ToInt32(values[4]);
            DepartureTime = DateTime.ParseExact(values[5], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            IsFastReservation = Convert.ToBoolean(values[6]);
            ReservationTime = DateTime.ParseExact(values[7], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            IsTourGuestLate = Convert.ToBoolean(values[8]);
            IsDriverLate = Convert.ToBoolean(values[9]);
            OriginalDriverId = Convert.ToInt32(values[10]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(),
                                   UserId.ToString(),
                                   DriverId.ToString(),
                                   StartAddressId.ToString(),
                                   EndAddressId.ToString(),
                                   DepartureTime.ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture),
                                   IsFastReservation.ToString(),
                                   ReservationTime.ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture),
                                   IsTourGuestLate.ToString(),
                                   IsDriverLate.ToString(),
                                   OriginalDriverId.ToString()
            };
            return csvValues;
        }
    }
}
