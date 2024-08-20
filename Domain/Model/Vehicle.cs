using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class Vehicle : ISerializable
    {
        public int Id { get; set; }
        public int Capacity { get; set; }
        public int DriverId { get; set; }
        public string Name { get; set; }

        public Vehicle() { }
        public Vehicle(int id, int capacity, int driverId, string name)
        {
            Id = id;
            Capacity = capacity;
            DriverId = driverId;
            Name = name;
        }

        public Vehicle(int capacity, int driverId, string name)
        {
            Capacity = capacity;
            DriverId = driverId;
            Name = name;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Capacity = Convert.ToInt32(values[1]);
            DriverId = Convert.ToInt32(values[2]);
            Name = Convert.ToString(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Capacity.ToString(),
                DriverId.ToString(),
                Name
            };
            return csvValues;
        }
    }
}
