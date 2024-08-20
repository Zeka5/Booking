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
    public class OwnerHomePageService
    {
        private IAccommodationRepository accommodationRepository;
        private ILocationRepository locationRepository;
        private int ownerId;

        public OwnerHomePageService(int ownerId)
        {
            accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            locationRepository = Injector.CreateInstance<ILocationRepository>();
            this.ownerId = ownerId;
        }

        private Location? GetLocationById(int id)
        {
            return locationRepository.GetById(id);
        }

        public void Update(ObservableCollection<AccommodationDto> accommodations)
        {
            accommodations.Clear();
            foreach (var accommodation in accommodationRepository.GetByOwnerId(ownerId))
                accommodations.Add(new AccommodationDto(accommodation, GetLocationById(accommodation.LocationId), null));
        }

        public void SubscribeAccommodations(IObserver observer)
        {
            accommodationRepository.Subscribe(observer);
        }
    }
}
