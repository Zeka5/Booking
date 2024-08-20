using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Xml.Linq;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for AddTourGuestToTour.xaml
    /// </summary>
    public partial class AddTourGuestToTour : Window, IObserver, INotifyPropertyChanged
    {
        public TourGuestDto TourGuestDto { get; set; }
        public TourGuestRepository TourGuestRepository { get; set; }
        public TourReservationRepository TourReservationRepository { get; set; }
        public TourRealizationRepository TourStartTimeRepository { get; set; }
        public int TourGuestCount {  get; set; }
        public int AvailablePlaces { get; set; }
        public int SelectedTourRealizationId { get; set; }
        public int SelectedVoucherId { get; set; }

        public int currentTourGuest;
        public int CurrentTourGuest
        {
            get { return currentTourGuest; }
            set
            {
                if (value != currentTourGuest)
                {
                    currentTourGuest = value;
                    OnPropertyChanged("CurrentTourGuest");
                }
            }
        }
        private bool isReservationAdded;
        public AddTourGuestToTour(int tourGuestCount,  int selectedTourStartTimeId, int selectedVoucherId)
        {
            InitializeComponent();
            DataContext = this;

            TourGuestDto = new TourGuestDto();
            TourGuestRepository = new TourGuestRepository();
            TourReservationRepository = new TourReservationRepository();
            TourStartTimeRepository = new TourRealizationRepository();

            CurrentTourGuest = 1;
            isReservationAdded = false;
            TourGuestCount = tourGuestCount;
            SelectedVoucherId = selectedVoucherId;
            AvailablePlaces = TourStartTimeRepository.GetById(selectedTourStartTimeId).Availability;
            SelectedTourRealizationId = selectedTourStartTimeId;

            Update();
        }

        public void Update()
        {
        }
        private void ConfirmNewTourGuest(object sender, RoutedEventArgs e)
        {
            if (CurrentTourGuest != TourGuestCount)
            {
                if (!isReservationAdded)
                {
                    AddTourReservation();
                    isReservationAdded = true;
                }

                AddTourGuest();
                CurrentTourGuest++;
                AvailablePlaces--;

                ClearInputFields();
            }
            else
            {
                if (!isReservationAdded)
                {
                    AddTourReservation();
                    isReservationAdded = true;
                }
                AddTourGuest();
                AvailablePlaces--;
                MessageBox.Show("You have entered information about all tourists!\n" +
                                AvailablePlaces + " places left!");
                this.Close();
            }
        }
        private void AddTourGuest()
        {
            BookingApp.Domain.Model.TourGuest tourGuest = new BookingApp.Domain.Model.TourGuest(TourGuestDto.FullName, TourGuestDto.Years, TourReservationRepository.GetLast().Id);
            TourGuestRepository.Add(tourGuest);
        }
        private void AddTourReservation()
        {
            TourReservation tourReservation = new TourReservation(SignInForm.curretnUserId, SelectedTourRealizationId, TourGuestCount);
            TourReservationRepository.Add(tourReservation);

            if(SelectedVoucherId == -1)
            {
                return;
            }

            ConfirmVoucherUsage();
        }
        private void ConfirmVoucherUsage()
        {
            VoucherRepository voucherRepository = new VoucherRepository();
            Voucher voucher = voucherRepository.GetById(SelectedVoucherId);
            voucher.Status = ValidityStatus.USED;
            voucher.TourReservationId = TourReservationRepository.GetAll().Last().Id;
            voucherRepository.Update(voucher);
        }
        private void ClearInputFields()
        {
            fullNameInput.Text = string.Empty;
            yearsInput.Text = string.Empty;
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
