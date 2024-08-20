using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class ScheduleRenovationViewModel : ViewModelBase
    {
        private AccommodationReservationService accommodationReservationService;
        private RenovationService renovationService;

        public RenovationViewModel RenovationViewModel { get; set; }

        //region
        #region IZDVOJITI U POSEBAN VM
        private Visibility datePicker = Visibility.Hidden;
        public Visibility DatePicker
        {
            get => datePicker;
            set
            {
                if (datePicker != value)
                {
                    datePicker = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime rangeStart;
        public DateTime RangeStart
        {
            get => rangeStart;
            set
            {
                if (rangeStart != value)
                {
                    rangeStart = value;
                    SelectedDate = rangeStart;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime rangeEnd;
        public DateTime RangeEnd
        {
            get => rangeEnd;
            set
            {
                if (rangeEnd != value)
                {
                    rangeEnd = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get => selectedDate;
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
        //
        public int OwnerId { get; }

        public ICommand FindDates { get; }
        public ICommand ConfirmRenovation { get; }
        public ICommand Cancel { get; }

        public ScheduleRenovationViewModel(NavigationStore navigationStore, AccommodationViewModel accommodation)
        {
            accommodationReservationService = new AccommodationReservationService(
                Injector.CreateInstance<IAccommodationReservationRepository>());
            renovationService = new RenovationService(Injector.CreateInstance<IRenovationRepository>());

            RenovationViewModel = new RenovationViewModel();
            RenovationViewModel.AccommodationId = accommodation.Id;
            RenovationViewModel.RangeStart = DateTime.Today;
            RenovationViewModel.RangeEnd = DateTime.Today;

            OwnerId = accommodation.OwnerId;

            FindDates = new FindFreeDatesCommand(this);
            ConfirmRenovation = new ConfirmRenovationCommand(navigationStore, this);
            Cancel = new NavigationCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore, OwnerId));
        }

        public bool IsDateFree(DateTime date, int accommodationId)
        {
            return accommodationReservationService.IsDateFree(date, accommodationId);
        }

        public void AddRenovation(Renovation renovation)
        {
            renovationService.AddRenovation(renovation);
        }
    }
}
