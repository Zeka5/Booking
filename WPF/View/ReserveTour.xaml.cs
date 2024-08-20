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
using System.Windows.Shapes;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for ReserveTour.xaml
    /// </summary>
    public partial class ReserveTour : Window, IObserver
    {
        public LocationRepository LocationRepository { get; set; }
        public LanguageRepository LanguageRepository { get; set; }
        public TourRepository TourRepository { get; set; }
        public TourRealizationRepository TourStartTimeRepository { get; set; }
        public TourReservationRepository TourReservationRepository { get; set; }
        public ObservableCollection<TourDto> TourList { get; set; }
        public int SelectedTourRealizationId { get; set; }
        public int SelectedVoucherId { get; set; }
        public int TourGuestsCount { get; set; }
        public ReserveTour(int selectedTourStartTimeId, int selectedVoucherId = -1)
        {
            InitializeComponent();
            DataContext = this;

            SelectedVoucherId = selectedVoucherId;
            TourList = new ObservableCollection<TourDto>();
            TourReservationRepository = new TourReservationRepository();
            TourRepository = new TourRepository();
            LocationRepository = new LocationRepository();
            LanguageRepository = new LanguageRepository();
            TourStartTimeRepository = new TourRealizationRepository();
            TourRepository.Subscribe(this);

            SelectedTourRealizationId = selectedTourStartTimeId;
            Update();
        }
        public void Update()
        {
            TourList.Clear();
            foreach (Tour tour in TourRepository.GetAll())
            {
                Location location = LocationRepository.GetById(tour.LocationId);
                Language language = LanguageRepository.GetById(tour.LanguageId);
                TourList.Add(new TourDto(tour,location,language));
            }
        }
        private void ConfirmTourGuestsNumberClick(object sender, RoutedEventArgs e)
        {
            if(TourGuestsCount <= TourStartTimeRepository.GetById(SelectedTourRealizationId).Availability)
            {
                Domain.Model.TourRealization tourStart = TourStartTimeRepository.GetById(SelectedTourRealizationId);

                int availablePlacesLeft = tourStart.Availability - TourGuestsCount;
                tourStart.Availability = availablePlacesLeft;

                AddTourGuestToTour addTourGuestToTour = new AddTourGuestToTour(TourGuestsCount, SelectedTourRealizationId, SelectedVoucherId);
                addTourGuestToTour.ShowDialog();
                Close();


                TourStartTimeRepository.Update(tourStart);
            }
            else
            {
                MessageBox.Show("You have exceeded the maximum number of available seats.", "Warning!");
            }

        }
    }
}
