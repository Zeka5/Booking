using BookingApp.Domain.Model;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BookingApp.Dto
{
    public class AccommodationDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private const string pattern = @"^[A-Z].*$";
        private string name;
        public string Name 
        { 
            get 
            {
                return name; 
            }
            set 
            { 
                if(value!=name)
                {
                    name = value;
                    OnPropertyChanged();
                }
                     
            } 
        }

        private LocationDto locationDto;
        public LocationDto LocationDto
        {
            get { return locationDto; }
            set
            {
                if (locationDto != value)
                {
                    locationDto = value;
                    OnPropertyChanged();
                }
            }
        }

        private int locationId;
        public int LocationId
        {
            get { return locationId; }
            set {
                if (value != locationId) {
                    locationId = value;
                    OnPropertyChanged();
                }
            }

        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (value != imagePath)
                {
                    imagePath = value;
                    OnPropertyChanged();
                }
            }

        }

        public Location Location { get; set; }

        private Domain.Model.Type type;
        public Domain.Model.Type Type
        {
            get { return type; }
            set
            {
                if (value != type)
                {
                    type = value;
                    OnPropertyChanged();
                }
            }

        }

        private int maxGuest;
        public int MaxGuest
        {
            get { return maxGuest; }
            set
            {
                if (value != maxGuest)
                {
                    maxGuest = value;
                    OnPropertyChanged();
                }
            }

        }

        private int minStayDays;
        public int MinStayDays
        {
            get { return minStayDays; }
            set
            {
                if (value != minStayDays)
                {
                    minStayDays = value;
                    OnPropertyChanged();
                }
            }

        }

        private int cancellationDays;
        public int CancellationDays
        {
            get { return cancellationDays; }
            set
            {
                if (value != cancellationDays)
                {
                    cancellationDays = value;
                    OnPropertyChanged();
                }
            }
        }
        public int OwnerId { get; set; }

        public LocationRepository LocationRepository { get; set; }
        public AccommodationDto() { }

        public AccommodationDto(Accommodation accommodation, Location location , Domain.Model.Image image)
        {
            Id = accommodation.Id;
            name = accommodation.Name;
            locationId = accommodation.LocationId;
            Location = location;
            type = accommodation.Type;
            maxGuest = accommodation.MaxGuest;
            minStayDays = accommodation.MinStayDays;
            cancellationDays = accommodation.CancellationDays;
            ImagePath = image == null ? " " : image.Path;
        }
        public Accommodation ToAccommodation() {
            return new Accommodation(name, locationId, type, maxGuest, minStayDays, cancellationDays, OwnerId);
        }

        public Accommodation ToModel()
        {
            return new Accommodation(name, LocationDto.Id, type, maxGuest, minStayDays, cancellationDays, OwnerId);
        }

        public bool ValidateValues()
        {
            Regex regex = new Regex(pattern);
            if (name == null || !regex.IsMatch(name)) return false;
            if (maxGuest < 1 || maxGuest > 5) return false;
            if (minStayDays < 1) return false;
            if (cancellationDays < 1) return false;
            if (Type == Domain.Model.Type.Any) return false;
            if (LocationDto is null) return false;
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
