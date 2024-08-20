using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View.Guide;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using BookingApp.WPF.View;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class CreateSpecifiedTourViewModel : INotifyPropertyChanged
    {
        public NavigationService NavService { get; set; }
        public string Type {  get; set; }

        private TourRequestDto tourRequest;
        public MyICommand SelectionChangedCommand { get; set; }
        public MyICommand AddTourStartCommand { get; set; }
        public MyICommand<DateTime> RemoveTourStartCommand { get; set; }

        public MyICommand AddCheckPointCommand { get; set; }
        public MyICommand<CheckPointDto> RemoveCheckPointCommand { get; set; }

        public MyICommand FindImageCommand { get; set; }
        public MyICommand<string> RemoveImageCommand { get; set; }

        public MyICommand SaveCommand { get; set; }

        private SavingTourService savingTourService;

        private ImageService imageService;

        private LanguageService languageService;

        private LocationService locationService;
        private TourRequestService tourRequestService;
        private TourRequestNotificationService tourRequestNotificationService;
        private CreatingRequestedTourAndReservationService creatingRequestedTourAndReservationService;

        public List<LocationDto> Countries { get; set; }
        public LocationDto SelectedCountry {  get; set; }
        public ObservableCollection<LocationDto> Cities { get; set; }
        public LocationDto SelectedCity { get; set; }
        public List<LanguageDto> Languages { get; }
        public LanguageDto SelectedLanguage { get; set; }
        public ObservableCollection<CheckPointDto> CheckPoints { get; set; }

        private string checkPointName;
        public string CheckPointName
        {
            get
            {
                return checkPointName;
            }
            set
            {
                if (value != checkPointName)
                {
                    checkPointName = value;
                    OnPropertyChanged("CheckPointName");
                }

            }
        }
        public ObservableCollection<DateTime> TourStarts { get; set; }
        public ObservableCollection<DateTime> DisabledDates { get; set; }
        private DateTime tourDate;
        public DateTime TourDate
        {
            get
            {
                return tourDate;
            }
            set
            {
                if (value != tourDate)
                {
                    tourDate = value;
                    OnPropertyChanged("TourDate");
                    AddTourStartCommand.RaiseCanExecuteChanged();
                }

            }
        }
        private DateTime startDate;
        private DateTime endDate;
        private bool freeDayExist=true;

        public MyICommand<DatePicker> LoadDatesCommand { get; set; }

        private int hours;
        public int Hours
        {
            get
            {
                return hours;
            }
            set
            {
                if (value != hours)
                {
                    hours = value;
                    OnPropertyChanged("Hours");
                    AddTourStartCommand.RaiseCanExecuteChanged();
                }

            }
        }

        private int minutes;
        public int Minutes
        {
            get
            {
                return minutes;
            }
            set
            {
                if (value != minutes)
                {
                    minutes = value;
                    OnPropertyChanged("Minutes");
                    AddTourStartCommand.RaiseCanExecuteChanged();
                }

            }
        }
        public ObservableCollection<string> Paths { get; set; }

        public TourDto Tour { get; set; }

        private int index;
        private DateTime tourStart;

        public CreateSpecifiedTourViewModel(NavigationService navService,TourRequestDto tourRequest,string type)
        {
            NavService = navService;
            Type = type;
            this.tourRequest = tourRequest;

            SelectionChangedCommand = new MyICommand(Execute_SelectionChangedCommand);
            AddTourStartCommand = new MyICommand(Execute_AddTourStartCommand, CanExecute_AddTourStartCommand);
            RemoveTourStartCommand = new MyICommand<DateTime>(Execute_RemoveTourStartCommand);
            AddCheckPointCommand = new MyICommand(Execute_AddCheckPointCommand);
            RemoveCheckPointCommand = new MyICommand<CheckPointDto>(Execute_RemoveCheckPointCommand);
            FindImageCommand = new MyICommand(Execute_FindImageCommand);
            RemoveImageCommand = new MyICommand<string>(Execute_RemoveImageCommand);
            SaveCommand = new MyICommand(Execute_SaveCommand, CanExecute_SaveCommand);

            imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            savingTourService = new SavingTourService(new TourService(Injector.CreateInstance<ITourRepository>()), new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), new TourService(Injector.CreateInstance<ITourRepository>()), new LocationService(Injector.CreateInstance<ILocationRepository>()), new LanguageService(Injector.CreateInstance<ILanguageRepository>()), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>())), imageService, new CheckPointService(Injector.CreateInstance<ICheckPointRepository>()));
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            tourRequestService = new TourRequestService(Injector.CreateInstance<ITourRequestRepository>(), new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), new TourService(Injector.CreateInstance<ITourRepository>()), new LocationService(Injector.CreateInstance<ILocationRepository>()), new LanguageService(Injector.CreateInstance<ILanguageRepository>()), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>())));
            tourRequestNotificationService = new TourRequestNotificationService(Injector.CreateInstance<ITourRequestNotificationRepository>(), tourRequestService);
            creatingRequestedTourAndReservationService = new CreatingRequestedTourAndReservationService(savingTourService, new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), new TourService(Injector.CreateInstance<ITourRepository>()), new LocationService(Injector.CreateInstance<ILocationRepository>()), new LanguageService(Injector.CreateInstance<ILanguageRepository>()), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>())), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new TourGuestService(Injector.CreateInstance<ITourGuestRepository>(), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>())), new TourRequestTourGuestService(Injector.CreateInstance<ITourRequestTourGuestRepository>()));

            Tour = new TourDto();

            Countries = new List<LocationDto>();
            Cities = new ObservableCollection<LocationDto>();
            Languages = new List<LanguageDto>();

            TourStarts = new ObservableCollection<DateTime>();
            TourDate = DateTime.Now;

            CheckPoints = new ObservableCollection<CheckPointDto>();
            this.index = 0;

            Paths = new ObservableCollection<string>();

            DisabledDates = new ObservableCollection<DateTime>();

            if (type.Equals("SuggestedLocation")) LoadSuggestedLocation();
            else if (type.Equals("SuggestedLanguage")) LoadSuggestedLanguage();
            else LoadTourInfoFromRequest();
        }
        private void LoadSuggestedLocation()
        {
            SetupLocation();
            LoadLanguages();
            SelectedLanguage = new LanguageDto();
        }

        private void LoadSuggestedLanguage()
        {
            LoadCountries();
            Countries = Countries.GroupBy(x => x.Country).Select(x => x.First()).ToList();
            SelectedCountry = new LocationDto();
            SetupLanguages();
        }

        private void LoadTourInfoFromRequest()
        {
            SetupDatePicker();
            SetupLocation();
            SetupLanguages();
            Tour.Capacity = tourRequest.NumberOfGuests;
            Tour.Description = tourRequest.Description;
        }

        public void SetupDatePicker()
        {
            startDate = DateTime.ParseExact(tourRequest.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            endDate = DateTime.ParseExact(tourRequest.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            TourDate = tourRequestService.FindUserFirstFreeDay(startDate, endDate,SignInForm.curretnUserId);
            LoadDisabledDates();
            LoadDatesCommand = new MyICommand<DatePicker>(Execute_LoadDatesCommand);
        }

        private void LoadDisabledDates()
        {
            AddDatesBeforeStartDate();
            foreach (var date in tourRequestService.FindUserBusyDays(startDate, endDate,SignInForm.curretnUserId))
            {
                if (!DisabledDates.Contains(date))
                    DisabledDates.Add(date);
            }
            AddDatesAfterEndDate();
        }

        private void AddDatesBeforeStartDate()
        {
            DateTime dateBefore = new DateTime(2010, 1, 1);
            while (dateBefore < startDate)
            {
                DisabledDates.Add(dateBefore);
                dateBefore = dateBefore.AddDays(1);
            }
        }

        private void AddDatesAfterEndDate()
        {
            DateTime dateAfter = new DateTime(2030, 12, 31);
            endDate = endDate.AddDays(1);
            while (endDate <= dateAfter)
            {
                DisabledDates.Add(endDate);
                if (endDate >= dateAfter)
                    break;
                endDate = endDate.AddDays(1);
            }
            endDate = DateTime.ParseExact(tourRequest.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        private void Execute_LoadDatesCommand(DatePicker datePicker)
        {
            if (datePicker != null)
            {
                foreach (var date in DisabledDates)
                {
                    datePicker.BlackoutDates.Add(new CalendarDateRange(date));
                }
            }
        }

        public void SetupLocation()
        {
            LoadCountries();
            SelectedCountry = Countries.Find(c => c.Id == tourRequest.Location.Id);
            LoadCities();
            SelectedCity = Cities.First(c => c.Id == tourRequest.Location.Id);
        }

        public void SetupLanguages()
        {
            LoadLanguages();
            SelectedLanguage = Languages.Find(l => l.Id == tourRequest.Language.Id);
        }

        private void LoadLanguages()
        {
            foreach (var language in languageService.GetAll())
            {
                Languages.Add(new LanguageDto(language));
            }
        }

        public void LoadCountries()
        {
            foreach (var location in locationService.GetAll())
            {
                Countries.Add(new LocationDto(location));
            }
        }

        private void Execute_SelectionChangedCommand()
        {
            LoadCities();
        }

        public void LoadCities()
        {
            SelectedCity = new LocationDto();
            Cities.Clear();
            foreach (var city in locationService.LoadCities(SelectedCountry.Country))
                Cities.Add(new LocationDto(city));
        }


        private void Execute_SaveCommand()
        {
            Tour.LocationId = SelectedCity.Id;
            Tour.LanguageId = SelectedLanguage.Id;
            Tour.UserId = SignInForm.curretnUserId;

            if (Type.Equals("TourRequest")) {
                creatingRequestedTourAndReservationService.CreateTourAndReserve(Tour.ToTour(), TourStarts.ToList(), GetCheckPoints(), Paths.ToList(), tourRequest.ToTourRequest());
                tourRequestNotificationService.Create(tourRequest.ToTourRequest(),Tour.Name,TourStarts.FirstOrDefault().ToString("dd/MM/yyyy HH:mm"), SignInForm.curretnUserId);
                MessageBox.Show("You created a requested tour!");
                NavService.Navigate(new RequestsPage(NavService));
                return;
            }
            savingTourService.SaveTour(Tour.ToTour(), TourStarts.ToList(), GetCheckPoints(), Paths.ToList());
            MessageBox.Show("You created a tour!");
            NavService.Navigate(new RequestsStatisticsPage(NavService));
        }

        private List<CheckPoint> GetCheckPoints()
        {
            List<CheckPoint> checkPoints = new List<CheckPoint>();
            foreach (var checkPoint in CheckPoints)
            {
                checkPoints.Add(checkPoint.ToCheckPoint());
            }
            return checkPoints;
        }

        private bool CanExecute_SaveCommand()
        {
            return this.CheckPoints.Count >= 2 && this.TourStarts.Count > 0 && this.Paths.Count > 0;
        }

        private void Execute_RemoveImageCommand(string obj)
        {
            Paths.Remove(obj);
            FindImageCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void Execute_FindImageCommand()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Slike|*.jpg;*.jpeg";
            openFileDialog.InitialDirectory = @"Resources\Images\";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == true)
            {
                string[] selectedImagePaths = openFileDialog.FileNames;
                if (selectedImagePaths.Length > 0)
                {
                    AddPaths(selectedImagePaths);
                }
            }
        }

        private void AddPaths(string[] selectedPaths)
        {
            foreach (string selectedPath in selectedPaths)
            {

                string relativePath = "../../../";
                string[] absolutePath = selectedPath.Split("\\");
                relativePath += absolutePath[absolutePath.Length - 3] + "/" + absolutePath[absolutePath.Length - 2] + "/" + absolutePath[absolutePath.Length - 1];
                this.Paths.Add(relativePath);
            }
            SaveCommand.RaiseCanExecuteChanged();
        }

        private void Execute_RemoveCheckPointCommand(CheckPointDto checkPointDto)
        {
            foreach (var checkPoint in CheckPoints)
            {
                if (checkPoint.Index > checkPointDto.Index)
                {
                    checkPoint.Index--;
                }
            }
            CheckPoints.Remove(checkPointDto);
            SaveCommand.RaiseCanExecuteChanged();
            this.index--;
        }

        private void Execute_AddCheckPointCommand()
        {
            this.index++;
            CheckPointDto checkPointDto = new CheckPointDto();
            checkPointDto.Name = CheckPointName;
            checkPointDto.Index = this.index;
            CheckPoints.Add(checkPointDto);
            CheckPointName = "";
            SaveCommand.RaiseCanExecuteChanged();
        }

        private bool CanExecute_AddTourStartCommand()
        {
            if(Type.Equals("TourRequest")) { this.tourStart = MakeTourStart(); return !(TourStarts.Count > 0); }
            this.tourStart = MakeTourStart();
            return IsTourStartValid(tourStart);
        }

        private void Execute_AddTourStartCommand()
        {
            TourStarts.Add(tourStart);
            SaveCommand.RaiseCanExecuteChanged();
            ResetDateandTime();
        }

        private void Execute_RemoveTourStartCommand(DateTime time)
        {
            TourStarts.Remove(time);
            SaveCommand.RaiseCanExecuteChanged();
            AddTourStartCommand.RaiseCanExecuteChanged();
        }

        private DateTime MakeTourStart()
        {
            DateTime tourStartTime = TourDate.Date;
            tourStartTime = tourStartTime.AddHours(Hours);
            tourStartTime = tourStartTime.AddMinutes(Minutes);
            return tourStartTime;
        }

        private bool IsTourStartValid(DateTime inputDate)
        {
            foreach (var ts in TourStarts)
            {
                if (ts.Date == inputDate.Date)
                {
                    if (!CheckDuration(ts, inputDate)) return false;
                }
            }
            return true;
        }

        private bool CheckDuration(DateTime checkDate, DateTime inputdate)
        {
            if (checkDate.Hour < inputdate.Hour)
            {
                return (inputdate - checkDate).TotalHours >= Tour.Duration;
            }
            else if (checkDate.Hour > inputdate.Hour)
            {
                return (checkDate - inputdate).TotalHours >= Tour.Duration;
            }
            else
            {
                return false;
            }
        }

        private void ResetDateandTime()
        {
            Hours = 0;
            Minutes = 0;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }

    }
}
