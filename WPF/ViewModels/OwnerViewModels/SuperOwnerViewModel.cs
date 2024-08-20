using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class SuperOwnerViewModel : ViewModelBase
    {
        private int totalRatings;
        public int TotalRatings
        {
            get { return totalRatings; }
            set
            {
                if (totalRatings != value)
                {
                    totalRatings = value;
                    OnPropertyChanged();
                }
            }
        }

        private double averageRating;
        public double AverageRating
        {
            get { return averageRating; }
            set
            {
                if (averageRating != value)
                {
                    averageRating = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isSuper;
        public bool IsSuper
        {
            get { return isSuper; }
            set
            {
                if (isSuper != value)
                {
                    isSuper = value;
                    OnPropertyChanged();
                }
            }
        }

        public SuperOwnerViewModel()
        {

        }

        public SuperOwnerViewModel(int totalRatings, double averageRating, bool isSuper)
        {
            this.totalRatings = totalRatings;
            this.averageRating = averageRating;
            this.isSuper = isSuper;
        }
    }
}
