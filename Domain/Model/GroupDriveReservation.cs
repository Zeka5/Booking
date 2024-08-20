using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public enum GroupDriveStatus { Pending, Processed, Rejected }
    public class GroupDriveReservation : DriveReservation, ISerializable
    {
        public int Id {  get; set; }
        public int PassengerNumber { get; set; }
        public string Language { get; set; }
        public GroupDriveStatus Status { get; set; }
        public bool IsRead { get; set; }
        public GroupDriveReservation(DriveReservation reservation, int passengerNumber, string language)
        {
            PassengerNumber = passengerNumber;
            Language = language;
            StartAddressId = reservation.StartAddressId;
            EndAddressId = reservation.EndAddressId;
            DepartureTime = reservation.DepartureTime;
            ReservationTime = reservation.ReservationTime;
            UserId = reservation.UserId;
            Status = GroupDriveStatus.Pending;
            IsRead = false;
        }
        public GroupDriveReservation(int userId, int startAddressId, int endAddressId, string departureTime, int passengerNumber, string language)
        {
            PassengerNumber = passengerNumber;
            Language = language;
            StartAddressId = startAddressId;
            EndAddressId = endAddressId;
            DepartureTime = DateTime.Parse(departureTime);
            // RESERVATION TIME TI SETUJ KAD PRIHVATIS ILI STA VEC
            //TAKODJE DRIVER ID TI SETUJES
            UserId = userId;
            Status = GroupDriveStatus.Pending;
            IsRead = false;
        }

        public GroupDriveReservation() { }

        public string[] ToCSV()
        {

            string[] csvValues = {  Id.ToString(),
                                    UserId.ToString(),
                                    StartAddressId.ToString(),
                                    EndAddressId.ToString(),
                                    PassengerNumber.ToString(),
                                    Language.ToString(),
                                    DepartureTime.ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture),
                                    ReservationTime.ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture),
                                    IsRead.ToString(),
                                    Status.ToString()
                                 };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            StartAddressId = Convert.ToInt32(values[2]);
            EndAddressId = Convert.ToInt32(values[3]);
            PassengerNumber = Convert.ToInt32(values[4]);
            Language = values[5];
            DepartureTime = DateTime.ParseExact(values[6], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            ReservationTime = DateTime.ParseExact(values[7], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            IsRead = Convert.ToBoolean(values[8]);
            if (string.Equals(values[9], "Pending")) Status = GroupDriveStatus.Pending;
            else if (string.Equals(values[9], "Processed")) Status = GroupDriveStatus.Processed;
            else  Status = GroupDriveStatus.Rejected;
        }
    }
}
