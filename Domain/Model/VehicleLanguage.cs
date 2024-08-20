using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class VehicleLanguage : ISerializable
    {
        public int VehicleId { get; set; }
        public int LanguageId { get; set; }

        public VehicleLanguage() { }
        public VehicleLanguage(int vehicleId, int languageId)
        {
            VehicleId = vehicleId;
            LanguageId = languageId;
        }

        public void FromCSV(string[] values)
        {
            VehicleId = Convert.ToInt32(values[0]);
            LanguageId = Convert.ToInt32(values[1]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                VehicleId.ToString(),
                LanguageId.ToString()
            };

            return csvValues;
        }
    }
}
