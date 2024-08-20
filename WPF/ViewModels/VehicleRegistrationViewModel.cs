using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels
{
    public class VehicleRegistrationViewModel : IObserver, INotifyPropertyChanged
    {
        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<CheckBox> locations;
        public List<CheckBox> Locations
        {
            get { return locations; }
            set
            {
                if (locations != value)
                {
                    locations = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private List<CheckBox> languages;
        public List<CheckBox> Languages
        {
            get { return languages; }
            set
            {
                if (languages != value)
                {
                    languages = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private VehicleService vehicleService;
        private VehicleLanguageService vehicleLanguageService;
        private VehicleLocationService vehicleLocationService;
        private LocationService locationService;
        private LanguageService languageService;
        private ImageService imageService;
        public NavigationService NavigationService { get; set; }

        public VehicleDto VehicleDTO { get; set; }
        private List<CheckBox> checkedLocations;
        private List<CheckBox> checkedLanguages;

        public VehicleRegistrationViewModel(int userId, NavigationService navigationService)
        {
            VehicleDTO = new VehicleDto();
            VehicleDTO.DriverId = userId;

            vehicleService = new VehicleService(Injector.CreateInstance<IVehicleRepository>());
            vehicleLocationService = new VehicleLocationService(Injector.CreateInstance<IVehicleLocationsRepository>(),
                new AddressService(Injector.CreateInstance<IAddressRepository>()), vehicleService);
            vehicleLanguageService = new VehicleLanguageService(Injector.CreateInstance<IVehicleLanguageRepository>());
            this.NavigationService = navigationService;

            locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            Locations = new List<CheckBox>();
            checkedLocations = new List<CheckBox>();

            languageService = new LanguageService(Injector.CreateInstance<ILanguageRepository>());
            Languages = new List<CheckBox>();
            checkedLanguages = new List<CheckBox>();

            this.imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            ImagePath = "../../../Resources/Images/";
            Update();
        }

        public void Update()
        {
            LoadLocations();
            LoadLanguages();
        }

        private void LoadLocations()
        {
            foreach (Location location in locationService.GetListOfAll())
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = location.City + "," + location.Country;
                Locations.Add(checkBox);
                checkedLocations.Add(checkBox);
            }
        }

        private void LoadLanguages()
        {
            foreach (Language language in languageService.GetAll())
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = language.Name;
                Languages.Add(checkBox);
                checkedLanguages.Add(checkBox);
            }
        }

        public bool Register()
        {
            List<Domain.Model.Image>? images = imageService.GetAll();
            if (!images.Any(x => x.EntityId == vehicleService.GetNextId() && x.ResourceType == ResourceType.Vehicle))
            {
                MessageBox.Show("Add at least one image to register.");
                return false;
            }
            AddRemainingAtributes();
            vehicleService.Add(VehicleDTO.ToVehicle());
            var myMessage = new NotificationMessage("change");
            Messenger.Default.Send(myMessage);
            return true;
        }

        private void AddRemainingAtributes()
        {
            foreach (CheckBox checkBox in checkedLocations)
            {
                if (checkBox.IsChecked == true)
                {
                    vehicleLocationService.Add(vehicleService.GetNextId(), checkedLocations.IndexOf(checkBox) + 1);
                }
            }

            foreach (CheckBox checkBox in checkedLanguages)
            {
                if (checkBox.IsChecked == true)
                {
                    vehicleLanguageService.Add(vehicleService.GetNextId(), checkedLanguages.IndexOf(checkBox) + 1);
                }
            }
        }

        public void AddImage()
        {
            if (!CheckImageFile()) { return; }
            Domain.Model.Image image = new BookingApp.Domain.Model.Image();
            image.Path = ImagePath;
            image.EntityId = vehicleService.GetNextId();
            image.ResourceType = ResourceType.Vehicle;
            imageService.Add(image);
            MessageBox.Show("Image added.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private bool CheckImageFile()
        {
            if (!File.Exists(ImagePath) || ImagePath == "../../../Resources/Images/")
            {
                MessageBox.Show("Image path | " + ImagePath + " | does not exist.", "Missing Path", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            var image = imageService.GetByPath(ImagePath);
            if (image != null)
            {
                MessageBox.Show("Image already added.", "Path error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
