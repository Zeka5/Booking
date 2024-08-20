using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVehicleLocationsRepository
    {
        List<VehicleLocations> GetAll();
        List<VehicleLocations> GetByVehicleId(int vehicleId);
        List<VehicleLocations> GetByLocationId(int locationId);
        List<int> GetLocationIdsByVehicleId(int vehicleId);
        VehicleLocations GetByIds(int vehicleId, int locationId);
        VehicleLocations Add(int vehicleId, int locationId);
        VehicleLocations Delete(int vehicleId, int locationId);
        void DeleteByVehicleId(int vehicleId);
        void Subscribe(IObserver observer);
        List<int> GetVehicleByLocation(int locationId);
    }
}
