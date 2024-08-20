using BookingApp.Commands;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Services;
using BookingApp.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class MainViewModel : ViewModelBase, IObserver
    {
        private readonly NavigationStore navigationStore;

        public ViewModelBase CurrentViewModel => navigationStore.CurrentViewModel;

        private AccommodationRatingService accommodationRatingService;
        public SuperOwnerViewModel SuperOwner { get; set; }

        public ICommand Home { get; }
        public ICommand ModifyReservations { get; }
        public ICommand Reviews { get; }
        public ICommand Renovations { get; }

        public MainViewModel(NavigationStore navigationStore, int ownerId)
        {
            this.navigationStore = navigationStore;

            accommodationRatingService = new AccommodationRatingService(
                Injector.CreateInstance<IAccommodationRatingRepository>(),
                new AccommodationReservationService(
                    Injector.CreateInstance<IAccommodationReservationRepository>(),
                    new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(),
                        new LocationService(Injector.CreateInstance<ILocationRepository>()),
                        new ImageService(Injector.CreateInstance<IImageRepository>()))),
                ownerId);
            SuperOwner = new SuperOwnerViewModel();
            SuperOwner.TotalRatings = 0;
            SuperOwner.AverageRating = 0;

            Home = new NavigationCommand<HomeViewModel>(this.navigationStore,
                () => new HomeViewModel(this.navigationStore, ownerId));
            ModifyReservations = new NavigationCommand<ModifyReservationsViewModel>(this.navigationStore,
                () => new ModifyReservationsViewModel(ownerId));
            Reviews = new NavigationCommand<ReviewsViewModel>(this.navigationStore, () => new ReviewsViewModel(ownerId));
            Renovations = new NavigationCommand<RenovationsDisplayViewModel>(this.navigationStore,
                () => new RenovationsDisplayViewModel(ownerId));

            accommodationRatingService.SubscribeSuperOwner(this);
            Update();

            this.navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public void Update()
        {
            int totalRatings = 0;
            double averageRatings = 0;
            var result = accommodationRatingService.IsSuperOwner(ref totalRatings, ref averageRatings);
            SuperOwner.IsSuper = result.Item1;
            SuperOwner.TotalRatings = result.Item2;
            SuperOwner.AverageRating = result.Item3;
        }
    }
}
