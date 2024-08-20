using BookingApp.Domain.Model;
using BookingApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class TourGuestDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public int TourReservationId { get; set; }

        private string fullName;
        public string FullName
        {
            get
            {
                return fullName;
            }
            set
            {
                if (value != fullName)
                {
                    fullName = value;
                    OnPropertyChanged("FullName");
                }

            }
        }

        private int years;
        public int Years
        {
            get
            {
                return years;
            }
            set
            {
                if (value != years)
                {
                    years = value;
                    OnPropertyChanged("Years");
                }

            }
        }

        private string checkPointName;
        public string CheckPointName
        {
            get
            {
                return checkPointName;
            }
            set
            {
                if (value != checkPointName)
                {
                    checkPointName = value;
                    OnPropertyChanged("CheckPointName");
                }

            }
        }
        public TourGuestDto()
        {
            
        }
        public TourGuestDto(TourGuest tourGuest)
        {
            Id = tourGuest.Id;
            FullName = tourGuest.FullName;
            Years = tourGuest.Years;
            TourReservationId = tourGuest.TourReservationId;
            CheckPointName = tourGuest.CheckPointName;   
        }
        
        public TourGuest toTourGuest()
        {
            return new TourGuest(Id,fullName,Years, TourReservationId,CheckPointName);
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
