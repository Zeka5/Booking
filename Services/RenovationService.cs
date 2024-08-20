using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class RenovationService
    {
        private readonly IRenovationRepository renovationRepository;
        private readonly AccommodationService accommodationService;

        public RenovationService(IRenovationRepository renovationRepository)
        {
            this.renovationRepository = renovationRepository;
        }

        public RenovationService(IRenovationRepository renovationRepository, AccommodationService accommodationService)
        {
            this.renovationRepository = renovationRepository;
            this.accommodationService = accommodationService;
        }

        public void AddRenovation(Renovation renovation)
        {
            renovationRepository.Add(renovation);
        }

        public IEnumerable<Renovation> GetRenovationByOwnerId(int id)
        {
            return renovationRepository.GetAll().Where(renovation =>
                accommodationService.GetAccommodationById(renovation.AccommodationId).OwnerId == id);
        }

        public Image? GetImageByEntityId(int id)
        {
            return accommodationService.GetImageByEntityIdAndType(id, ResourceType.Accommodation);
        }

        public Accommodation GetAccommodationById(int id)
        {
            return accommodationService.GetAccommodationById(id);
        }

        public void DeleteRenovation(int id)
        {
            renovationRepository.Delete(id);
        }

        public void SubscribeRenovations(IObserver observer)
        {
            renovationRepository.Subscribe(observer);
        }
    }
}
