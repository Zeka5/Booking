using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class GroupDriveReservationDto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int StartAddressId { get; set; }
        public bool IsRead { get; set; }
        public int EndAddressId { get; set; }

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

        private GroupDriveStatus status;
        public GroupDriveStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private int numberOfGuests;
        public int NumberOfGuests
        {
            get
            {
                return numberOfGuests;
            }
            set
            {
                if (value != numberOfGuests)
                {
                    numberOfGuests = value;
                    OnPropertyChanged("NumberOfGuests");
                }

            }
        }

        private string departureTime;
        public string DepartureTime
        {
            get
            {
                return departureTime;
            }
            set
            {
                if (value != departureTime)
                {
                    departureTime = value;
                    OnPropertyChanged("DepartureTime");
                }

            }
        }
        public GroupDriveReservationDto()
        {

        }

        public GroupDriveReservationDto(GroupDriveReservation groupDriveReservation, string startAddress, string endAddress)
        {
            Id = groupDriveReservation.Id;
            StartAddressId = groupDriveReservation.StartAddressId;
            EndAddressId = groupDriveReservation.EndAddressId;
            Status = groupDriveReservation.Status;
            StartAddress = startAddress;
            EndAddress = endAddress;
            NumberOfGuests = groupDriveReservation.PassengerNumber;
            DepartureTime = groupDriveReservation.DepartureTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            IsRead = groupDriveReservation.IsRead;
        }

        /*public GroupDriveReservation ToGroupDriveReservation()
        {
            //return new GroupDriveReservation(Name, LastName, Age, Email, Phone, DateTime.ParseExact(MembershipDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
        }*/
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
