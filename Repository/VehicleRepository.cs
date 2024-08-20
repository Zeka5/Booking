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
    public class VehicleRepository : IVehicleRepository
    {
        private const string FilePath = "../../../Resources/Data/vehicles.csv";

        private readonly Serializer<Vehicle> serializer;

        private List<Vehicle> vehicles;

        public Subject VehicleSubject;
        private ImageRepository imageRepository;
        private VehicleLanguageRepository vehicleLanguageRepository;
        private VehicleLocationsRepository vehicleLocationsRepository;

        public VehicleRepository()
        {
            serializer = new Serializer<Vehicle>();
            vehicles = serializer.FromCSV(FilePath);
            VehicleSubject = new Subject();
            imageRepository = new ImageRepository();
            vehicleLanguageRepository = new VehicleLanguageRepository();
            vehicleLocationsRepository = new VehicleLocationsRepository();
        }

        public List<Vehicle> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public Vehicle GetById(int id)
        {
            return vehicles.Find(v => v.Id == id);
        }

        public Vehicle GetByDriverId(int driverId)
        {
            return vehicles.Find(v => v.DriverId == driverId);
        }

        public int GetNextId()
        {
            if(vehicles.Count < 1)
            {
                return 1;
            }

            return vehicles.Max(v => v.Id) + 1;
        }

        public Vehicle Add(Vehicle vehicle)
        {
            vehicles = serializer.FromCSV(FilePath);
            vehicle.Id = GetNextId();
            vehicles.Add(vehicle);
            serializer.ToCSV(FilePath, vehicles);
            VehicleSubject.NotifyObservers();
            return vehicle;
        }

        public void Delete(Vehicle vehicle)
        {
            vehicles = serializer.FromCSV(FilePath);
            Vehicle foundVehicle = vehicles.Find(v => v.Id == vehicle.Id);
            vehicles.Remove(foundVehicle);
            serializer.ToCSV(FilePath, vehicles);

            DeleteRemainingData(vehicle.Id);

            VehicleSubject.NotifyObservers();
        }

        private void DeleteRemainingData(int vehicleId)
        {
            imageRepository.DeleteByVehicleId(vehicleId);
            vehicleLocationsRepository.DeleteByVehicleId(vehicleId);
            vehicleLanguageRepository.DeleteByVehicleId(vehicleId);
        }

        public void Subscribe(IObserver observer)
        {
            VehicleSubject.Subscribe(observer);
        }
    }
}
