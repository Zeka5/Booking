using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.WPF.ViewModels
{
    public class BreakRequestViewModel : INotifyPropertyChanged
    {
        private DateTime selectedDateUntil;
        public DateTime SelectedDateUntil
        {
            get { return selectedDateUntil; }
            set
            {
                if (selectedDateUntil != value)
                {
                    selectedDateUntil = value;
                    OnPropertyChanged();
                }
            }
        }
        private DateTime selectedDateFrom;
        public DateTime SelectedDateFrom
        {
            get { return selectedDateFrom; }
            set
            {
                if (selectedDateFrom != value)
                {
                    selectedDateFrom = value;
                    OnPropertyChanged();
                }
                if (selectedDateUntil <= selectedDateFrom)
                {
                    SelectedDateUntil = selectedDateFrom.AddDays(1);
                }
            }
        }

        private DriveReservationService driveReservationService;
        private VehicleLocationService vehicleLocationService;
        private UserService userService;

        private int id;

        public BreakRequestViewModel(int id)
        {
            userService = new UserService(Injector.CreateInstance<IUserRepository>());
            vehicleLocationService = new VehicleLocationService(Injector.CreateInstance<IVehicleLocationsRepository>(),
                new AddressService(Injector.CreateInstance<IAddressRepository>()), new VehicleService(Injector.CreateInstance<IVehicleRepository>()));
            driveReservationService = new DriveReservationService(Injector.CreateInstance<IDriveReservationRepository>(),
                        vehicleLocationService, userService);
            SelectedDateFrom = DateTime.Now;
            SelectedDateUntil = DateTime.Now;
            this.id = id;
        }

        public bool RequestBreak()
        {
            if ((SelectedDateFrom - DateTime.Now).TotalHours <= 48)
            {
                return RequestUrgentBreak();
            }

            MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to request a break\nfrom "
                + SelectedDateFrom.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                + "\nuntil " + SelectedDateUntil.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                + "?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);
            if (dialogResult == MessageBoxResult.Cancel)
            {
                return false;
            }

            driveReservationService.MakeBreakRequest(id, SelectedDateFrom, SelectedDateUntil);
            return true;
        }

        private bool RequestUrgentBreak()
        {
            MessageBoxResult urgentResult = MessageBox.Show("Are you sure you want to request an \bURGENT\b break\nfrom "
                + SelectedDateFrom.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                + "\nuntil " + SelectedDateUntil.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                + "?", "Confirmation", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (urgentResult == MessageBoxResult.Cancel)
            {
                return false;
            }

            driveReservationService.MakeUrgentBreakRequest(id, SelectedDateFrom, SelectedDateUntil);
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
