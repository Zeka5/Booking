using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class RatingNotificationDto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int ReservationId { get; set; }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    OnPropertyChanged();
                }
            }
        }

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

        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                if (accommodationName != value)
                {
                    accommodationName = value;
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

        public RatingNotificationDto()
        {
            
        }

        public RatingNotificationDto(int id, int guestId, int reservationId, string imagePath, string guestUsername,
            string accommodationName, int daysLeft)
        {
            Id = id;
            GuestId = guestId;
            ReservationId = reservationId;
            this.imagePath = imagePath;
            this.guestUsername = guestUsername;
            this.accommodationName = accommodationName;
            this.daysLeft = daysLeft;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
