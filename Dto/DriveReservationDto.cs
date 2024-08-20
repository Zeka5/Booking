using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace BookingApp.Dto
{
    public class DriveReservationDto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int DriverId { get; set; }
        public string TourGuestDelay { get; set; }
        public string DriverDelay { get; set; }
        public int StartAddressId { get; set; }

        private string startAddress;
        public string StartAddress
        {
            get
            {
                return startAddress;
            }
            set
            {
                if (value != startAddress)
                {
                    startAddress = value;
                    OnPropertyChanged("StartAddress");
                }

            }
        }

        private string endAddress;
        public string EndAddress
        {
            get
            {
                return endAddress;
            }
            set
            {
                if (value != endAddress)
                {
                    endAddress = value;
                    OnPropertyChanged("EndAddress");
                }

            }
        }
        public int EndAddressId { get; set; }
        public string StartTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ReservationTime { get; set; }
        public bool IsFastReservation { get; set; }
        public string TouristName { get; set; }
        public string DriverName { get; set; }
        public int OriginalDriverId { get; set; }
        

        public DriveReservationDto()
        {
        }

        public DriveReservationDto(DriveReservation driveReservation, string startAddress, string endAddress, string driverName, string touristName = "")
        {
            Id = driveReservation.Id;
            TouristId = driveReservation.UserId;
            DriverId = driveReservation.DriverId;
            StartAddressId = driveReservation.StartAddressId;
            EndAddressId = driveReservation.EndAddressId;
            StartTime = driveReservation.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            IsFastReservation = driveReservation.IsFastReservation;
            ReservationTime = driveReservation.ReservationTime;
            DepartureTime = driveReservation.DepartureTime;
            OriginalDriverId = driveReservation.OriginalDriverId;

            StartAddress = startAddress;
            EndAddress = endAddress;
            TouristName = touristName;

            if (driveReservation.IsTourGuestLate) TourGuestDelay = "Late";
            else TourGuestDelay = "On time";

            if (driveReservation.IsDriverLate) DriverDelay = "Late";
            else DriverDelay = "On time";

            DriverName = driverName;
        }

        public DriveReservation ToDriveReservation()
        {
            bool isTourGuestLate = false;

            if (TourGuestDelay == "Late") isTourGuestLate = true;
            else isTourGuestLate = false;
            return new DriveReservation(Id, TouristId, StartAddressId, EndAddressId, DriverId, StartTime, IsFastReservation, ReservationTime, isTourGuestLate);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }
    }
}
