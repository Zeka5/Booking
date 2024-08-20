using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class VehicleLocations : ISerializable
    {
        public int VehicleId { get; set; }
        public int LocationId { get; set; }

        public VehicleLocations() { }
        public VehicleLocations(int vehicleId, int locationId)
        {
            VehicleId = vehicleId;
            LocationId = locationId;
        }

        public void FromCSV(string[] values)
        {
            VehicleId = Convert.ToInt32(values[0]);
            LocationId = Convert.ToInt32(values[1]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                VehicleId.ToString(),
                LocationId.ToString()
            };

            return csvValues;
        }
    }
}
