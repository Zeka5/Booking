using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View;
using BookingApp.WPF.View.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels.GuideViewModels
{
    public class RequestsPageViewModel : INotifyPropertyChanged
    {
        public NavigationService NavService { get; set; }

        private TourRequestService tourRequestService;
        private LanguageService languageService;
        private LocationService locationService;
        private UserService userService;
        public ObservableCollection<TourRequestDto> TourRequests { get; set; }

        public List<LocationDto> Countries { get; set; }

        public ObservableCollection<LocationDto> Cities { get; set; }

        public List<LanguageDto> Languages { get; set; }
        public MyICommand SearchCommand { get; set; }
        public MyICommand ResetCommand { get; set; }

        public MyICommand<TourRequestDto> AcceptTourRequestCommand { get; set; }

        public MyICommand NavigateToComplexTourRequests {  get; set; } 
        public MyICommand NavigateToRequestsStatistics {  get; set; }

        private LocationDto selectedCountry;
        public LocationDto SelectedCountry
        {
            get
            {
                return selectedCountry;
            }
            set
            {
                if (value != selectedCountry)
                {
                    selectedCountry = value;
                    OnPropertyChanged("SelectedCountry");
                    LoadCitiesofSelectedCountry();
                    SearchCommand.RaiseCanExecuteChanged();
                }

            }
        }

        private LocationDto selectedCity;
        public LocationDto SelectedCity
        {
            get
            {
                return selectedCity;
            }
            set
            {
                if (value != selectedCity)
                {
                    selectedCity = value;
                    OnPropertyChanged("SelectedCity");
                    SearchCommand.RaiseCanExecuteChanged();
                }

            }
        }

        private int capacity;
        public int Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                if (value != capacity)
                {
                    capacity = value;
                    OnPropertyChanged("Capacity");
                    SearchCommand.RaiseCanExecuteChanged();
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
                    SearchCommand.RaiseCanExecuteChanged();
                }

            }
        }

        private DateTime startDate;
        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                if (value != startDate)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                    SearchCommand.RaiseCanExecuteChanged();
                }

            }
        }

        private DateTime endDate;
        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                if (value != endDate)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                    SearchCommand.RaiseCanExecuteChanged();
                }

            }
        }

        public RequestsPageViewModel(NavigationService navService) 
        { 
            NavService=navService;
            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            tourRequestService = new TourRequestService(Injector.CreateInstance<ITourRequestRepository>(), new TourRealizationService(Injector.CreateInstance<ITourRealizationRepository>(), new TourService(Injector.CreateInstance<ITourRepository>()), new LocationService(Injector.CreateInstance<ILocationRepository>()), new LanguageService(Injector.CreateInstance<ILanguageRepository>()), new TourReservationService(Injector.CreateInstance<ITourReservationRepository>()), new CheckPointService(Injector.CreateInstance<ICheckPointRepository>())));
            userService = new UserService(Injector.CreateInstance<IUserRepository>());
            TourRequests=new ObservableCollection<TourRequestDto>();
            Countries = new List<LocationDto>();
            Cities = new ObservableCollection<LocationDto>();
            Languages = new List<LanguageDto>();
            startDate = DateTime.Now.Date;
            endDate = DateTime.Now.Date;
            SearchCommand = new MyICommand(Execute_SearchCommand, CanExecute_SearchCommand);
            ResetCommand = new MyICommand(Execute_ResetCommand);
            AcceptTourRequestCommand = new MyICommand<TourRequestDto>(Execute_AcceptTourRequestCommand);
            NavigateToRequestsStatistics = new MyICommand(Execute_NavigateToRequestStatistics);
            LoadCities();
            LoadCountries();
            LoadLanguages();
            LoadTourRequests();
        }

        private void Execute_NavigateToRequestStatistics()
        {
            NavService.Navigate(new RequestsStatisticsPage(NavService));
        }

        private void LoadCities()
        {
            Cities.Clear();
            foreach (Location location in locationService.GetAll())
            {
                Cities.Add(new LocationDto(location));
            }
        }

        private void LoadCitiesofSelectedCountry()
        {
            Cities.Clear();
            if (SelectedCountry != null)
            {
                foreach (Location location in locationService.LoadCities(SelectedCountry.Country))
                {
                    Cities.Add(new LocationDto(location));
                }
            }
        }

        private void Execute_AcceptTourRequestCommand(TourRequestDto tourRequest)
        {
            DateTime startDate = DateTime.ParseExact(tourRequest.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime endDate = DateTime.ParseExact(tourRequest.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (tourRequestService.FindUserFirstFreeDay(startDate, endDate, SignInForm.curretnUserId) != new DateTime()) NavService.Navigate(new CreateSpecifiedTour(NavService, tourRequest, "TourRequest"));
            else MessageBox.Show("You are not free in this range");
        }

        private void LoadCountries()
        {
            Countries.Clear();
            foreach (Location location in locationService.GetAll())
            {
                Countries.Add(new LocationDto(location));
            }
            Countries = Countries.GroupBy(x => x.Country).Select(x => x.First()).ToList();
        }

        private void LoadLanguages()
        {
            Languages.Clear();
            foreach (Language language in languageService.GetAll())
            {
                Languages.Add(new LanguageDto(language));
            }
        }

        private void LoadTourRequests()
        {
            TourRequests.Clear();
            foreach(TourRequest tourRequest in tourRequestService.GetAllRequestsByStatus("Pending")) 
            { 
                TourRequests.Add(MakeTourRequestDto(tourRequest));
            }
        }

        private TourRequestDto MakeTourRequestDto(TourRequest tourRequest)
        {
            Location location = locationService.GetById(tourRequest.LocationId);
            Language language = languageService.GetById(tourRequest.LanguageId);
            TourRequestDto tourRequestDto = new TourRequestDto(tourRequest, location, language);
            tourRequestDto.TouristName = userService.GetUsernameById(tourRequestDto.TouristId);
            return tourRequestDto;
        }

        private bool CanExecute_SearchCommand()
        {
            return SelectedLanguage != null || SelectedCountry != null || SelectedCity != null || Capacity > 0 || (EndDate != DateTime.Now.Date && StartDate<EndDate);
        }

        private void Execute_SearchCommand()
        {
            TourRequestSearchParametars tourRequestSearch = new TourRequestSearchParametars(GetSelectedCountry(), GetSelectedCity(), Capacity, GetSelectedLanguage(), DateOnly.FromDateTime(StartDate), DateOnly.FromDateTime(EndDate));
            TourRequests.Clear();
            foreach (TourRequest tourRequest in tourRequestService.GetAllRequestsByStatus("Pending"))
            {
                Language language = languageService.GetById(tourRequest.LanguageId);
                Location location = locationService.GetById(tourRequest.LocationId);
                if (tourRequestSearch.IsSearched(tourRequest, location, language))
                {
                    TourRequests.Add(MakeTourRequestDto(tourRequest));
                }

            }
        }

        private string GetSelectedCountry()
        {
            if(SelectedCountry!=null) return SelectedCountry.Country;
            return "";
        }

        private string GetSelectedCity()
        {
            if (SelectedCity != null) return SelectedCity.City;
            return "";
        }

        private string GetSelectedLanguage()
        {
            if (SelectedLanguage != null) return SelectedLanguage.Name;
            return "";
        }

        private void Execute_ResetCommand()
        {
            SelectedLanguage = null;
            SelectedCountry = null;
            SelectedCity = null;
            Capacity = 0;
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date;
            LoadTourRequests();
            LoadCities();
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
