using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class VehicleRegistrationService
    {
        private IVehicleRepository vehicleRepository;
        private IVehicleLanguageRepository vehicleLanguageRepository;
        private IVehicleLocationsRepository vehicleLocationsRepository;
        private IImageRepository imageRepository;
        private ILanguageRepository languageRepository;
        private ILocationRepository locationRepository;

        private readonly int id;

        public VehicleRegistrationService(int id)
        {
            vehicleRepository = Injector.CreateInstance<IVehicleRepository>();
            vehicleLanguageRepository = Injector.CreateInstance<IVehicleLanguageRepository>();
            vehicleLocationsRepository = Injector.CreateInstance<IVehicleLocationsRepository>();
            imageRepository = Injector.CreateInstance<IImageRepository>();
            languageRepository = Injector.CreateInstance<ILanguageRepository>();
            locationRepository = Injector.CreateInstance<ILocationRepository>();

            this.id = id;
        }
    }
}
