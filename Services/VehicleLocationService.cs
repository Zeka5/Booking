using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class VehicleLocationService
    {
        private IVehicleLocationsRepository vehicleLocationsRepository;
        private AddressService addressService;
        private VehicleService vehicleService;

        public VehicleLocationService(IVehicleLocationsRepository vehicleLocationsRepository, AddressService addressService, VehicleService vehicleService)
        {
            this.vehicleLocationsRepository = vehicleLocationsRepository;
            this.addressService = addressService;
            this.vehicleService = vehicleService;
        }

        public VehicleLocationService(IVehicleLocationsRepository vehicleLocationsRepository)
        {
            this.vehicleLocationsRepository = vehicleLocationsRepository;
        }

        public List<int> GetVehicleIdsByAddressId(int addressId)
        {
            int locationId = addressService.GetLocationId(addressId);
            List<int> vehicleIds = GetVehicleByLocation(locationId);
            return vehicleIds;
        }

        public List<int> GetVehicleByLocation(int locationId)
        {
            return vehicleLocationsRepository.GetVehicleByLocation(locationId);
        }

        public void Add(int vehicleId, int locationId)
        {
            vehicleLocationsRepository.Add(vehicleId, locationId);
        }

        public List<int> GetLocationsByVehicleId(int vehicleId)
        {
            return vehicleLocationsRepository.GetLocationIdsByVehicleId(vehicleId);
        }

        public bool IsRegisteredForLocation(DriveReservation driveReservation, int userId)
        {
            int locationId = addressService.GetLocationByReservation(driveReservation);
            return vehicleLocationsRepository.GetLocationIdsByVehicleId(vehicleService.GetByDriverId(userId).Id).Contains(locationId);
        }
    }
}
