using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class TourStatsDto:INotifyPropertyChanged
    {
        public TourDto Tour { get; set; }

        private int group1;
        public int Group1
        {
            get
            {
                return group1;
            }
            set
            {
                if (value != group1)
                {
                    group1 = value;
                    OnPropertyChanged("Group1");
                }

            }
        }

        private int group2;
        public int Group2
        {
            get
            {
                return group2;
            }
            set
            {
                if (value != group2)
                {
                    group2 = value;
                    OnPropertyChanged("Group2");
                }

            }
        }

        private int group3;
        public int Group3
        {
            get
            {
                return group3;
            }
            set
            {
                if (value != group3)
                {
                    group3 = value;
                    OnPropertyChanged("Group3");
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
                if (value != guestNumber)
                {
                    guestNumber = value;
                    OnPropertyChanged("GuestNumber");
                }

            }
        }

        public TourStatsDto()
        {
        }
        public TourStatsDto(TourDto tour,int group1,int group2,int group3)
        {
            Tour = tour;
            Group1 = group1;
            Group2 = group2;
            Group3 = group3;
            GuestNumber=group1+ group2 + group3;
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
