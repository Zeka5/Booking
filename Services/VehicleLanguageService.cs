using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.View.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class VehicleLanguageService
    {
        private IVehicleLanguageRepository vehicleLanguageRepository;
        private LanguageService languageService;

        public VehicleLanguageService(IVehicleLanguageRepository vehicleLanguageRepository)
        {
            this.vehicleLanguageRepository = vehicleLanguageRepository;
        }

        public VehicleLanguageService(IVehicleLanguageRepository vehicleLanguageRepository, LanguageService languageService)
        {
            this.vehicleLanguageRepository = vehicleLanguageRepository;
            this.languageService = languageService;
        }

        public List<int> GetVehicleIdsByLangaugeName(string languageName)
        {
            int languageId = languageService.GetByName(languageName).Id;
            List<int> vehicleIds = GetVehicleByLanguageId(languageId);
            return vehicleIds;
        }

        public List<int> GetVehicleByLanguageId(int languageId)
        {
            return vehicleLanguageRepository.GetVehiclesByLanguage(languageId);
        }

        public void Add(int vehicleId, int languageId)
        {
            vehicleLanguageRepository.Add(vehicleId, languageId);
        }

        public List<int> GetLanguagesByVehicleId(int vehicleId)
        {
            return vehicleLanguageRepository.GetLangauageIds(vehicleId);
        }
    }
}
