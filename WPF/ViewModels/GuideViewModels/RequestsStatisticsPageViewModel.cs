using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Services;
using BookingApp.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Globalization;
using BookingApp.WPF.View.Guide;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class RequestsStatisticsPageViewModel:INotifyPropertyChanged
    {
        public NavigationService NavService { get; set; }
        private LanguageService languageService;
        private LocationService locationService;
        private TourRequestService tourRequestService;
        public List<LocationDto> Locations { get; set; }
        public List<LanguageDto> Languages {  get; set; }
        public List<int> Years { get; set; }
        public ObservableCollection<KeyValuePair<int, int>> StatsPerYear { get; set; }
        public ObservableCollection<KeyValuePair<string, int>> StatsPerMonth { get; set; }
        private LocationDto selectedLocation;
        public LocationDto SelectedLocation
        {
            get
            {
                return selectedLocation;
            }
            set
            {
                if (value != selectedLocation)
                {
                    selectedLocation = value;
                    OnPropertyChanged("SelectedLocation");
                    LoadStatsPerYearByLocation();
                    if (selectedLanguage != null)
                    {
                        selectedLanguage = null;
                        OnPropertyChanged("SelectedLanguage");
                    }
                    if (SelectedYear != 0) LoadStatsByMonth();
                }

            }
        }
        private LanguageDto selectedLanguage;
        public LanguageDto SelectedLanguage
        {
            get
            {
                return selectedLanguage;
            }
            set
            {
                if (value != selectedLanguage)
                {
                    selectedLanguage = value;
                    OnPropertyChanged("SelectedLanguage");
                    LoadStatsPerYearByLanguage();
                    if (selectedLocation != null)
                    {
                        selectedLocation = null;
                        OnPropertyChanged("SelectedLocation");
                    }
                    if (SelectedYear != 0) LoadStatsByMonth();
                }

            }
        }

        private int selectedYear;
        public int SelectedYear
        {
            get
            {
                return selectedYear;
            }
            set
            {
                if (value != selectedYear)
                {
                    selectedYear = value; 
                    OnPropertyChanged("SelectedYear");
                    LoadStatsByMonth();
                }

            }
        }

        public MyICommand NavigateToMostWantedLanguageCommand { get; set; }
        public MyICommand NavigateToMostWantedLocationCommand { get; set; }

        public RequestsStatisticsPageViewModel(NavigationService navService) 
        { 
            NavService = navService;
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            tourRequestService = new TourRequestService(Injector.CreateInstance<ITourRequestRepository>(), new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), new TourService(Injector.CreateInstance<ITourRepository>()), new LocationService(Injector.CreateInstance<ILocationRepository>()), new LanguageService(Injector.CreateInstance<ILanguageRepository>()), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>())));
            NavigateToMostWantedLanguageCommand = new MyICommand(Execute_NavigateToMostWantedLanguageCommand);
            NavigateToMostWantedLocationCommand = new MyICommand(Execute_NavigateToMostWantedLocationCommand);
            Locations = new List<LocationDto>();
            Languages = new List<LanguageDto>();
            Years = new List<int>();
            StatsPerYear = new ObservableCollection<KeyValuePair<int, int>>();
            StatsPerMonth = new ObservableCollection<KeyValuePair<string, int>>();
            LoadYears();
            LoadLocations();
            LoadLanguages();
        }

        private void Execute_NavigateToMostWantedLanguageCommand()
        {
            int langugageId=tourRequestService.FindMostWantedLanguage();
            TourRequestDto tourRequest=new TourRequestDto();
            tourRequest.Language=languageService.GetById(langugageId);
            NavService.Navigate(new CreateSpecifiedTour(NavService,tourRequest, "SuggestedLanguage"));
        }
        private void Execute_NavigateToMostWantedLocationCommand()
        {
            int locationId = tourRequestService.FindMostWantedLocation();
            TourRequestDto tourRequest = new TourRequestDto();
            tourRequest.Location = locationService.GetById(locationId);
            NavService.Navigate(new CreateSpecifiedTour(NavService, tourRequest, "SuggestedLocation"));
        }

        private void LoadYears()
        {
            int currentYear = DateTime.Now.Year;
            for (int year = currentYear; year >= 2000; year--)
            {
                Years.Add(year);
            }
        }

        private void LoadLocations()
        {
            foreach (var location in locationService.GetAll()) Locations.Add(new LocationDto(location));
        }
        private void LoadLanguages()
        {
            foreach (var language in languageService.GetAll()) Languages.Add(new LanguageDto(language));
        }

        private void LoadStatsPerYearByLanguage()
        {
            if (SelectedLanguage != null)
            {
                StatsPerYear.Clear();
                foreach (int year in Years)
                {
                    int value = tourRequestService.FindAllRequestsByYearandParametar(year, SelectedLanguage.Id, "Language");
                    StatsPerYear.Add(new KeyValuePair<int, int>(year, value));
                }
            }
        }

        private void LoadStatsPerYearByLocation()
        {
            if (SelectedLocation != null)
            {
                StatsPerYear.Clear();
                foreach (int year in Years)
                {
                    int value = tourRequestService.FindAllRequestsByYearandParametar(year, SelectedLocation.Id, "Location");
                    StatsPerYear.Add(new KeyValuePair<int, int>(year, value));
                }
            }
        }

        private void LoadStatsByMonth()
        {
            StatsPerMonth.Clear();
            string[] monthNames = DateTimeFormatInfo.CurrentInfo.MonthNames;
            int i = 1;
            if (SelectedLocation != null) LoadStatsByLocation(monthNames, i);
            else if (SelectedLanguage != null) LoadStatsByLanguage(monthNames, i);
            else return;
        }

        private void LoadStatsByLocation(string[] months,int index)
        {
            foreach (string month in months)
            {
                if(string.IsNullOrEmpty(month)) return;
                int value = tourRequestService.FindAllRequestsByMonthandParametar(index, SelectedYear, SelectedLocation.Id, "Location");
                StatsPerMonth.Add(new KeyValuePair<string, int>(month, value));
                index++;
            }
        }

        private void LoadStatsByLanguage(string[] months, int index)
        {
            foreach (string month in months)
            {
                if(string.IsNullOrEmpty(month)) return;
                int value = tourRequestService.FindAllRequestsByMonthandParametar(index, SelectedYear, SelectedLanguage.Id, "Language");
                StatsPerMonth.Add(new KeyValuePair<string, int>(month, value));
                index++;
            }
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
