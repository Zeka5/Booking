using BookingApp.Dto;
using BookingApp.Domain.Model;
using BookingApp.Repository;
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

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for ReserveDrive.xaml
    /// </summary>
    public partial class ReserveDrive : Window
    {
        public List<LocationDto> Locations { get; set; }
        public AddressDto StartAddress { get; set; }
        public AddressDto EndAddress { get; set; }
        public LocationDto SelectedStartLocation { get; set; }
        public LocationDto SelectedEndLocation { get; set; }
        private LocationRepository LocationRepository { get; set; }
        private AddressRepository AddressRepository { get; set; }
        private DriveReservationRepository DriveReservationRepository { get; set; }
        private DriverRepository DriverRepository { get; set; }

        public ReserveDrive()
        {
            InitializeComponent();
            DataContext = this;

            Locations = new List<LocationDto>();
            LocationRepository = new LocationRepository();
            AddressRepository = new AddressRepository();
            DriveReservationRepository = new DriveReservationRepository();
            DriverRepository = new DriverRepository();

            SelectedEndLocation = new LocationDto();
            SelectedStartLocation = new LocationDto();

            StartAddress = new AddressDto();
            EndAddress = new AddressDto();

            LoadLocations();
        }
        private void LoadLocations()
        {
            this.Locations.Clear();

            foreach (var location in LocationRepository.GetAll())
            {
                this.Locations.Add(new LocationDto(location));
            }
        }
        private void LoadAdresses()
        {
            int selectedInitialLocationId = SelectedStartLocation.ToLocation().Id;
            int selectedEndLocationId = SelectedEndLocation.ToLocation().Id;

            StartAddress.LocationId = selectedInitialLocationId;
            EndAddress.LocationId = selectedEndLocationId;

            AddressRepository.Add(StartAddress.ToAdress());
            AddressRepository.Add(EndAddress.ToAdress());
        }
        private void DriveReservationClick(object sender, RoutedEventArgs e)
        {
            LoadAdresses();

            int startAddressId = AddressRepository.GetBeforeLast().AdressId;
            int endAddressId = AddressRepository.GetLast().AdressId;

            SelectDriver selectDriver = new SelectDriver(startAddressId, endAddressId, DriveStartTime.Text);

            if (TouristHomePage.IsFastDrive)
            {
                FastDriveReservation(startAddressId, endAddressId, selectDriver);
                return;
            }
            selectDriver.Show();
            Close();
        }

        private void FastDriveReservation(int startAddressId, int endAddressId, SelectDriver selectDriver)
        {
            foreach (Driver driver in DriverRepository.GetAll())
            {
                if (selectDriver.IsDriverAvailableForReservation(driver))
                {
                    DriveReservation driveReservation = new DriveReservation(SignInForm.curretnUserId, startAddressId, endAddressId, driver.Id, DriveStartTime.Text, true);
                    DriveReservationRepository.Add(driveReservation);
                    Close();
                    return;
                }
            }
        }
    }
}
