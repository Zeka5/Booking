using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class VehicleLanguageRepository : IVehicleLanguageRepository
    {
        private const string FilePath = "../../../Resources/Data/vehicleLanguages.csv";

        private readonly Serializer<VehicleLanguage> serializer;

        private List<VehicleLanguage> vehicleLanguages;

        public VehicleLanguageRepository()
        {
            serializer = new Serializer<VehicleLanguage>();
            vehicleLanguages = serializer.FromCSV(FilePath);
        }

        public List<VehicleLanguage> GetAll()
        {
            return serializer.FromCSV(FilePath);
        }

        public List<int> GetLangauageIds(int vehicleId)
        {
            List<int> foundLanguages = new List<int>();

            foreach (VehicleLanguage vehicleLanguage in vehicleLanguages)
            {
                if (vehicleLanguage.VehicleId == vehicleId)
                {
                    foundLanguages.Add(vehicleLanguage.LanguageId);
                }
            }
            return foundLanguages;
        }

        public List<VehicleLanguage> GetByVehicleId(int vehicleId)
        {
            List<VehicleLanguage> foundLanguages = new List<VehicleLanguage>();

            foreach (VehicleLanguage vehicleLanguage in vehicleLanguages)
            {
                if (vehicleLanguage.VehicleId == vehicleId)
                {
                    foundLanguages.Add(vehicleLanguage);
                }
            }
            return foundLanguages;
        }

        public List<VehicleLanguage> GetByLanguageId(int languageId)
        {
            List<VehicleLanguage> foundLanguages = new List<VehicleLanguage>();

            foreach (VehicleLanguage vehicleLanguage in vehicleLanguages)
            {
                if (vehicleLanguage.LanguageId == languageId)
                {
                    foundLanguages.Add(vehicleLanguage);
                }
            }
            return foundLanguages;
        }

        public List<int> GetVehiclesByLanguage(int languageId)
        {
            List<int> foundVehicles = new List<int>();

            foreach (VehicleLanguage vehicleLanguage in vehicleLanguages)
            {
                if (vehicleLanguage.LanguageId == languageId)
                {
                    foundVehicles.Add(vehicleLanguage.VehicleId);
                }
            }
            return foundVehicles;
        }

        public VehicleLanguage GetByIds(int vehicleId, int languageId)
        {
            return vehicleLanguages.Find(v => v.VehicleId == vehicleId && v.LanguageId == languageId);
        }

        public VehicleLanguage Add(int vehicleId, int languageId)
        {
            vehicleLanguages = serializer.FromCSV(FilePath);
            VehicleLanguage newVehicleLanguage = new VehicleLanguage(vehicleId, languageId);

            if (vehicleLanguages.Any(v => v.VehicleId == vehicleId && v.LanguageId == languageId))
            {
                return null;
            }

            vehicleLanguages.Add(newVehicleLanguage);
            serializer.ToCSV(FilePath, vehicleLanguages);
            return newVehicleLanguage;
        }

        public VehicleLanguage Delete(int vehicleId, int languageId)
        {
            vehicleLanguages = serializer.FromCSV(FilePath);
            VehicleLanguage foundVehicleLanguage = GetByIds(vehicleId, languageId);

            if (foundVehicleLanguage == null)
            {
                return null;
            }

            vehicleLanguages.Remove(foundVehicleLanguage);
            serializer.ToCSV(FilePath, vehicleLanguages);
            return foundVehicleLanguage;
        }

        public void DeleteByVehicleId(int vehicleId)
        {
            foreach (VehicleLanguage vehicleLanguage in GetByVehicleId(vehicleId))
            {
                Delete(vehicleLanguage.VehicleId, vehicleLanguage.LanguageId);
            }
        }
    }
}
