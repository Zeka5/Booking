using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class VehicleService
    {
        private IVehicleRepository vehicleRepository;
        private VehicleLanguageService vehicleLanguageService;
        private VehicleLocationService vehicleLocationService;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            this.vehicleRepository = vehicleRepository;
        }

        public VehicleService(IVehicleRepository vehicleRepository,
            VehicleLanguageService vehicleLanguageService,
            VehicleLocationService vehicleLocationService)
        {
            this.vehicleRepository = vehicleRepository;
            this.vehicleLanguageService = vehicleLanguageService;
            this.vehicleLocationService = vehicleLocationService;
        }

        public List<Vehicle> GetCompatibleVehicles(GroupDriveReservation reservation)
        {
            List<int> languageVehicleIds = vehicleLanguageService.GetVehicleIdsByLangaugeName(reservation.Language);
            List<int> locationVehicleIds = vehicleLocationService.GetVehicleIdsByAddressId(reservation.StartAddressId);

            return languageVehicleIds.Intersect(locationVehicleIds)
                                     .Select(vehicleId => GetById(vehicleId))
                                     .ToList();
        }

        public int GetNextId() { return this.vehicleRepository.GetNextId(); }

        public void Add(Vehicle vehicle)
        {
            this.vehicleRepository.Add(vehicle);
        }

        public bool IsRegistered(int driverId)
        {
            if (vehicleRepository.GetByDriverId(driverId) == null) { return false; }
            return true;
        }

        public string GetNameByDriverId(int driverId)
        {
            return vehicleRepository.GetByDriverId(driverId).Name.ToString();
        }

        public void Delete(int driverId)
        {
            vehicleRepository.Delete(vehicleRepository.GetByDriverId(driverId));
        }

        public Vehicle GetByDriverId(int driverId)
        {
            return vehicleRepository.GetByDriverId(driverId);
        }

        public Vehicle GetById(int id)
        {
            return vehicleRepository.GetById(id);
        }

        public void Subscribe(IObserver observer)
        {
            vehicleRepository.Subscribe(observer);
        }
    }
}
