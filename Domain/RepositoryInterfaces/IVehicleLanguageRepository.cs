using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IVehicleLanguageRepository
    {
        List<VehicleLanguage> GetAll();
        List<int> GetLangauageIds(int vehicleId);
        List<VehicleLanguage> GetByLanguageId(int languageId);
        VehicleLanguage GetByIds(int vehicleId, int languageId);
        VehicleLanguage Add(int vehicleId, int languageId);
        VehicleLanguage Delete(int vehicleId, int languageId);
        List<int> GetVehiclesByLanguage(int languageId);
        void DeleteByVehicleId(int vehicleId);
    }
}
