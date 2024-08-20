using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class RenovationViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public string ImagePath { get; set; }
        public string AccommodationName { get; set; }

        private DateTime rangeStart;
        public DateTime RangeStart
        {
            get => rangeStart;
            set
            {
                if (rangeStart != value)
                {
                    rangeStart = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime rangeEnd;
        public DateTime RangeEnd
        {
            get => rangeEnd;
            set
            {
                if (rangeEnd != value)
                {
                    rangeEnd = value;
                    OnPropertyChanged();
                }
            }
        }

        private int daysToRenovate;
        public int DaysToRenovate
        {
            get => daysToRenovate;
            set
            {
                if (daysToRenovate != value)
                {
                    daysToRenovate = value;
                    OnPropertyChanged();
                }
            }
        }

        public RenovationViewModel()
        {

        }

        public RenovationViewModel(int id, int accommodationId, string imagePath, string accommodationName, DateTime rangeStart,
            DateTime rangeEnd)
        {
            Id = id;
            AccommodationId = accommodationId;
            ImagePath = imagePath;
            AccommodationName = accommodationName;
            RangeStart = rangeStart;
            RangeEnd = rangeEnd;
        }

        public Renovation ToRenovation()
        {
            return new Renovation(Id, AccommodationId, RangeStart, RangeEnd);
        }
    }
}
