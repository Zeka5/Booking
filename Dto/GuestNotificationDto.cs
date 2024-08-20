using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class GuestNotificationDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int accommodationReservationId;
        public int AccommodationReservationId
        {
            get { return accommodationReservationId; }

            set
            {
                if (accommodationReservationId != value)
                {
                    accommodationReservationId = value;
                    OnPropertyChanged("AccoommodatonReservationId");
                }

            }
        }
        private int reservationChangeRequestId;
        public int ReservationChangeRequestId
        {
            get { return reservationChangeRequestId; }

            set
            {
                if (reservationChangeRequestId != value)
                {
                    reservationChangeRequestId = value;
                    OnPropertyChanged("ReservationChangeRequestId");
                }

            }
        }
        private string dateTime;
        public string DateTime
        {
            get { return dateTime; }

            set
            {
                if (dateTime != value)
                {
                    dateTime = value;
                    OnPropertyChanged("DateTime");
                }

            }
        }
        private string time;
        public string Time
        {
            get { return time; }

            set
            {
                if (time != value)
                {
                    time = value;
                    OnPropertyChanged("Time");
                }

            }
        }
        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }

            set
            {
                if (accommodationName != value)
                {
                    accommodationName = value;
                    OnPropertyChanged("AccommodationName");
                }

            }
        }
        private bool isRead;
        public bool IsRead
        {
            get { return isRead; }
            set
            {
                if (isRead != value)
                {
                    isRead = value;
                    OnPropertyChanged(nameof(IsRead));
                }
            }

        }
        public GuestNotificationDto() { }
        public GuestNotificationDto(int id , int accommodationReservationId, string dateTime)
        {
            Id=id;
            AccommodationReservationId=accommodationReservationId;
            DateTime = dateTime;  
        }
        public GuestNotificationDto(GuestNotification guestNotification, Accommodation accommodation) {
            Id = guestNotification.Id;
            ReservationChangeRequestId = guestNotification.ReservationChangeRequestId;
            AccommodationReservationId = guestNotification.AccommodationReservationId;
            DateTime = guestNotification.DateTime.ToString("dd/MM/yyyy");
            Time = guestNotification.DateTime.ToString("HH:mm");
            AccommodationName = accommodation.Name;
            IsRead = guestNotification.IsRead;
        }
        public GuestNotification ToGuestNotification() {
            string[] valuesDateTime = dateTime.Split('/');
            string[] valuesTime = Time.Split(":");
            return new GuestNotification(Id,ReservationChangeRequestId,AccommodationReservationId,new DateTime(Convert.ToInt32(valuesDateTime[2]), Convert.ToInt32(valuesDateTime[1]), Convert.ToInt32(valuesDateTime[0]), Convert.ToInt32(valuesTime[0]), Convert.ToInt32(valuesTime[1]),0));
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
