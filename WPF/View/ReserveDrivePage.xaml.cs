using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for ReserveDrivePage.xaml
    /// </summary>
    public partial class ReserveDrivePage : Page, INotifyPropertyChanged
    {
        public List<LocationDto> Locations { get; set; }
        public List<LanguageDto> Languages { get; set; }
        public AddressDto StartAddress { get; set; }
        public AddressDto EndAddress { get; set; }
        public LocationDto SelectedStartLocation { get; set; }
        public LocationDto SelectedEndLocation { get; set; }
        public LanguageDto SelectedLanguage { get; set; }
        public LocationRepository LocationRepository { get; set; }
        public AddressRepository AddressRepository { get; set; }
        public LanguageRepository LanguageRepository { get; set; }
        public DriveReservationRepository DriveReservationRepository { get; set; }
        public GroupDriveReservationRepository GroupDriveReservationRepository { get; set; }
        public DriverRepository DriverRepository { get; set; }
        public bool IsFastDrive { get; set; }

        public ReserveDrivePage()
        {
            InitializeComponent();
            DataContext = this;

            Locations = new List<LocationDto>();
            Languages = new List<LanguageDto>();
            LocationRepository = new LocationRepository();
            AddressRepository = new AddressRepository();
            DriveReservationRepository = new DriveReservationRepository();
            DriverRepository = new DriverRepository();
            LanguageRepository = new LanguageRepository();
            GroupDriveReservationRepository = new GroupDriveReservationRepository();

            SelectedEndLocation = new LocationDto();
            SelectedStartLocation = new LocationDto();
            SelectedLanguage = new LanguageDto();

            StartAddress = new AddressDto();
            EndAddress = new AddressDto();

            NumberOfGuests = 1;
            IsFastDrive = false;

            LoadLanguages();
            LoadLocations();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public int numberOfGuests;
        public int NumberOfGuests
        {
            get { return numberOfGuests; }
            set
            {
                if (value != numberOfGuests)
                {
                    numberOfGuests = value;
                    OnPropertyChanged("NumberofTourGuests");
                }
            }
        }
        public void LoadLanguages()
        {
            Languages.Clear();

            foreach (var language in LanguageRepository.GetAll())
            {
                this.Languages.Add(new LanguageDto(language));
            }
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

            if (IsFastDrive)
            {
                FastDriveReservation(startAddressId, endAddressId, selectDriver);
                return;
            }
            selectDriver.Show();
        }
        private void FastDriveReservation(int startAddressId, int endAddressId, SelectDriver selectDriver)
        {
            foreach (Driver driver in DriverRepository.GetAll())
            {
                if (selectDriver.IsDriverAvailableForReservation(driver))
                {
                    DriveReservation driveReservation = new DriveReservation(SignInForm.curretnUserId, startAddressId, endAddressId, driver.Id, DriveStartTime.Text, true);
                    DriveReservationRepository.Add(driveReservation);
                    return;
                }
            }
        }
        private void GroupDriveReservationClick(object sender, RoutedEventArgs e)
        {
            LoadAdresses();

            int startAddressId = AddressRepository.GetBeforeLast().AdressId;
            int endAddressId = AddressRepository.GetLast().AdressId;

            GroupDriveReservation groupDriveReservation = new GroupDriveReservation(SignInForm.curretnUserId, startAddressId, endAddressId, DriveStartTimes.Text, NumberOfGuests, SelectedLanguage.Name);
            GroupDriveReservationRepository.Add(groupDriveReservation);
            MessageBox.Show("Successfully reserved!");
            NavigationService.Navigate(new Tours());
        }
        public void DecreaseNumberOfTourGuestsCLick(object sender, RoutedEventArgs e)
        {
            if (NumberOfGuests == 1)
            {
                return;
            }
            NumberOfGuests--;
        }
        public void IncreaseNumberOfTourGuestsCLick(object sender, RoutedEventArgs e)
        {
            if (NumberOfGuests == 10)
            {
                return;
            }
            NumberOfGuests++;
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
