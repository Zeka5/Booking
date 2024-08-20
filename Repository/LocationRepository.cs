using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private const string filePath = "../../../Resources/Data/locations.csv";
        private readonly Serializer<Location> serializer;
        private List<Location> locations;

        public LocationRepository()
        {
            this.serializer = new Serializer<Location>();
            this.locations = this.serializer.FromCSV(filePath);
        }

        public List<Location> GetAll()
        {
            return this.locations;
        }

        public Location? GetById(int id) {
            return locations.Find(location => location.Id == id);
        }
        public Location? GetByName(string coutrny, string city)
        {
            return locations.Find(location => location.Country == coutrny && location.City == city);
        }
    }
}
