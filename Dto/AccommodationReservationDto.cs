using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class AccommodationReservationDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int accommodationId;
        public int AccommodationId
        {
            get { return accommodationId; }

            set
            {
                if (accommodationId != value)
                {
                    accommodationId = value;
                    OnPropertyChanged("AccoommodatonId");
                }
                   
            }
        }

        private string firstDay;

        public string FirstDay {
            get
            {
                return firstDay;
            }
            set
            {
                if (firstDay != value)
                {
                    firstDay = value;
                    OnPropertyChanged("FirstDay");
                }
            }
        }

        private string lastDay;

        public string LastDay
        {
            get
            {
                return lastDay;
            }
            set
            {
                if (lastDay != value)
                {
                    lastDay = value;
                    OnPropertyChanged("LastDay");
                }
            }
        }

        private int daysToStay;

        public int DaysToStay
        {
            get
            {
                return daysToStay;
            }
            set
            {
                if (daysToStay != value) { 
                    daysToStay=value;
                    OnPropertyChanged("DaysToStay");
                }
            }
        }

        private int guestNumber;

        public int GuestNumber
        {
            get
            {
                return guestNumber;
            }
            set
            {
                if (guestNumber != value)
                {
                    guestNumber = value;
                    OnPropertyChanged("GuestNumber");
                }
            }
        }

        private int userId;

        public int UserId
        {
            get
            {
                return userId;
            }
            set
            {
                if (userId != value)
                {
                    userId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

        private Location location;

        public Location Location
        {
            get
            {
                return location;
            }
            set
            {
                if (location != value)
                {
                    location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private Domain.Model.Type type;

        public Domain.Model.Type Type
        {
            get
            {
                return type;
            }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        private string accommodationName;

        public string AccommodationName
        {
            get
            {
                return accommodationName;
            }
            set
            {
                if (accommodationName != value)
                {
                    accommodationName = value;
                    OnPropertyChanged("AccommodationName");
                }
            }
        }
        private string range;

        public string Range
        {
            get
            {
                return range;
            }
            set
            {
                if (range != value)
                {
                    range = value;
                    OnPropertyChanged("Range");
                }
            }
        }
        
        private ReservationStatus reservationStatus;
        public ReservationStatus ReservationStatus
        {
            get 
            {
                return reservationStatus;
            }
            set
            {
                if (value != reservationStatus)
                    reservationStatus = value;
                    OnPropertyChanged(nameof(ReservationStatus));
            }
        }
        public AccommodationReservationDto() { }
        public AccommodationReservationDto(AccommodationReservation accommodationReservation, Location location, Accommodation accommodation)
        {
            Id = accommodationReservation.Id;
            firstDay = accommodationReservation.FirstDay.ToString("dd/MM/yyyy");
            lastDay = accommodationReservation.LastDay.ToString("dd/MM/yyyy");
            accommodationId = accommodationReservation.AccommodationId;
            daysToStay = accommodationReservation.DaysToStay;
            guestNumber = accommodationReservation.GuestNumber;
            userId = accommodationReservation.UserId;
            type = accommodation.Type;
            accommodationName = accommodation.Name;
            this.location = location;
            Range = firstDay + "-" + lastDay;
            reservationStatus = accommodationReservation.Status;
        }
        public AccommodationReservation ToAccommodationReservation() {
            string[] valuesFirstDay= firstDay.Split('/');

            string[] valuesLastDay = lastDay.Split('/');
            valuesLastDay[2]=valuesLastDay[2].Substring(0, 4);
            valuesFirstDay[2]=valuesFirstDay[2].Substring(0,4);

            return new AccommodationReservation(Id, AccommodationId,new DateTime(Convert.ToInt32(valuesFirstDay[2]), Convert.ToInt32(valuesFirstDay[1]), Convert.ToInt32(valuesFirstDay[0])),
                new DateTime(Convert.ToInt32(valuesLastDay[2]), Convert.ToInt32(valuesLastDay[1]), Convert.ToInt32(valuesLastDay[0])), DaysToStay,GuestNumber,UserId,ReservationStatus);
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
