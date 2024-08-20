using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class Drive : DriveReservation, ISerializable
    {
        public int StartPrice { get; set; }
        public int EndPrice { get; set; }
        public DateTime EndTime { get; set; }

        public Drive(DriveReservation reservation, int startPrice, int endPrice, DateTime endTime)
        {
            DriverId = reservation.DriverId;
            DepartureTime = reservation.DepartureTime;
            UserId = reservation.UserId;
            StartAddressId = reservation.StartAddressId;
            EndAddressId = reservation.EndAddressId;
            StartPrice = startPrice;
            EndPrice = endPrice;
            EndTime = endTime;
        }

        public Drive() { }

        public string[] ToCSV()
        {

            string[] csvValues = {  Id.ToString(),
                                    DriverId.ToString(),
                                    UserId.ToString(),
                                    StartPrice.ToString(),
                                    EndPrice.ToString(),
                                    StartAddressId.ToString(),
                                    EndAddressId.ToString(),
                                    DepartureTime.ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture),
                                    EndTime.ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture)
                                 };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            DriverId = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            StartPrice = Convert.ToInt32(values[3]);
            EndPrice = Convert.ToInt32(values[4]);
            StartAddressId = Convert.ToInt32(values[5]);
            EndAddressId = Convert.ToInt32(values[6]);
            DepartureTime = DateTime.ParseExact(values[7], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            EndTime = DateTime.ParseExact(values[8], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        }
    }
}
