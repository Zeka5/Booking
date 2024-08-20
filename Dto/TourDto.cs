using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.Dto
{
    public class TourDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

        public int LocationId {  get; set; }

        public int LanguageId {  get; set; }

        public int UserId { get; set; }
        
        public ObservableCollection<TourRealizationDto> TourRealizations { get; set; }=new ObservableCollection<TourRealizationDto>();
        private TourRealizationDto tourStart;
        public TourRealizationDto TourStart
        {
            get => tourStart;
            set
            {
                if (tourStart != value)
                {
                    tourStart = value;
                    OnPropertyChanged(nameof(TourStart));
                }
            }
        }


        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }

            }
        }

        public Location Location { get; set; }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }

            }
        }

        public Language Language { get; set; }

        private int capacity;
        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                if (value != capacity)
                {
                    capacity = value;
                    OnPropertyChanged("Capacity");
                }

            }
        }

        private double duration;
        public double Duration
        {
            get
            {
                return duration;
            }
            set
            {
                if (value != duration)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                }

            }
        }

        private bool canCancel;
        public bool CanCancel
        {
            get { return canCancel; }
            set
            {
                if (canCancel != value)
                {
                    canCancel = value;
                    OnPropertyChanged("CanCancel");
                }
            }
        }

        private int expectedTouristNumber;
        public int ExpectedTouristNumber
        {
            get
            {
                return expectedTouristNumber;
            }
            set
            {
                if (value != expectedTouristNumber)
                {
                    expectedTouristNumber = value;
                    OnPropertyChanged("ExpectedTouristNumber");
                }

            }
        }

        private string imagePath;
        public string ImagePath 
        {
            get
            {
                return imagePath;
            }
            set 
            {
                if(value != imagePath) 
                { 
                    imagePath = value;
                    OnPropertyChanged("ImagePath");
                }
            }
        }

        public TourDto()
        {
        }
        public TourDto(Tour tour,Location location,Language language)
        {
            Id = tour.Id;
            Name = tour.Name;
            Location = location;
            LocationId = tour.LocationId;
            Description = tour.Description;
            Language = language;
            LanguageId= tour.LanguageId;
            Capacity = tour.Capacity;
            //AvaliablePlaces = tour.AvaliablePlaces;
            Duration = tour.Duration;
            UserId=tour.UserId;

        }
        public TourDto(Tour tour, Location location, Language language, string imagePath)
        {
            Id = tour.Id;
            Name = tour.Name;
            Location = location;
            LocationId = tour.LocationId;
            Description = tour.Description;
            Language = language;
            LanguageId = tour.LanguageId;
            Capacity = tour.Capacity;
            //AvaliablePlaces = tour.AvaliablePlaces;
            Duration = tour.Duration;
            ImagePath = imagePath;
        }
        public Tour ToTour()
        {
            return new Tour(Id,name,LocationId, description,LanguageId, capacity, duration,UserId);
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
