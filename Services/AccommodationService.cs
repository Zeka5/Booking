using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class AccommodationService
    {
        private IAccommodationRepository accommodationRepository;
        private LocationService locationService;
        private ImageService imageService;

        public AccommodationService(IAccommodationRepository accommodationRepository, ImageService imageService)
        {
            this.accommodationRepository = accommodationRepository;
            this.imageService = imageService;
        }

        public AccommodationService(IAccommodationRepository accommodationRepository, LocationService locationService, 
            ImageService imageService) {
            this.accommodationRepository = accommodationRepository;
            this.locationService = locationService;
            this.imageService = imageService;
        }

        public void AddAccommodation(Accommodation accommodation)
        {
            accommodationRepository.Add(accommodation);
        }

        public void Update(ObservableCollection<AccommodationDto> accommodationList) {
            accommodationList.Clear();
            foreach (Accommodation accommodation in accommodationRepository.GetAll())
            {
                Location? location = locationService.GetById(accommodation.LocationId);
                Image? image = imageService.GetFirstByEntityAndType(accommodation.Id, ResourceType.Accommodation);
                accommodationList.Add(new AccommodationDto(accommodation, location, image));
            }
        }

        public void SearchClick(ObservableCollection<AccommodationDto> accommodationList, AccommodationSearchCriteria accommodationSearchCriteria)
        {
            accommodationList.Clear();
            List<Accommodation> accommodations = accommodationRepository.GetAll();
            foreach (Accommodation accommodation in accommodations)
            {
                Location? location = locationService.GetById(accommodation.LocationId);
                Image? image = imageService.GetFirstByEntityAndType(accommodation.Id, ResourceType.Accommodation);
                if (accommodationSearchCriteria.IsSearched(accommodation, location))
                {
                    accommodationList.Add(new AccommodationDto(accommodation, location, image));
                }
            }
        }

        public Location GetLocationById(int locationId)
        {
            return locationService.GetById(locationId);
        }
        public List<Accommodation> GetAll() { 
            return accommodationRepository.GetAll();
        }

        public Accommodation GetAccommodationById(int id) {
            return accommodationRepository.GetById(id);
        }

        public bool GetIsCancelable(AccommodationReservation accommodationReservation) { 
            return accommodationRepository.GetIsCancelable(accommodationReservation);
        }

        public Accommodation GetLastAccommodation()
        {
            return accommodationRepository.GetLast();
        }

        public IEnumerable<Accommodation> GetAccommodationsByOwnerId(int id)
        {
            return accommodationRepository.GetByOwnerId(id);
        }

        public IEnumerable<Location> GetAllLocations()
        {
            return locationService.GetAll();
        }

        public bool AssignImages(ICollection<ImageItemDto> images)
        {
            foreach (var image in images)
                if (!imageService.Add(image.ToModel(GetLastAccommodation().Id, ResourceType.Accommodation)))
                    return false;

            return true;
        }

        public Image? GetImageByEntityIdAndType(int id, ResourceType type)
        {
            return imageService.GetFirstByEntityAndType(id, type);
        }

        public void Subscribe(IObserver observer)
        {
            accommodationRepository.Subscribe(observer);
        }
    }
}
