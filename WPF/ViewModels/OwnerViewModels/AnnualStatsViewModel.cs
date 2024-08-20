using BookingApp.Commands;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class AnnualStatsViewModel : ViewModelBase
    {
        private readonly int year;
        private readonly AccommodationViewModel accommodation;

        private AccommodationReservationService reservationService;
        private ReservationChangeRequestService requestService;
        private RenovateSuggestionService renovationService;

        public string ImagePath { get; set; }
        public string AccommodationName { get; set; }
        public AnnualDataViewModel AnnualDataViewModel { get; set; }

        public ICommand OverallStats { get; }

        public AnnualStatsViewModel(NavigationStore navigationStore, int year, AccommodationViewModel accommodation)
        {
            this.year = year;
            this.accommodation = accommodation;

            reservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>());
            requestService = new ReservationChangeRequestService(Injector.CreateInstance<IReservationChangeRequestRepository>());
            renovationService = new RenovateSuggestionService(Injector.CreateInstance<IRenovateSuggestionRepository>());

            ImagePath = accommodation.ImagePath;
            AccommodationName = accommodation.Name;
            AnnualDataViewModel = new AnnualDataViewModel();

            OverallStats = new NavigationCommand<OverallStatsViewModel>(navigationStore,
                () => new OverallStatsViewModel(navigationStore, accommodation));

            Load();
        }

        private void Load()
        {
            foreach (var reservation in reservationService.GetReservationsByAccommodationIdAndYear(accommodation.Id, year))
            {
                int requests = requestService.RequestsCountByReservationId(reservation.Id);
                int suggestions = renovationService.GetRenovationSuggestionCount(reservation.Id);
                AnnualDataViewModel.AddData(reservation, requests, suggestions);
            }

            AnnualDataViewModel.Sort();
            AnnualDataViewModel.CalculateOccupancyRate();
            AnnualDataViewModel.CalculateHighestAndLowestRate();
        }
    }
}
