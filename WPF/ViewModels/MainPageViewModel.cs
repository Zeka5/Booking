using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public List<Domain.Model.Type> Types { get; set; }
        public AccommodationDto SelectedAccommodation { get; set; }
        public ObservableCollection<AccommodationDto> AccommodationList { get; set; }
        public AccommodationService AccommodationService { get; set; }
        public ImageService ImageService { get; set; }
        public MyICommand SearchCommand { get; set; }
        public MyICommand ResetCommand { get; set; }
        public MyICommand<AccommodationDto> AccommodationViewNavigationCommand { get; set; }
        public NavigationService NavigationService { get; set; }
        private User user;

        private int guestNumber;
        public int GuestNumber
        {
            get
            {
                return guestNumber;
            }
            set
            {
                if (value != guestNumber)
                {
                    guestNumber = value;
                    OnPropertyChanged(nameof(GuestNumber));
                }
            }
        }

        private int day;

        public int Day
        {
            get
            {
                return day;
            }
            set
            {
                if (value != day)
                {
                    day = value;
                    OnPropertyChanged(nameof(Day));
                }
            }
        }

        private string city;
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                if (value != city)
                {
                    city = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        private string country;
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                if (value != country)
                {
                    country = value;
                    OnPropertyChanged(nameof(Country));
                }
            }
        }

        private string accommodationName;
        public string AccommodationName
        {
            get
            {
                return accommodationName;
            }
            set
            {
                if (value != accommodationName)
                {
                    accommodationName = value;
                    OnPropertyChanged(nameof(AccommodationName));
                }
            }
        }

        private Domain.Model.Type selectedType;

        public Domain.Model.Type SelectedType
        {
            get
            {
                return selectedType;
            }
            set
            {
                if (value != selectedType)
                {
                    selectedType = value;
                    OnPropertyChanged(nameof(SelectedType));
                }
            }
        }
        public MainPageViewModel(NavigationService navigationService, User user) {
            NavigationService = navigationService;
            this.user = user;
            SearchCommand = new MyICommand(ExecuteSearch);
            ResetCommand = new MyICommand(ExecuteReset);
            AccommodationViewNavigationCommand = new MyICommand<AccommodationDto>(ExecuteNavigationToAccommodationView);
            Types = Enum.GetValues(typeof(Domain.Model.Type)).OfType<Domain.Model.Type>().ToList();
            SelectedAccommodation = new AccommodationDto();
            ImageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            AccommodationList = new ObservableCollection<AccommodationDto>();
            AccommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(),
                new LocationService(Injector.CreateInstance<ILocationRepository>()),ImageService);
            SetValues();
            Update();
        }
        private void SetValues()
        {
            AccommodationName = "";
            City = "";
            Country = "";
            Day = 0;
            GuestNumber = 0;
            SelectedType = Domain.Model.Type.Any;
        }
        public void Update()
        {
            AccommodationList.Clear();
            List<Accommodation>accommodations = AccommodationService.GetAll();
            foreach (Accommodation accommodation in accommodations)
            {
                Location? location = AccommodationService.GetLocationById(accommodation.LocationId);
                Image? image = ImageService.GetFirstByEntityAndType(accommodation.Id, ResourceType.Accommodation);
                AccommodationList.Add(new AccommodationDto(accommodation, location, image));
            }
        }
        public void ExecuteReset() {
            SetValues();
            Update();
        }
        public void ExecuteSearch() {
            AccommodationList.Clear();
            AccommodationSearchCriteria accommodationSearchCriteria = new AccommodationSearchCriteria(AccommodationName, City, Country,GuestNumber ,Day,SelectedType);
            List<Accommodation> accommodations = AccommodationService.GetAll();
            foreach (Accommodation accommodation in accommodations)
            {
                Location? location = AccommodationService.GetLocationById(accommodation.LocationId);
                Image? image = ImageService.GetFirstByEntityAndType(accommodation.Id, ResourceType.Accommodation);
                if (accommodationSearchCriteria.IsSearched(accommodation, location))
                {
                    AccommodationList.Add(new AccommodationDto(accommodation, location, image));
                }
            }
        }
        private void ExecuteNavigationToAccommodationView(AccommodationDto accommodationDto) {
            NavigationService.Navigate(new AccommodationView(accommodationDto, user,NavigationService));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
