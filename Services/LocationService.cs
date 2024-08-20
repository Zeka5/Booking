using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class LocationService
    {
        private ILocationRepository locationRepository;

        public LocationService(ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
        }

        public List<Location> GetListOfAll()
        {
            return locationRepository.GetAll();
        }

        public Location GetById(int id)
        {
            return locationRepository.GetById(id);
        }
        public Location GetByName(string country, string city)
        {
            return locationRepository.GetByName(country, city);
        }

        public IEnumerable<Location> GetAll()
        {
            return locationRepository.GetAll();
        }

        public List<Location> LoadCities(string selectedCountry)
        {
            List<Location> cities = new List<Location>();

            foreach (var location in locationRepository.GetAll())
            {
                if (location.Country.Equals(selectedCountry))
                    cities.Add(location);
            }

            return cities;
        }
    }
}
