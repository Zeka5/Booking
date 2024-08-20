using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace BookingApp.View
{
    /// <summary>
    /// Interaction logic for VehicleRegistration.xaml
    /// </summary>
    public partial class VehicleRegistration : Window, IObserver, INotifyPropertyChanged
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
        //public VehicleRegistrationViewModel VehicleRegistrationViewModel { get; set; }
        private VehicleRepository vehicleRepository;
        public VehicleDto VehicleDTO { get; set; }

        private VehicleLocationsRepository vehicleLocationsRepository;
        private LocationRepository locationRepository;
        private List<CheckBox> checkedLocations;

        private VehicleLanguageRepository vehicleLanguageRepository;
        private LanguageRepository languageRepository;
        private List<CheckBox> languages;

        private ImageRepository imageRepository;
        public VehicleRegistration(int userId)
        {
            InitializeComponent();
            DataContext = this;

            VehicleDTO = new VehicleDto();
            VehicleDTO.DriverId = userId;

            vehicleRepository = new VehicleRepository();
            vehicleLocationsRepository = new VehicleLocationsRepository();
            vehicleLanguageRepository = new VehicleLanguageRepository();

            locationRepository = new LocationRepository();
            Locations = new List<CheckBox>();
            checkedLocations = new List<CheckBox>();
            LoadLocations();

            languageRepository = new LanguageRepository();
            languages = new List<CheckBox>();
            LoadLanguages();

            imageRepository = new ImageRepository();
            ImagePath = "../../../Resources/Images/";

            //DODACU DELETE IMAGE ~Zeka
        }

        private void LoadLocations()
        {
            foreach (Location location in locationRepository.GetAll())
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = location.City + "," + location.Country;
                Locations.Add(checkBox);
                checkedLocations.Add(checkBox);
            }
        }

        private void LoadLanguages()
        {
            foreach(Language language in languageRepository.GetAll())
            {
                CheckBox checkBox = new CheckBox();
                checkBox.Content = language.Name;
                languages.Add(checkBox);
                languageCheckBox.Items.Add(checkBox);
            }
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            List<Domain.Model.Image>? images = imageRepository.GetAll();
            if (!images.Any(x => x.EntityId == vehicleRepository.GetNextId() && x.ResourceType == ResourceType.Vehicle))
            {
                MessageBox.Show("Add at least one image to register.");
                return;
            }
            AddRemainingAtributes();
            vehicleRepository.Add(VehicleDTO.ToVehicle());
            var myMessage = new NotificationMessage("change");
            Messenger.Default.Send(myMessage);
            Close();
        }

        private void AddRemainingAtributes()
        {
            foreach (CheckBox checkBox in checkedLocations)
            {
                if (checkBox.IsChecked == true)
                {
                    vehicleLocationsRepository.Add(vehicleRepository.GetNextId(), checkedLocations.IndexOf(checkBox) + 1);
                }
            }

            foreach (CheckBox checkBox in languages)
            {
                if (checkBox.IsChecked == true)
                {
                    vehicleLanguageRepository.Add(vehicleRepository.GetNextId(), languages.IndexOf(checkBox) + 1);
                }
            }
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            if (!CheckImageFile()) { return; }
            Domain.Model.Image image = new BookingApp.Domain.Model.Image();
            image.Path = ImagePath;
            image.EntityId = vehicleRepository.GetNextId();
            image.ResourceType = ResourceType.Vehicle;
            imageRepository.Add(image);
            MessageBox.Show("Image added.");
        }

        private bool CheckImageFile()
        {
            if (!File.Exists(ImagePath) || ImagePath == "../../../Resources/Images/")
            {
                MessageBox.Show("Image path | " + ImagePath + " | does not exist.");
                return false;
            }
            if (imageRepository.GetByPath(ImagePath) != null)
            {
                MessageBox.Show("Image already added.");
                return false;
            }
            return true;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
