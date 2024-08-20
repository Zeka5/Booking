using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.View;
using BookingApp.WPF.View.DriverView;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using Xceed.Wpf.Toolkit.Core.Converters;

namespace BookingApp.WPF.ViewModels
{
    public class DriverProfilePageViewModel : IObserver, INotifyPropertyChanged
    {
        private List<string> vehicleLocations;
        public List<string> VehicleLocations
        {
            get { return vehicleLocations; }
            set
            {
                if (vehicleLocations != value)
                {
                    vehicleLocations = value;
                    OnPropertyChanged();
                }
            }
        }
        private List<string> vehicleLanguages;
        public List<string> VehicleLanguages
        {
            get { return vehicleLanguages; }
            set
            {
                if (vehicleLanguages != value)
                {
                    vehicleLanguages = value;
                    OnPropertyChanged();
                }
            }
        }

        private string vehicleImage;
        public string VehicleImage
        {
            get { return vehicleImage; }
            set
            {
                if (vehicleImage != value)
                {
                    vehicleImage = value;
                    OnPropertyChanged();
                }
            }
        }

        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                if (fullName != value)
                {
                    fullName = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility registrationVisibility;
        public Visibility RegistrationVisibility
        {
            get { return registrationVisibility; }
            set
            {
                if (registrationVisibility != value)
                {
                    registrationVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility vehicleVisibility;
        public Visibility VehicleVisibility
        {
            get { return vehicleVisibility; }
            set
            {
                if (vehicleVisibility != value)
                {
                    vehicleVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility superDriverVisibility;
        public Visibility SuperDriverVisibility
        {
            get { return superDriverVisibility; }
            set
            {
                if (superDriverVisibility != value)
                {
                    superDriverVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        private int superDriverMonths;
        public int SuperDriverMonths
        {
            get { return superDriverMonths; }
            set
            {
                if (superDriverMonths != value)
                {
                    superDriverMonths = value;
                    OnPropertyChanged();
                }
            }
        }

        private string nextDriveETA;
        public string NextDriveETA
        {
            get { return nextDriveETA; }
            set
            {
                if (nextDriveETA != value)
                {
                    nextDriveETA = value;
                    OnPropertyChanged();
                }
            }
        }
        private DriverService driverService;
        private VehicleService vehicleService;
        private DriveReservationService driveReservationService;
        private VehicleLocationService vehicleLocationService;
        private VehicleLanguageService vehicleLanguageService;
        private LocationService locationService;
        private LanguageService languageService;
        private ImageService imageService;

        public DriverDto Driver { get; set; }
        public VehicleDto Vehicle { get; set; }
        public NavigationService NavigationService { get; set; }
        public MyICommand BackCommand { get; set; }
        public MyICommand RegisterVehicleCommand { get; set; }
        public MyICommand StatisticsCommand { get; set; }
        public MyICommand DismissVehicleCommand { get; set; }
        public MyICommand BreakRequestCommand { get; set; }
        public MyICommand SignOutCommand { get; set; }
        public MyICommand HelpCommand { get; set; }

        private readonly int id;
        private DriveReservation driveReservation;
        private DispatcherTimer timer;
        private int seconds=0,minutes=0,hours=0;

        public DriverProfilePageViewModel(int id, NavigationService navigationService)
        {
            driverService = new DriverService(Injector.CreateInstance<IDriverRepository>());
            vehicleService = new VehicleService(Injector.CreateInstance<IVehicleRepository>());
            vehicleLocationService = new VehicleLocationService(Injector.CreateInstance<IVehicleLocationsRepository>(),
                new AddressService(Injector.CreateInstance<IAddressRepository>()), vehicleService);
            driveReservationService = new DriveReservationService(Injector.CreateInstance<IDriveReservationRepository>(),
                vehicleLocationService, new UserService(Injector.CreateInstance<IUserRepository>()));
            vehicleLanguageService = new VehicleLanguageService(Injector.CreateInstance<IVehicleLanguageRepository>());
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            NavigationService = navigationService;
            BackCommand = new MyICommand(Execute_GoBack);
            RegisterVehicleCommand = new MyICommand(Execute_RegisterVehicle);
            StatisticsCommand = new MyICommand(Execute_Statistics);
            DismissVehicleCommand = new MyICommand(Execute_DismissVehicle);
            BreakRequestCommand = new MyICommand(Execute_BreakRequest);
            SignOutCommand = new MyICommand(Execute_SignOut);
            HelpCommand = new MyICommand(Execute_Help);

            Vehicle = new VehicleDto();
            VehicleLanguages = new List<string>();
            VehicleLocations = new List<string>();

            this.id = id;
            timer = new DispatcherTimer();
            RegistrationVisibility = Visibility.Visible;
            VehicleVisibility = Visibility.Collapsed;
            SuperDriverVisibility = Visibility.Collapsed;

            Driver = new DriverDto(driverService.GetByUserId(id));
            FullName = Driver.Name + " " + Driver.LastName;
            SuperDriverMonths = 0;
            NextDriveETA = "00:00:00";

            vehicleService.Subscribe(this);
            imageService.Subscribe(this);
            Messenger.Default.Register<NotificationMessage>(this, NotifyMe);
            Update();
        }

        private void Execute_Help()
        {
            NavigationService.Navigate(new DriverHelpPage("ProfilePage", id, NavigationService));
        }

        private void Execute_SignOut()
        {
            return;
        }

        private void Execute_BreakRequest()
        {
            NavigationService.Navigate(new BreakRequest(id, NavigationService));
        }

        private void Execute_DismissVehicle()
        {
            DismissVehicle();
        }

        private void Execute_Statistics()
        {
            NavigationService.Navigate(new DriverStatistics(id, NavigationService));
        }

        private void Execute_RegisterVehicle()
        {
            NavigationService.Navigate(new View.DriverView.VehicleRegistration(id, NavigationService));
        }

        private void Execute_GoBack()
        {
            NavigationService.Navigate(new DriverHomePage(id, NavigationService));
        }

        public void Update()
        {
            if (!vehicleService.IsRegistered(id))
            {
                RegistrationVisibility = Visibility.Visible;
                VehicleVisibility = Visibility.Collapsed;
            }
            else { UpdateVehicle(); }
            if (Driver.IsSuperDriver)
            {
                var membershipDate = DateTime.ParseExact(Driver.MembershipDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                SuperDriverMonths = (365-(DateTime.Now - membershipDate).Days)/30;
                SuperDriverVisibility = Visibility.Visible;
            }

        }

        public void NotifyMe(NotificationMessage message)
        {
            UpdateVehicle();
        }

        private void UpdateVehicle()
        {
            Vehicle = new VehicleDto(vehicleService.GetByDriverId(id));
            UpdateLocationAndLanguage(Vehicle);

            RegistrationVisibility = Visibility.Collapsed;
            VehicleVisibility = Visibility.Visible;
            if (driveReservationService.GetOrderedReservationsById(id).Count == 0)
            {
                return;
            }
            driveReservation = driveReservationService.GetOrderedReservationsById(id)[0];
            CountdownNextDrive();
        }

        private void CountdownNextDrive()
        {
            TimeSpan nextDriveCountdown = driveReservation.DepartureTime - DateTime.Now;
            int totalSeconds = Convert.ToInt32(nextDriveCountdown.TotalSeconds);
            hours = totalSeconds / 3600;
            int remainingSeconds = totalSeconds % 3600;
            minutes = remainingSeconds / 60;
            seconds = remainingSeconds % 60;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            seconds--;
            if (seconds == 0) { minutes--; seconds = 60; }
            if (minutes == 0) { hours--; minutes = 60; }
            NextDriveETA = $"{hours:00}:{minutes:00}:{seconds:00}";
            if (hours == 0 && minutes == 0 && seconds == 0) { timer.Stop(); }
        }

        private void UpdateLocationAndLanguage(VehicleDto Vehicle)
        {
            foreach (int locationId in vehicleLocationService.GetLocationsByVehicleId(Vehicle.Id))
            {
                string location = locationService.GetById(locationId).City + ", " + locationService.GetById(locationId).Country;
                VehicleLocations.Add(location);
            }
            foreach (int languageId in vehicleLanguageService.GetLanguagesByVehicleId(Vehicle.Id))
            {
                VehicleLanguages.Add(languageService.GetById(languageId).Name);
            }
            VehicleImage = imageService.GetFirstByEntityAndType(Vehicle.Id, ResourceType.Vehicle).Path;
        }

        public void DismissVehicle()
        {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to DISMISS registered vehicle?", "Confirmation",
                                                            MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if (dialogResult == MessageBoxResult.Cancel)
            {
                return;
            }

            RegistrationVisibility = Visibility.Visible;
            VehicleVisibility = Visibility.Collapsed;
            vehicleService.Delete(id);
        }

        public void RegisterVehicle()
        {
            /*VehicleRegistration vehicleRegistration = new View.DriverView.VehicleRegistration(id);
            vehicleRegistration.Show();*/
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
