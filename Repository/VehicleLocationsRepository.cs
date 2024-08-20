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
    public class VehicleLocationsRepository : IVehicleLocationsRepository
    {
        private const string FilePath = "../../../Resources/Data/vehiclelocations.csv";

        private readonly Serializer<VehicleLocations> serializer;

        private List<VehicleLocations> vehicleLocations;

        public Subject VehicleLocationsSubject;

        public VehicleLocationsRepository()
        {
            serializer = new Serializer<VehicleLocations>();
            vehicleLocations = serializer.FromCSV(FilePath);
            VehicleLocationsSubject = new Subject();
        }

        public List<VehicleLocations> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public List<VehicleLocations> GetByVehicleId(int vehicleId)
        {
            List<VehicleLocations> foundLocations = new List<VehicleLocations>();

            foreach (VehicleLocations vehicleLocation in vehicleLocations)
            {
                if(vehicleLocation.VehicleId == vehicleId)
                {
                    foundLocations.Add(vehicleLocation);
                }
            }
            return foundLocations;
        }

        public List<int> GetLocationIdsByVehicleId(int vehicleId)
        {
            List<int> foundLocations = new List<int>();

            foreach (VehicleLocations vehicleLocation in vehicleLocations)
            {
                if (vehicleLocation.VehicleId == vehicleId)
                {
                    foundLocations.Add(vehicleLocation.LocationId);
                }
            }
            return foundLocations;
        }

        public List<VehicleLocations> GetByLocationId(int locationId)
        {
            List<VehicleLocations> foundLocations = new List<VehicleLocations>();

            foreach (VehicleLocations vehicleLocation in vehicleLocations)
            {
                if (vehicleLocation.LocationId == locationId)
                {
                    foundLocations.Add(vehicleLocation);
                }
            }
            return foundLocations;
        }

        public List<int> GetVehicleByLocation(int locationId)
        {
            List<int> foundLocations = new List<int>();

            foreach (VehicleLocations vehicleLocation in vehicleLocations)
            {
                if (vehicleLocation.LocationId == locationId)
                {
                    foundLocations.Add(vehicleLocation.VehicleId);
                }
            }
            return foundLocations;
        }

        public VehicleLocations GetByIds(int vehicleId, int locationId)
        {
            return vehicleLocations.Find(v => v.VehicleId == vehicleId && v.LocationId == locationId);
        }

        public VehicleLocations Add(int vehicleId, int locationId)
        {
            vehicleLocations = serializer.FromCSV(FilePath);
            VehicleLocations newVehicleLocation = new VehicleLocations(vehicleId, locationId);

            if(vehicleLocations.Any(v => v.VehicleId == vehicleId && v.LocationId == locationId))
            {
                return null;
            }

            vehicleLocations.Add(newVehicleLocation);
            serializer.ToCSV(FilePath, vehicleLocations);
            return newVehicleLocation;
        }

        public VehicleLocations Delete(int vehicleId, int locationId)
        {
            vehicleLocations = serializer.FromCSV(FilePath);
            VehicleLocations foundVehicleLocation = GetByIds(vehicleId, locationId);

            if(foundVehicleLocation == null)
            {
                return null;
            }

            vehicleLocations.Remove(foundVehicleLocation);
            serializer.ToCSV(FilePath, vehicleLocations);
            return foundVehicleLocation;
        }

        public void DeleteByVehicleId(int vehicleId)
        {
            foreach (VehicleLocations vehicleLocation in GetByVehicleId(vehicleId))
            {
                Delete(vehicleLocation.VehicleId, vehicleLocation.LocationId);
            }
        }

        public void Subscribe(IObserver observer)
        {
            VehicleLocationsSubject.Subscribe(observer);
        }
    }
}
