using BookingApp.Commands;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class OverallStatsViewModel : ViewModelBase
    {
        private readonly int accommodationId;

        private AccommodationReservationService reservationService;
        private ReservationChangeRequestService requestService;
        private RenovateSuggestionService renovationService;

        public string ImagePath { get; set; }
        public string AccommodationName { get; set; }
        public OverallDataViewModel OverallDataViewModel { get; set; }

        public ICommand Home { get; }
        public ICommand MonthlyStats { get; }

        public OverallStatsViewModel(NavigationStore navigationStore, AccommodationViewModel accommodation)
        {
            accommodationId = accommodation.Id;

            reservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>());
            requestService = new ReservationChangeRequestService(Injector.CreateInstance<IReservationChangeRequestRepository>());
            renovationService = new RenovateSuggestionService(Injector.CreateInstance<IRenovateSuggestionRepository>());

            ImagePath = accommodation.ImagePath;
            AccommodationName = accommodation.Name;
            OverallDataViewModel = new OverallDataViewModel();

            Home = new NavigationCommand<HomeViewModel>(navigationStore, () => new HomeViewModel(navigationStore, accommodation.OwnerId));
            MonthlyStats = new OpenAnnualStatsCommand(navigationStore, accommodation);

            Load();
        }

        private void Load()
        {
            foreach (var reservation in reservationService.GetReservationsByAccommodationId(accommodationId))
            {
                int requests = requestService.RequestsCountByReservationId(reservation.Id);
                int suggestions = renovationService.GetRenovationSuggestionCount(reservation.Id);
                OverallDataViewModel.AddData(reservation, requests, suggestions);
            }

            OverallDataViewModel.Sort();
            OverallDataViewModel.CalculateOccupancyRate();
            OverallDataViewModel.CalculateHighestAndLowestRate();
        }
    }
}
