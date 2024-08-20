using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Image = BookingApp.Domain.Model.Image;
using System.ComponentModel;
using BookingApp.Services;
using System.Printing;
using System.Windows.Navigation;
using BookingApp.Utilities;
using System.Collections.Specialized;
using BookingApp.WPF.View.Guide;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.View;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class AddingTourPageViewModel:INotifyPropertyChanged
    {
        public NavigationService NavService {get; set; }
        public MyICommand SelectionChangedCommand { get; set; }
        public MyICommand AddTourStartCommand { get; set; }
        public MyICommand<DateTime> RemoveTourStartCommand { get; set; }

        public MyICommand AddCheckPointCommand {  get; set; }
        public MyICommand<CheckPointDto> RemoveCheckPointCommand { get; set; }

        public MyICommand FindImageCommand {  get; set; }
        public MyICommand<string> RemoveImageCommand { get; set; }

        public MyICommand SaveCommand { get; set; }

        private SavingTourService savingTourService;

        private ImageService imageService;

        private LanguageService languageService;

        private LocationService locationService;
        public List<LocationDto> Countries { get; set; }
        public LocationDto SelectedCountry { get; set; }
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
        private DateTime tourDate;
        public DateTime TourDate {
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

        public AddingTourPageViewModel(NavigationService navService)
        {
            NavService = navService;

            SelectionChangedCommand = new MyICommand(Execute_SelectionChangedCommand);
            AddTourStartCommand = new MyICommand(Execute_AddTourStartCommand, CanExecute_AddTourStartCommand);
            RemoveTourStartCommand = new MyICommand<DateTime>(Execute_RemoveTourStartCommand);
            AddCheckPointCommand = new MyICommand(Execute_AddCheckPointCommand);
            RemoveCheckPointCommand = new MyICommand<CheckPointDto>(Execute_RemoveCheckPointCommand);
            FindImageCommand = new MyICommand(Execute_FindImageCommand);
            RemoveImageCommand = new MyICommand<string>(Execute_RemoveImageCommand);
            SaveCommand = new MyICommand(Execute_SaveCommand,CanExecute_SaveCommand);

            imageService= new ImageService(Injector.CreateInstance<IImageRepository>());
            savingTourService = new SavingTourService(new TourService(Injector.CreateInstance<ITourRepository>()), new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(),new TourService(Injector.CreateInstance<ITourRepository>()),new LocationService(Injector.CreateInstance<ILocationRepository>()),new LanguageService(Injector.CreateInstance<ILanguageRepository>()),new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()),new CheckPointService(Injector.CreateInstance<ICheckPointRepository>())),imageService, new CheckPointService(Injector.CreateInstance<ICheckPointRepository>()));
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());

            Countries = new List<LocationDto>();
            Cities = new ObservableCollection<LocationDto>();
            SelectedCountry = new LocationDto();
            SelectedCity = new LocationDto();
            Languages = new List<LanguageDto>();
            SelectedLanguage = new LanguageDto();

            Tour = new TourDto();

            TourStarts = new ObservableCollection<DateTime>();
            TourDate = DateTime.Now;

            CheckPoints = new ObservableCollection<CheckPointDto>();
            this.index = 0;

            Paths = new ObservableCollection<string>();

            LoadUniqueCountries();
            LoadLanguages();
        }


        private void LoadLanguages()
        {
            foreach(var language in languageService.GetAll()) 
            {
                Languages.Add(new LanguageDto(language));    
            }
        }

        public void LoadUniqueCountries()
        {
            foreach (var location in locationService.GetAll())
            {
                Countries.Add(new LocationDto(location));
            }
            Countries = Countries.GroupBy(x => x.Country).Select(x => x.First()).ToList();
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
            Tour.UserId=SignInForm.curretnUserId;


            savingTourService.SaveTour(Tour.ToTour(), TourStarts.ToList(),GetCheckPoints(), Paths.ToList());
            MessageBox.Show("You created a tour!");
            NavService.Navigate(new HomePage(NavService));
        }

        private List<CheckPoint> GetCheckPoints()
        {
            List<CheckPoint> checkPoints= new List<CheckPoint>();
            foreach(var checkPoint in CheckPoints)
            {
                checkPoints.Add(checkPoint.ToCheckPoint());
            }
            return checkPoints;
        }

        private bool CanExecute_SaveCommand()
        {
            return this.CheckPoints.Count >=2 && this.TourStarts.Count > 0 && this.Paths.Count > 0;
        }

        private void Execute_RemoveImageCommand(string obj)
        {
            Paths.Remove(obj);
            FindImageCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
        }

        /*private bool CanExecute_FindImageCommand()
        {
            return MakeFilter() != "";
        }*/

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

        /*private string MakeFilter()
        {
            string filter = imageService.GetAllUnassignedImages(this.Paths.ToList());
            if (filter == "") return "";
            return filter;
        }*/

        private void AddPaths(string[] selectedPaths)
        {
            foreach (string selectedPath in selectedPaths)
            {

                string relativePath = "../../../";
                string[] absolutePath = selectedPath.Split("\\");
                relativePath += absolutePath[absolutePath.Length - 3] + "/" + absolutePath[absolutePath.Length - 2] + "/" + absolutePath[absolutePath.Length - 1];
                this.Paths.Add(relativePath);
            }

            //FindImageCommand.RaiseCanExecuteChanged();
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
        }

        private DateTime MakeTourStart()
        {
            DateTime tourStartTime= TourDate.Date;
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
                    if(!CheckDuration(ts, inputDate)) return false;
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
