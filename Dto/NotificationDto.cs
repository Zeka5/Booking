using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class NotificationDto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int GuestId { get; set; }
        public bool GuestRated { get; set; }
        public DateTime ReservationLastDay { get; set; }

        private string guestUsername;
        public string GuestUsername
        {
            get { return guestUsername; }
            set
            {
                if (guestUsername != value)
                {
                    guestUsername = value;
                    OnPropertyChanged();
                }
            }
        }

        private int daysLeft;
        public int DaysLeft
        {
            get { return daysLeft; }
            set
            {
                if (daysLeft != value)
                {
                    daysLeft = value;
                    OnPropertyChanged();
                }
            }
        }

        public NotificationDto(int ownerId, int guestId, bool guestRated, DateTime reservationLastDay, string guestUsername,
            int daysLeft)
        {
            OwnerId = ownerId;
            GuestId = guestId;
            GuestRated = GuestRated;
            ReservationLastDay = reservationLastDay;

            GuestUsername = guestUsername;
            DaysLeft = daysLeft;
        }

        public NotificationDto(Notification notification, string guestUsername, int daysLeft)
        {
            Id = notification.Id;
            //OwnerId = notification.OwnerId;
            //GuestId = notification.GuestId;
            //GuestRated = notification.GuestRated;
            //ReservationLastDay = notification.ReservationLastDay;

            GuestUsername = guestUsername;
            DaysLeft = daysLeft;
        }

        public Notification ToNotification()
        {
            //return new Notification(Id, OwnerId, GuestId, GuestRated, ReservationLastDay);
            return null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
