using BookingApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BookingApp.Repository;
using System.Collections.ObjectModel;
using BookingApp.Domain.Model;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for SelectDriver.xaml
    /// </summary>
    public partial class SelectDriver : Window
    {
        public ObservableCollection<DriverDto> DriverList { get; set; }
        public DriveReservationRepository DriveReservationRepository { get; set; } 
        public DriverRepository DriverRepository { get; set; }
        public VehicleRepository VehicleRepository { get; set; }
        public VehicleLocationsRepository VehicleLocationsRepository { get; set; }
        public AddressRepository AdressRepository { get; set; }
        public DriverDto? SelectedDriver { get; set; }
        public int StartAddressId { get; set; }
        public int FinalAddressId { get; set; }
        public DateTime ReservationTime { get; set; }
        public SelectDriver(int startAddressId, int finalAddressId, string reservationTime)
        {
            InitializeComponent();
            DataContext = this;

            DriverList = new ObservableCollection<DriverDto>();

            DriveReservationRepository = new DriveReservationRepository();
            DriverRepository = new DriverRepository();
            VehicleRepository = new VehicleRepository();
            AdressRepository = new AddressRepository();
            VehicleLocationsRepository = new VehicleLocationsRepository();

            StartAddressId = startAddressId;
            FinalAddressId = finalAddressId;
            ReservationTime = DateTime.Parse(reservationTime);

            LoadDrivers();
        }
        public void LoadDrivers()
        {
            DriverList.Clear();

            foreach (Driver driver in DriverRepository.GetAll())
            {
                if (IsDriverAvailableForReservation(driver))
                {
                    //DriverList.Add(new DriverDto(driver, VehicleRepository.GetById(driver.Vehicle.Id).Name));
                }
            }
            if (DriverList.Count == 0)
            {
                MessageBox.Show("There is no drivers available for the given input!");
            }
        }
        public bool IsDriverAvailableForReservation(Driver driver)
        {
            /*if (!IsVehicleRegisteredAtLocation(driver.Vehicle.Id))
            {
                return false;
            }*/

            foreach (DriveReservation driveReservation in DriveReservationRepository.GetAll())
            {
                if (driveReservation.DriverId == driver.Id && !IsForwardedTermAvailable(driveReservation.DepartureTime))
                {
                    return false;
                }
            }

            return true;
        }
        private bool IsForwardedTermAvailable(DateTime departureTime)
        {
            return !(departureTime.AddMinutes(15) >= ReservationTime 
                   && departureTime.AddMinutes(-15) <= ReservationTime);
        }

        private bool IsVehicleRegisteredAtLocation(int vehicleId)
        {
            int startLocationId = AdressRepository.GetById(StartAddressId).Id;

            return VehicleLocationsRepository
                .GetByVehicleId(vehicleId)
                .Any(vl => vl.LocationId == startLocationId);
        }
        private void SelectDriverClick(object sender, RoutedEventArgs e)
        {
            DriveReservation driveReservation = new DriveReservation(SignInForm.curretnUserId, StartAddressId, FinalAddressId, SelectedDriver.Id, ReservationTime.ToString());
            DriveReservationRepository.Add(driveReservation);
            Close();
        }
    }
}
