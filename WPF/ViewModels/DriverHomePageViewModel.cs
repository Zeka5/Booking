using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.View;
using BookingApp.View.Guide;
using BookingApp.WPF.View;
using BookingApp.WPF.View.DriverView;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using static QuestPDF.Helpers.Colors;

namespace BookingApp.WPF.ViewModels
{
    public class DriverHomePageViewModel : IObserver, INotifyPropertyChanged
    {
        private string vehicleName;
        public string VehicleName
        {
            get { return vehicleName; }
            set
            {
                if (vehicleName != value)
                {
                    vehicleName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string driveTime;
        public string DriveTime
        {
            get { return driveTime; }
            set
            {
                if (driveTime != value)
                {
                    driveTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private string drivePrice;
        public string DrivePrice
        {
            get { return drivePrice; }
            set
            {
                if (drivePrice != value)
                {
                    drivePrice = value;
                    OnPropertyChanged();
                }
            }
        }

        private string currentDrive;
        public string CurrentDrive
        {
            get { return currentDrive; }
            set
            {
                if (currentDrive != value)
                {
                    currentDrive = value;
                    OnPropertyChanged();
                }
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
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

        private Visibility startDriveVisibility;
        public Visibility StartDriveVisibility
        {
            get { return startDriveVisibility; }
            set
            {
                if (startDriveVisibility != value)
                {
                    startDriveVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility finishDriveVisibility;
        public Visibility FinishDriveVisibility
        {
            get { return finishDriveVisibility; }
            set
            {
                if (finishDriveVisibility != value)
                {
                    finishDriveVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isStartEnabled;
        public bool IsStartEnabled
        {
            get { return isStartEnabled; }
            set
            {
                if (isStartEnabled != value)
                {
                    isStartEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isLocationEnabled;
        public bool IsLocationEnabled
        {
            get { return isLocationEnabled; }
            set
            {
                if (isLocationEnabled != value)
                {
                    isLocationEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isDismissEnabled;
        public bool IsDismissEnabled
        {
            get { return isDismissEnabled; }
            set
            {
                if (isDismissEnabled != value)
                {
                    isDismissEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isOnLocation;
        public bool IsOnLocation
        {
            get { return isOnLocation; }
            set
            {
                if (isOnLocation != value)
                {
                    isOnLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool isLateEnabled;
        public bool IsLateEnabled
        {
            get { return isLateEnabled; }
            set
            {
                if (isLateEnabled != value)
                {
                    isLateEnabled = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private bool delayHitTest;
        public bool DelayHitTest
        {
            get { return delayHitTest; }
            set
            {
                if (delayHitTest != value)
                {
                    delayHitTest = value;
                    OnPropertyChanged();
                }
            }
        }

        private int delay;
        public int Delay
        {
            get { return delay; }
            set
            {
                if (delay != value)
                {
                    delay = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<string> notifications;
        public List<string> Notifications
        {
            get { return notifications; }
            set
            {
                if (notifications != value)
                {
                    notifications = value;
                    OnPropertyChanged();
                }
            }
        }

        private Visibility notifyBellVisibility;
        public Visibility NotifyBellVisibility
        {
            get { return notifyBellVisibility; }
            set
            {
                if (notifyBellVisibility != value)
                {
                    notifyBellVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        public MyICommand ProfileCommand { get; set; }
        public MyICommand AcceptExtraDriveCommand { get; set; }
        public MyICommand DismissDriveCommand { get; set; }
        public MyICommand FinishDriveCommand { get; set; }
        public MyICommand LeaveCommand { get; set; }
        public MyICommand StartCommand { get; set; }
        public MyICommand OnLocationCommand { get; set; }
        public MyICommand LateCommand { get; set; }
        public MyICommand HelpCommand { get; set; }
        public MyICommand ReadCommand { get; set; }

        public ObservableCollection<DriveReservationDto> DriveReservations { get; set; }
        public ObservableCollection<DriveReservationDto> ExtraDriveReservations { get; set; }
        public DriveReservationDto SelectedDriveReservation { get; set; }
        public DriveReservationDto SelectedExtraDrive { get; set; }
        public string SelectedNotification { get; set; }

        private DriveReservationService driveReservationService;
        private VehicleLocationService vehicleLocationService;
        private VehicleLanguageService vehicleLanguageService;
        private DriveService driveService;
        private DriverService driverService;
        private VehicleService vehicleService;
        private AddressService addressService;
        private SuperPointsService superPointsService;
        private TourGuestService tourGuestService;
        private GroupDriveReservationService groupReservationService;
        private LanguageService languageService;
        private LocationService locationService;
        private DriveService driveLocationService;
        public NavigationService NavigationService { get; set; }

        private readonly int id;
        private DispatcherTimer timer;
        private int hours = 0, minutes = 0, seconds = 0;
        public int StartPrice = 80;
        public int EndPrice = 80;
        private Driver driver;
        private int points;

        public DriverHomePageViewModel(int id, NavigationService navigationService)
        {
            addressService = new AddressService(Injector.CreateInstance<IAddressRepository>());
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            vehicleLocationService = new VehicleLocationService(Injector.CreateInstance<IVehicleLocationsRepository>(),
                        addressService, new VehicleService(Injector.CreateInstance<IVehicleRepository>()));
            vehicleLanguageService = new VehicleLanguageService(Injector.CreateInstance<IVehicleLanguageRepository>(),
                        languageService);
            vehicleService = new VehicleService(Injector.CreateInstance<IVehicleRepository>(),
                        vehicleLanguageService, vehicleLocationService);
            driveReservationService = new DriveReservationService(Injector.CreateInstance<IDriveReservationRepository>(),
                        vehicleLocationService, new UserService(Injector.CreateInstance<IUserRepository>()));
            driveService = new DriveService(Injector.CreateInstance<IDriveRepository>());
            driverService = new DriverService(Injector.CreateInstance<IDriverRepository>());
            superPointsService = new SuperPointsService(Injector.CreateInstance<ISuperPointsRepository>());
            tourGuestService = new TourGuestService(Injector.CreateInstance<ITourGuestRepository>(),
                        new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()));
            groupReservationService = new GroupDriveReservationService(Injector.CreateInstance<IGroupDriveReservationRepository>(),
                        driveReservationService, vehicleService);
            driveLocationService = new DriveService(Injector.CreateInstance<IDriveRepository>(), addressService, locationService);
            NavigationService = navigationService;
            ProfileCommand = new MyICommand(Execute_ProfilePage);
            AcceptExtraDriveCommand = new MyICommand(Execute_AcceptExtraDrive);
            DismissDriveCommand = new MyICommand(Execute_DismissDrive);
            FinishDriveCommand = new MyICommand(Execute_FinishDrive);
            LeaveCommand = new MyICommand(Execute_Leave);
            StartCommand = new MyICommand(Execute_Start);
            OnLocationCommand = new MyICommand(Execute_OnLocation);
            LateCommand = new MyICommand(Execute_Late);
            HelpCommand = new MyICommand(Execute_Help);
            ReadCommand = new MyICommand(Execute_Read);

            DriveReservations = new ObservableCollection<DriveReservationDto>();
            ExtraDriveReservations = new ObservableCollection<DriveReservationDto>();
            SelectedDriveReservation = new DriveReservationDto();
            SelectedExtraDrive = new DriveReservationDto();
            SelectedNotification = "";

            driveService.Subscribe(this);
            driveReservationService.Subscribe(this);

            this.id = id;
            timer = new DispatcherTimer();
            DriveTime = "00:00:00";
            DrivePrice = "0";
            driver = driverService.GetByUserId(id);
            Name = driver.Name;
            Notifications = new List<string>();
            IsLateEnabled = true;
            IsStartEnabled = false;
            IsDismissEnabled = true;
            isOnLocation = true;
            DelayHitTest = true;

            FinishDriveVisibility = Visibility.Collapsed;
            RegistrationVisibility = Visibility.Visible;
            VehicleVisibility = Visibility.Collapsed;
            NotifyBellVisibility = Visibility.Visible;
            groupReservationService.ManageGroupDrives();
            driveReservationService.DismissBreakRequest(id);
            CheckLocation();
            Update();
            CheckReservations();
        }

        public void Update()
        {
            DriveReservations.Clear();
            ExtraDriveReservations.Clear();
            LoadDrives();
            if (!HasDriverRegisteredVehicle()) { return; }

            RegistrationVisibility = Visibility.Collapsed;
            VehicleVisibility = Visibility.Visible;
            SetVehicleName();
            WriteCurrentAddress();
            CheckSuperDriver();
            CheckNotificaitons();
        }

        private void CheckNotificaitons()
        {
            if(Notifications.Count == 0)
            {
                NotifyBellVisibility = Visibility.Collapsed;
                return;
            }
            NotifyBellVisibility = Visibility.Visible;
        }

        private void CheckLocation()
        {
            if(vehicleService.GetByDriverId(id).Id == 0)
            {
                return;
            }
            int vehicleId = vehicleService.GetByDriverId(id).Id;
            int mostFrequentLocation = driveLocationService.GetMostFrequentLocation();
            List<int> leastFrequentLocation = driveLocationService.GetLeastFrequentLocations();
            if (vehicleLocationService.GetLocationsByVehicleId(vehicleId).Contains(mostFrequentLocation))
            {
                Notifications.Add("You are registered for the BEST location!");
            }
            foreach(int locationid in leastFrequentLocation)
            {
                if (vehicleLocationService.GetLocationsByVehicleId(vehicleId).Contains(locationid))
                {
                    Notifications.Add("You are registered for one of the least wanted location.\nWish you the BEST!");
                    break;
                }
            }
        }

        private void CheckSuperDriver()
        {
            if (driver.IsSuperDriver)
            {
                if ((DateTime.Now - driver.MembershipDate) > TimeSpan.FromDays(365))
                {
                    driver.IsSuperDriver = false;
                    driver.Points = 0;
                    driverService.EditSuperDriver(driver);
                }
                return;
            }
            CountPoints();
        }

        private void CountPoints()
        {
            points = 0;
            foreach(SuperPoints superPoints in superPointsService.GetAllByUserId(id))
            {
                points += superPoints.Value;
            }
            driver.Points = points;
            if (points >= 8)
            {
                driver.IsSuperDriver = true;
                driver.MembershipDate = DateTime.Now;
                driver.Points = 0;
                driverService.EditSuperDriver(driver);
                superPointsService.DeleteAllPoints(id);
            }
        }

        private void LoadDrives()
        {
            foreach (DriveReservation driveReservation in driveReservationService.LoadExtraDrives())
            {
                ExtraDriveReservations.Add(MakeDto(driveReservation));
            }
            List<DriveReservation> reservations = driveReservationService.LoadReservations(id).OrderBy(x => x.DepartureTime).ToList();
            foreach (DriveReservation driveReservation in reservations)
            {
                DriveReservations.Add(MakeDto(driveReservation));
            }
        }

        private DriveReservationDto MakeDto(DriveReservation driveReservation)
        {
            string startAddress = addressService.GetById(driveReservation.StartAddressId).Street;
            string endAddress = addressService.GetById(driveReservation.EndAddressId).Street;
            string touristName = tourGuestService.GetById(driveReservation.UserId).FullName;
            return new DriveReservationDto(driveReservation, startAddress, endAddress, "", touristName);
        }

        public bool HasDriverRegisteredVehicle()
        {
            if (!vehicleService.IsRegistered(id)) { return false; }
            return true;
        }

        public bool IsFirstLogIn()
        {
            return driver.IsFirstLogIn;
        }

        public void SetSecondLogIn()
        {
            driver.IsFirstLogIn = false;
            driverService.EditDriver(driver);
        }

        public void SetVehicleName()
        {
            VehicleName = "vehicle: " + vehicleService.GetNameByDriverId(id);
        }

        public void WriteCurrentAddress()
        {
            if (DriveReservations.Count <= 0)
            {
                CurrentDrive = "No drives at the moment";
                return;
            }
            CurrentDrive = addressService.GetCurrentAddress(DriveReservations[0].ToDriveReservation());
        }

        public void AcceptExtraDrive()
        {
            if (SelectedExtraDrive.EndAddress == null)
            {
                MessageBox.Show("Select a drive you want to accept", "No selected item", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            AddPoints();
            driveReservationService.UpdateFastDrive(SelectedExtraDrive.ToDriveReservation(), id);
            driverService.EditSuperDriver(driver);
        }

        private void AddPoints()
        {
            if (driveReservationService.GetById(SelectedExtraDrive.Id).OriginalDriverId != 0)
            {
                driver.Points += 5;
                MessageBox.Show("You earned 5 points for accepting\nthis drive from collegue!", "Drive Accepted", MessageBoxButton.OK, MessageBoxImage.Information);
                Notifications.Add("Thanks for being a team player!");
            }
            else
            {
                if (!driver.IsSuperDriver)
                {
                    superPointsService.AddPoint(id, 1);
                }
                driver.Points += 1;
                MessageBox.Show("You earned a point for accepting\na fast drive reservaton!", "Drive Accepted", MessageBoxButton.OK, MessageBoxImage.Information);
                Notifications.Add("Thanks for being fast! New drive added.");
            }
        }

        public void StartTime()
        {
            StartDriveVisibility = Visibility.Collapsed;
            FinishDriveVisibility = Visibility.Visible;
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            seconds++;
            if (seconds % 5 == 0) { EndPrice += 10; }
            if (seconds == 60) { minutes++; seconds = 0; }
            if (minutes == 60) { hours++; minutes = 0; }
            DriveTime = $"{hours:00}:{minutes:00}:{seconds:00}";
            DrivePrice = EndPrice.ToString();
        }

        public void FinishDrive()
        {
            StartDriveVisibility = Visibility.Visible;
            FinishDriveVisibility = Visibility.Collapsed;
            FinishedDriveInformation finishedDrive = new FinishedDriveInformation(EndPrice);
            finishedDrive.Show();
        }

        public void FinishState()
        {
            Drive drive = new Drive(DriveReservations[0].ToDriveReservation(), StartPrice, EndPrice, DateTime.Now);
            driveService.Add(drive);
            driveReservationService.Delete(DriveReservations[0].ToDriveReservation());

            timer.Stop();
            seconds = 0; minutes = 0; hours = 0;
            EndPrice = StartPrice;
            DriveTime = "00:00:00";
            DrivePrice = "0";
            Delay = 0;
        }

        public void DismissDrive()
        {
            TimeSpan timeUntilDrive = SelectedDriveReservation.DepartureTime - DateTime.Now;
            if(timeUntilDrive.TotalHours <= 48)
            {
                MessageBox.Show("It's too late to dismiss chosen drive.\nIt's in less than 48 hours!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            DeleteDriveReservation();

        }

        private void DeleteDriveReservation()
        {
            MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to DISMISS selected drive\nat"
                + SelectedDriveReservation.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                + "?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (dialogResult == MessageBoxResult.Cancel)
            {
                return;
            }

            driveReservationService.Delete(SelectedDriveReservation.ToDriveReservation());
            DeletePoints();
        }

        private void DeletePoints()
        {
            if (driver.IsSuperDriver)
            {
                if (driver.Points <= 5) { driver.Points = 0; }
                else { driver.Points -= 5; }
                driverService.EditSuperDriver(driver);
            }
        }

        private void Execute_Help()
        {
            NavigationService.Navigate(new DriverHelpPage("MainPage", id, NavigationService));
        }

        private void Execute_Late()
        {
            DelayHitTest = false;
            IsLateEnabled = false;
            IsDismissEnabled = false;
        }

        public void Execute_OnLocation()
        {
            IsStartEnabled = true;
            IsOnLocation = false;
            IsLateEnabled = false;
            DelayHitTest = false;
            IsDismissEnabled = false;
        }

        private void CheckReservations()
        {
            if (DriveReservations.Count == 0)
            {
                IsOnLocation = false;
                IsLateEnabled = false;
                DelayHitTest = false;
            }
            else
            {
                IsOnLocation = true;
                IsLateEnabled = true;
                DelayHitTest = true;
            }
            if (HasDriverRegisteredVehicle())
            {
                IsOnLocation = true;
                IsLateEnabled = true;
                DelayHitTest = true;
            }
            else
            {
                IsOnLocation = false;
                IsLateEnabled = false;
                DelayHitTest = false;
            }
        }

        private void Execute_Start()
        {
            IsStartEnabled = false;
            IsOnLocation = false;

            StartTime();
        }

        private void Execute_Leave()
        {
            FinishState();
            IsStartEnabled = false;
        }

        private void Execute_FinishDrive()
        {
            FinishDrive();
            FinishState();
            IsDismissEnabled = true;
            IsOnLocation = true;
            IsLateEnabled = true;
            DelayHitTest = true;
            CheckReservations();
        }

        private void Execute_DismissDrive()
        {
            DismissDrive();
        }

        private void Execute_AcceptExtraDrive()
        {
            AcceptExtraDrive();
        }

        private void Execute_ProfilePage()
        {
            NavigationService.Navigate(new DriverProfilePage(id, NavigationService));
        }

        private void Execute_Read()
        {
            if (SelectedNotification == "")
            {
                MessageBox.Show("Choose a notification that you want to read.", "No selected item", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Notifications.Remove(SelectedNotification);
        }

        public void SignOut()
        {
            SignInForm signIn = new SignInForm();
            signIn.Show();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
