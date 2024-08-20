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
    public class ReservationChangeRequestDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int accommodationReservationId;
        public int AccommodationReservationId
        {
            get
            {
                return accommodationReservationId;            
            }
            set 
            {
                if(accommodationReservationId != value)
                {
                    accommodationReservationId = value;
                    OnPropertyChanged(nameof(AccommodationReservationId));
                }
            }
        }
        private DateTime firstDay;
        public DateTime FirstDay
        {
            get
            {
                return firstDay;
            }
            set
            {
                if (firstDay != value)
                {
                    firstDay = value;
                    OnPropertyChanged(nameof(FirstDay));
                }
            }
        }
        private DateTime lastDay;
        public DateTime LastDay
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
                    OnPropertyChanged(nameof(LastDay));
                }
            }
        }
        private Status status;
        public Status Status
        {
            get
            {
                return status;
            }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }
        private string comment;
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                if (comment != value)
                {
                    comment = value;
                    OnPropertyChanged(nameof(Comment));
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
                    OnPropertyChanged(nameof(AccommodationName));
                }
            }
        }
        private string newDate;
        public string NewDate
        {
            get
            {
                return newDate;
            }
            set
            {
                if (newDate != value)
                {
                    newDate = value;
                    OnPropertyChanged(nameof(NewDate));
                }
            }
        }

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

        private bool dateFree;
        public bool DateFree
        {
            get { return dateFree; }
            set
            {
                if (dateFree != value)
                {
                    dateFree = value;
                    OnPropertyChanged();
                }
            }
        }

        public ReservationChangeRequestDto() { }
        public ReservationChangeRequestDto(ReservationChangeRequest reservationChangeRequest, ReservationDate reservationDate,
            Accommodation accommodation)
        {
            this.accommodationReservationId = reservationChangeRequest.AccommodationReservationId;
            this.firstDay = reservationChangeRequest.FirstDay;
            this.lastDay = reservationChangeRequest.LastDay;
            this.status = reservationChangeRequest.Status;
            this.newDate = reservationDate.Date;
            this.comment = reservationChangeRequest.Comment;
            this.accommodationName = accommodation.Name;
        }
        public ReservationChangeRequestDto(int id, int reservationId, DateTime firstDay, DateTime lastDay, Status status, string comment,
            string imagePath, bool dateFree)
        {
            Id = id;
            AccommodationReservationId = reservationId;
            FirstDay = firstDay;
            LastDay = lastDay;
            Status = status;
            Comment = comment;
            ImagePath = imagePath;
            DateFree = dateFree;
        }
        public ReservationChangeRequest ToReservationChangeRequest() {
            return new ReservationChangeRequest(Id, AccommodationReservationId, FirstDay, LastDay, Status, Comment);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
