using BookingApp.Observer;
using BookingApp.Services;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels
{
    public class OwnerViewModel : ViewModelBase, IObserver
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

        private OwnerService ownerService;

        public OwnerViewModel(int ownerId)
        {
            ownerService = new OwnerService(ownerId);
            ownerService.Subscribe(this);
            TotalRatings = 0;
            AverageRating = 0;
            Update();
        }

        public void Update()
        {
            ownerService.Update(ref totalRatings, ref averageRating);
            if (TotalRatings > 5 && AverageRating >= 4.5) IsSuper = true;
        }
    }
}
