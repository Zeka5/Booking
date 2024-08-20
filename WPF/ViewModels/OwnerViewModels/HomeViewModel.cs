using BookingApp.Commands;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
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
    public class HomeViewModel : ViewModelBase, IObserver
    {
        private AccommodationService accommodationService;
        private int ownerId;

        public ObservableCollection<AccommodationViewModel> Accommodations { get; }

        public ICommand OpenRegistrationForm { get; }
        public ICommand Stats { get; }
        public ICommand Renovate { get; }

        public HomeViewModel(NavigationStore navigationStore, int ownerId)
        {
            accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(),
                new LocationService(Injector.CreateInstance<ILocationRepository>()),
                new ImageService(Injector.CreateInstance<IImageRepository>()));
            this.ownerId = ownerId;

            Accommodations = new ObservableCollection<AccommodationViewModel>();
            accommodationService.Subscribe(this);

            OpenRegistrationForm = new NavigationCommand<RegisterAccommodationViewModel>(navigationStore,
                () => new RegisterAccommodationViewModel(navigationStore, ownerId));
            Stats = new OpenStatsCommand(navigationStore);
            Renovate = new ScheduleRenovationCommand(navigationStore);

            Update();
        }

        public void Update()
        {
            Accommodations.Clear();
            foreach (var accommodation in accommodationService.GetAccommodationsByOwnerId(ownerId))
            {
                var image = accommodationService.GetImageByEntityIdAndType(accommodation.Id, ResourceType.Accommodation);
                var location = accommodationService.GetLocationById(accommodation.LocationId);
                Accommodations.Add(new AccommodationViewModel(accommodation.Id, accommodation.OwnerId,
                    (image is null) ? Image.defaultAccommodationImagePath : image.Path,
                    accommodation.Name, location.City, location.Country, accommodation.Type.ToString()));
            }
        }
    }
}
