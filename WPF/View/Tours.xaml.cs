using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Tours.xaml
    /// </summary>
    public partial class Tours : Page, IObserver
    {
        public static ObservableCollection<TourDto> TourList { get; set; }
        public LanguageRepository LanguageRepository { get; set; }
        public LocationRepository LocationRepository { get; set; }
        public ImageRepository ImageRepository { get; set; }
        private TourRepository TourRepository { get; set; }
        public TourDto? SelectedTour { get; set; }
        public Tours()
        {
            InitializeComponent();
            DataContext = this;

            TourList = new ObservableCollection<TourDto>();
            TourRepository = new TourRepository();
            LanguageRepository = new LanguageRepository();
            LocationRepository = new LocationRepository();
            ImageRepository =  new ImageRepository();
            TourRepository.Subscribe(this);

            Update();
        }
        public void Update()
        {
            TourList.Clear();
            foreach (Tour tour in TourRepository.GetAll())
            {
                Location location = LocationRepository.GetById(tour.LocationId);
                Language language = LanguageRepository.GetById(tour.LanguageId);
                string imagePath = ImageRepository.GetByEntityAndType(tour.Id, ResourceType.Tour).FirstOrDefault().Path;
                TourList.Add(new TourDto(tour, location, language, imagePath));
            }
        }
        private void ReserveTourClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTour != null)
            {
                NavigationService.Navigate(new TourRealizationPage(SelectedTour));
            }
            else
            {
                MessageBox.Show("Select a tour!");
            }
        }
    }

}
