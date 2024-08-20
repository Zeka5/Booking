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
    public class LocationDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string city;
        public string City
        {
            get { return city; }
            set
            {
                if (value != city)
                {
                    city = value;
                    OnPropertyChanged();
                }
            }
        }

        private string country;
        public string Country
        {
            get { return country; }
            set
            {
                if (value != country)
                {
                    country = value;
                    OnPropertyChanged();
                }
            }
        }

        public LocationDto() { }

        public LocationDto(Location location)
        {
            Id = location.Id;
            city = location.City;
            country = location.Country;
        }

        public Location ToLocation()
        {
            return new Location(Id, City, Country);
        }

        public bool IsNull()
        {
            if (city is null) return true;
            if (country is null) return true;
            return false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
