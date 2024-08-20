using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class RegisterAccommodationService
    {
        private IImageRepository imageRepository;
        private ILocationRepository locationRepository;
        private IAccommodationRepository accommodationRepository;

        public RegisterAccommodationService()
        {
            imageRepository = Injector.CreateInstance<IImageRepository>();
            locationRepository = Injector.CreateInstance<ILocationRepository>();
            accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
        }

        public void LoadLocations(List<LocationDto> locations)
        {
            locations.Clear();
            foreach (var location in locationRepository.GetAll()) locations.Add(new LocationDto(location));
        }

        public bool AddImage(ImageItemDto image)
        {
            string fullPath = "../../.." + image.Path;
            if (imageRepository.ExistsWithPath(fullPath)) return false;
            int entityId = accommodationRepository.GetLast().Id;
            imageRepository.Add(new Image(fullPath, entityId, ResourceType.Accommodation));
            return true;
        }

        public void UpdateImage(string path)
        {
            Domain.Model.Image? image = imageRepository.GetByPath(path);
            image.EntityId = accommodationRepository.GetLast().Id;
            image.ResourceType = ResourceType.Accommodation;
            imageRepository.Update(image);
        }

        public void AddAccommodation(Accommodation accommodation)
        {
            accommodationRepository.Add(accommodation);
        }
    }
}
