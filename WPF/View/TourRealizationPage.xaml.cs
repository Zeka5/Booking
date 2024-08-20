using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingApp.WPF.View
{
    /// <summary>
    /// Interaction logic for TourRealiztionPage.xaml
    /// </summary>
    public partial class TourRealizationPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<TourRealizationDto> TourRealizations { get; set; }
        public TourRealizationRepository TourStartTimeRepository { get; set; }
        public TourReservation TourReservation { get; set; }
        public TourReservationRepository TourReservationRepository { get; set; }
        public VoucherRepository VoucherRepository { get; set; }
        public TourGuestRepository TourGuestRepository { get; set; }
        public TourRealizationDto? SelectedTourRealization { get; set; }
        public TourGuestDto TourGuestDto { get; set; }
        public TourDto TourDto { get; set; }
        public List<BookingApp.Domain.Model.TourGuest> TempTourGuestList { get; set; }
        public int SelectedTourId { get; set; }
        public bool HasVoucher {  get; set; }   // tek kada se unesu svi tourGuestovi, vaucer ze biti iskoristen (da se ne bi iskoristio ranije, a da se otkaze u medjuvr)

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

        public int numberOfTourGuests;
        public int NumberOfTourGuests
        {
            get { return numberOfTourGuests; }
            set
            {
                if (value != numberOfTourGuests)
                {
                    numberOfTourGuests = value;
                    OnPropertyChanged("NumberOfTourGuests");
                }
            }
        }
        public string ImagePath { get; set; }
        public TourRealizationPage(TourDto selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            NumberOfTourGuests = 1;
            HasVoucher = false;
            CurrentTourGuest = 1;

            TourRealizations = new ObservableCollection<TourRealizationDto>();
            TourStartTimeRepository = new TourRealizationRepository();
            TourReservation = new TourReservation();
            TourReservationRepository = new TourReservationRepository();
            VoucherRepository = new VoucherRepository();
            SelectedTourId = selectedTour.Id;
            TempTourGuestList = new List<BookingApp.Domain.Model.TourGuest>();
            TourGuestRepository = new TourGuestRepository();
            TourGuestDto = new TourGuestDto();
            TourDto = selectedTour;

            TourGuestDto.FullName = "Full name";

            if (VoucherRepository.GetValidVouchersByUserId(SignInForm.curretnUserId).Count > 0)
            {
                VoucherCheckbox.Visibility = Visibility.Visible;
            }

            Update();
        }


        public void Update()
        {
            TourRealizations.Clear();
            foreach (Domain.Model.TourRealization tourRealization in TourStartTimeRepository.GetAll())
            {
                if (SelectedTourId == tourRealization.TourId && tourRealization.TourState == State.None)
                {
                    AddTourStartTime(tourRealization);
                }
            }
        }
        private void AddTourStartTime(Domain.Model.TourRealization tourRealization)
        {
            if (tourRealization.Availability > 0)
            {
                TourRealizations.Add(new TourRealizationDto(tourRealization));
            }
        }
        private void ConfirmStartDateClick(object sender, RoutedEventArgs e)
        {
            if (SelectedTourRealization == null)
            {
                MessageBox.Show("Select a Tour start time!");
                return;
            }

            if (TourReservationRepository.GetByTourRelizationIdAndUserId(SelectedTourRealization.Id, SignInForm.curretnUserId) != null)
            {
                MessageBox.Show("The current user has already reserved this tour at this time!");
                return;
            }

            ConfrimDateButton.Visibility = Visibility.Hidden;
            TourGuestInfoGrid.Visibility = Visibility.Visible;
        }
        private void ConfirmTourGuestsNumber()
        {
            Domain.Model.TourRealization tourRealization = TourStartTimeRepository.GetById(SelectedTourRealization.Id);

            int availablePlacesLeft = tourRealization.Availability - NumberOfTourGuests;
            tourRealization.Availability = availablePlacesLeft;
            TourStartTimeRepository.Update(tourRealization);
        }
        private void ConfirmNewTourGuest(object sender, RoutedEventArgs e)
        {
            if (CurrentTourGuest != NumberOfTourGuests )
            {
                TempTourGuestList.Add(new BookingApp.Domain.Model.TourGuest(TourGuestDto.FullName, TourGuestDto.Years));
                CurrentTourGuest++;

                ClearInputFields();
            }
            else
            {
                TempTourGuestList.Add(new BookingApp.Domain.Model.TourGuest(TourGuestDto.FullName, TourGuestDto.Years));

                AddTourReservation();
                AddTourGuests();
                ConfirmTourGuestsNumber();

                MessageBox.Show("Successfuly reserved!");

                NavigationService.Navigate(new Tours());
            }
        }
        private void AddTourGuests()
        {
            foreach(BookingApp.Domain.Model.TourGuest tourGuest in TempTourGuestList)
            {
                tourGuest.TourReservationId = TourReservationRepository.GetLast().Id;
                TourGuestRepository.Add(tourGuest);
            }
        }
        private void AddTourReservation()
        {
            TourReservation tourReservation = new TourReservation(SignInForm.curretnUserId, SelectedTourRealization.Id, NumberOfTourGuests);
            TourReservationRepository.Add(tourReservation);

            AcquirePotentiallVoucher();

            if (HasVoucher)
            {
                UseVoucher();
            }
        }
        private void AcquirePotentiallVoucher()
        {
            int reservationCount = 0;

            List<TourReservation> tempTourReservations = new List<TourReservation>();

            foreach (var tourReservation  in TourReservationRepository.GetAll().Where(x => x.UserId == SignInForm.curretnUserId))
            {
                if(TourStartTimeRepository.GetById(tourReservation.TourRealizationId).StartTime > DateTime.Now.AddDays(-365) 
                    && TourStartTimeRepository.GetById(tourReservation.TourRealizationId).TourState == State.Finished)
                {
                    reservationCount ++;
                    tempTourReservations.Add(tourReservation);
                }

                if (reservationCount == 5)
                {
                    VoucherRepository.Add(new Voucher(SignInForm.curretnUserId, 6));
                    UpdateAcquiredVoucherTourReservation(tempTourReservations);

                    break;
                }
            }

        }
        private void UpdateAcquiredVoucherTourReservation(List<TourReservation> tourReservations)
        {
            foreach(var tourReservation in tourReservations)
            {
                tourReservation.AcquiredVoucher = true;
                TourReservationRepository.Update(tourReservation);
            }
        }
        private void UseVoucher()
        {
            VoucherRepository voucherRepository = new VoucherRepository();
            Voucher voucher = voucherRepository.GetAll().FirstOrDefault();
            voucher.Status = ValidityStatus.USED;
            voucher.TourReservationId = TourReservationRepository.GetAll().Last().Id;
            voucherRepository.Update(voucher);
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void ClearInputFields()
        {
            NameTextBox.Text = "";
            AgeTextBox.Text = "";
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void DecreaseNumberOfTourGuestsCLick(object sender, RoutedEventArgs e)
        {
            if(NumberOfTourGuests == 1)
            {
                return;
            }
            NumberOfTourGuests--;
        }
        public void IncreaseNumberOfTourGuestsCLick(object sender, RoutedEventArgs e)
        {
            if (NumberOfTourGuests >= SelectedTourRealization.Availability)
            {
                MessageBox.Show(SelectedTourRealization.Availability.ToString() + " are available!");
                return;
            }
            NumberOfTourGuests++;
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }
    }
}
