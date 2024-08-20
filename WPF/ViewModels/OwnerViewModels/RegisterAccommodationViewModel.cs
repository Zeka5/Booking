using BookingApp.Services;
using BookingApp.Domain.Model;
using BookingApp.Dto;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using BookingApp.WPF.View;
using System.Collections.ObjectModel;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.ViewModels.OwnerViewModels;
using BookingApp.Stores;
using System.Windows.Input;
using BookingApp.Commands;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class RegisterAccommodationViewModel : ViewModelBase
    {
        private AccommodationService accommodationService;

        public List<LocationDto> Locations { get; }
        public AccommodationDto AccommodationDto { get; set; }
        public ObservableCollection<ImageItemDto> AddedImages { get; set; }

        public ICommand AddImages { get; }
        public ICommand RegisterAccommodation { get; }
        public ICommand Cancel { get; }

        public RegisterAccommodationViewModel(NavigationStore navigationStore, int ownerId)
        {
            accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(),
                new LocationService(Injector.CreateInstance<ILocationRepository>()),
                new ImageService(Injector.CreateInstance<IImageRepository>()));

            Locations = new List<LocationDto>();
            AccommodationDto = new AccommodationDto();
            AddedImages = new ObservableCollection<ImageItemDto>();
            AccommodationDto.OwnerId = ownerId;

            AddImages = new AddImagesCommand(AddedImages);
            RegisterAccommodation = new RegisterAccommodationCommand(navigationStore, this);
            Cancel = new NavigationCommand<HomeViewModel>(navigationStore,
                () => new HomeViewModel(navigationStore, ownerId));

            Load();
        }

        private void Load()
        {
            Locations.Clear();
            foreach (var location in accommodationService.GetAllLocations()) Locations.Add(new LocationDto(location));
        }

        public bool ValidateValues => AccommodationDto.ValidateValues();

        public void Submit()
        {
            accommodationService.AddAccommodation(AccommodationDto.ToModel());
            accommodationService.AssignImages(AddedImages);
        }

        public void AddClick()
        {
            var imageUpload = new ImageUpload(AddedImages);
            imageUpload.ShowDialog();
        }
    }
}
