using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class TourRequestTourGuestDto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int TourRequestId { get; set; }

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

        private int age;
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value != age)
                {
                    age = value;
                    OnPropertyChanged("Years");
                }

            }
        }
        public TourRequestTourGuestDto()
        {

        }
        public TourRequestTourGuestDto(TourRequestTourGuest tourRequestTourGuest)
        {
            Id = tourRequestTourGuest.Id;
            FullName = tourRequestTourGuest.FullName;
            Age = tourRequestTourGuest.Age;
            TourRequestId = tourRequestTourGuest.TourRequestId;
        }

        public TourRequestTourGuest toTourRequestTourGuest()
        {
            return new TourRequestTourGuest(Id, fullName, Age, TourRequestId);
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
