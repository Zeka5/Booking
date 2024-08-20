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
    public class TourRequestNotificationDto:INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int TourRequestId { get; set; }
        public Location Location { get; set; }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value != title)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }

            }
        }
        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (value != text)
                {
                    text = value;
                    OnPropertyChanged("Text");
                }

            }
        }

        private string creationDate;
        public string CreationDate
        {
            get
            {
                return creationDate;
            }
            set
            {
                if (value != creationDate)
                {
                    creationDate = value;
                    OnPropertyChanged("CreationDate");
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
                    OnPropertyChanged("IsRead");
                }
            }

        }

        public TourRequestNotificationDto()
        {
        }
        public TourRequestNotificationDto(TourRequestNotification tourRequestNotification)
        {
            Id = tourRequestNotification.Id;
            TourRequestId = tourRequestNotification.TourRequestId;
            Title = tourRequestNotification.Title;
            Text = tourRequestNotification.Text;
            CreationDate = tourRequestNotification.CreationDate.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            IsRead = tourRequestNotification.IsRead;
        }
        public TourRequestNotificationDto(TourRequestNotification tourRequestNotification, Location location)
        {
            Id = tourRequestNotification.Id;
            TourRequestId = tourRequestNotification.TourRequestId;
            Title = tourRequestNotification.Title;
            Text = tourRequestNotification.Text;
            CreationDate = tourRequestNotification.CreationDate.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            IsRead = tourRequestNotification.IsRead;
            Location = location;
        }
        public TourRequestNotification ToTourRequestNotification()
        {
            return new TourRequestNotification(Id,TourRequestId,title,text, DateTime.ParseExact(creationDate, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), isRead);
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
