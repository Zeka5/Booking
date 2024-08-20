using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace BookingApp.WPF.ViewModels.TourGuestViewModels
{
    public class MyDriveReservationsViewModel
    {
        public DriveReservationService driveReservationService;
        public VehicleLocationService vehicleLocationService;
        public LocationService locationService;
        public AddressService addressService;
        public LanguageService languageService;
        public DriverService driverService;
        public static ObservableCollection<DriveReservationDto> DriveReservationsList { get; set; }
        public DriveReservationDto SelectedDriveReservation { get; set; }
        public MyDriveReservationsViewModel()
        {
            driveReservationService = new DriveReservationService(Injector.CreateInstance<IDriveReservationRepository>(),
                        vehicleLocationService, new UserService(Injector.CreateInstance<IUserRepository>()));
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            addressService = new AddressService(Injector.CreateInstance<IAddressRepository>());
            driverService = new DriverService(Injector.CreateInstance<IDriverRepository>());

            DriveReservationsList = new ObservableCollection<DriveReservationDto>();
            SelectedDriveReservation = new DriveReservationDto();
            LoadDriveReservations();
        }
        private void LoadDriveReservations()
        {
            DriveReservationsList.Clear();

            foreach (var driveReservation in driveReservationService.GetAll())
            {
                DriveReservationsList.Add(new DriveReservationDto(driveReservation, addressService.GetById(driveReservation.StartAddressId).Street, addressService.GetById(driveReservation.EndAddressId).Street, driverService.GetByUserId(driveReservation.DriverId).Name));
            }
        }
        public void TourGuestIsLate()
        {
            SelectedDriveReservation.TourGuestDelay = "Late";

            driveReservationService.Update(SelectedDriveReservation.ToDriveReservation());

            LoadDriveReservations();
        }
        public void UnreliableDriver()
        {
            if(SelectedDriveReservation.DriverDelay == "On time" && SelectedDriveReservation.DepartureTime.AddMinutes(10) > DateTime.Now)
            {
                Driver driver = driverService.GetByUserId(SelectedDriveReservation.DriverId);
                driver.UnreliableCount++;

                driverService.Update(driver);

                MessageBox.Show("NEPOUZDAN!");
                return;
            }

            MessageBox.Show("Ne mozete ga ozanciti kao nepouzdanog");
        }
    }
}
