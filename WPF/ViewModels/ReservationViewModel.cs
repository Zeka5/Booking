using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.Services;
using BookingApp.Utilities;
using BookingApp.View.Validation;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels
{
    public class ReservationViewModel : INotifyPropertyChanged
    {
        public AccommodationReservationService AccommodationReservationService { get; set; }
        public GuestService GuestService { get; set; }
        public AccommodationDto AccommodationDto { get; set; }
        public ReservationDate SelectedReservation { get; set; }
        public AccommodationReservationDto AccommodationReservationDto { get; set; }
        public MyICommand<Grid> InsertValuesCommand { get; set; }
        public MyICommand ReserveCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        public NavigationService NavigationService { get; set; }
        private bool messageBoxFlag;
        private User user;
        private ObservableCollection<ReservationDate> reservationOptions;
        public ObservableCollection<ReservationDate> ReservationOptions
        {
            get { return reservationOptions; }
            set
            {
                reservationOptions = value;
                OnPropertyChanged(nameof(ReservationOptions));
            }
        }
        private int daysToStay;
        public int DaysToStay
        {
            get
            {
                return daysToStay;
            }
            set
            {
                if (value != daysToStay)
                {
                    daysToStay = value;
                    OnPropertyChanged(nameof(DaysToStay));

                }
            }
        }

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

        private DateTime rangeBegin;
        public DateTime RangeBegin
        {
            get
            {
                return rangeBegin;
            }
            set
            {
                if (value != rangeBegin)
                {
                    rangeBegin = value;
                    OnPropertyChanged(nameof(RangeBegin));
                }
            }
        }

        private DateTime rangeEnd;
        public DateTime RangeEnd
        {
            get
            {
                return rangeEnd;
            }
            set
            {
                if (value != rangeEnd)
                {
                    rangeEnd = value;
                    OnPropertyChanged(nameof(RangeEnd));
                }
            }
        }
        public ReservationViewModel(AccommodationDto accommodationDto, User user, NavigationService navigationService)
        {
            NavigationService = navigationService;
            InsertValuesCommand = new MyICommand<Grid>(ExecuteValuesInsertion);
            ReserveCommand = new MyICommand(ExecuteReservation);
            CancelCommand = new MyICommand(ExecuteCancelation);
            LoadServices();
            ReservationOptions = new ObservableCollection<ReservationDate>();
            AccommodationReservationDto = new AccommodationReservationDto();
            AccommodationDto = accommodationDto;
            this.user = user;
            RangeBegin = DateTime.Today;
            RangeEnd = DateTime.Today.AddDays(1);           
        }
        private void LoadServices()
        {
            AccommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>(),
                new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(), new LocationService(Injector.CreateInstance<ILocationRepository>()), new ImageService(Injector.CreateInstance<IImageRepository>())));
            GuestService = new GuestService(Injector.CreateInstance<IGuestRepository>());
        }
        public void Update()
        {
            AccommodationReservationService.DaysToStay = DaysToStay;
            AccommodationReservationService.AccommodationId = AccommodationDto.Id;
            List<DateTime> freeDates = AccommodationReservationService.GetFreeDates(RangeBegin, RangeEnd);
            var value = AccommodationReservationService.GetReservationOptions(freeDates, RangeBegin, RangeEnd);
            List<ReservationDate> reservationDates = value.Item1;
            ReservationOptions.Clear();
            foreach(ReservationDate reservationDate in reservationDates)
            {
                ReservationOptions.Add(reservationDate);
            }
            messageBoxFlag = value.Item2;
            if (messageBoxFlag)
            {
                MessageBox.Show("There are no available dates for your search here are nearest options");
            }
        } 
        public void ExecuteReservation() {
            if (!UpdateAccommodationReservationDto())
            {
                MessageBox.Show("Please select the date ");
                return ;
            }
            AccommodationReservation accommodationReservation = AccommodationReservationDto.ToAccommodationReservation();
            AccommodationReservationService.Add(accommodationReservation);
            GuestService.RemoveBonusPoint(user.Id);
            MessageBox.Show("Your reservation is succesfully saved");
            NavigationService.Navigate(new MainPage(user,NavigationService));
        }
        private bool UpdateAccommodationReservationDto()
        {
            if (SelectedReservation == null) return false;
            AccommodationReservationDto.FirstDay = SelectedReservation.FirstDay.ToString("dd/MM/yyyy");
            AccommodationReservationDto.LastDay = SelectedReservation.LastDay.ToString("dd/MM/yyyy");
            AccommodationReservationDto.DaysToStay = DaysToStay;
            AccommodationReservationDto.AccommodationId = AccommodationDto.Id;
            AccommodationReservationDto.GuestNumber = GuestNumber;
            AccommodationReservationDto.UserId = user.Id;
            AccommodationReservationDto.ReservationStatus = ReservationStatus.Active;
            return true;
        }
        public bool AreConditionsMet()
        {
            if (RangeBegin.AddDays(DaysToStay - 1) > RangeEnd)
            {
                MessageBox.Show("Your range is lower than number of days that you want to reserve, please try again");
                return false;
            }
            return true;
        }
        private void ExecuteValuesInsertion(Grid grid) {
            if (!AreConditionsMet()) return;
            Update();
            grid.Visibility = Visibility.Visible;
        }
        private void ExecuteCancelation() {
            NavigationService.Navigate(new MainPage(user, NavigationService));
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
