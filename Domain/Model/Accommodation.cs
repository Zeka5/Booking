using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public enum Type
    {
        Apartment,
        House,
        Cabin,
        Any
    }
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LocationId { get; set; }
        public Type Type { get; set; }
        public int MaxGuest { get; set; }
        public int MinStayDays { get; set; }
        public int CancellationDays { get; set; }
        public int OwnerId { get; set; }

        public Accommodation()
        {

        }
        public Accommodation(string name, int locationId, Type type, int maxGuest, int minStayDays, int cancellationDays, int ownerId)
        {
            Name = name;
            LocationId = locationId;
            Type = type;
            MaxGuest = maxGuest;
            MinStayDays = minStayDays;
            CancellationDays = cancellationDays;
            OwnerId = ownerId;
        }

        public Accommodation(int id, string name, int locationId, Type type, int maxGuest, int minStayDays, int cancellationDays, int ownerId)
        {
            Id = id;
            Name = name;
            LocationId = locationId;
            Type = type;
            MaxGuest = maxGuest;
            MinStayDays = minStayDays;
            CancellationDays = cancellationDays;
            OwnerId = ownerId;
        }

        public static Type GetType(string type)
        {
            if (string.Equals(type, "Apartment")) return Type.Apartment;
            else if (string.Equals(type, "House")) return Type.House;
            else return Type.Cabin;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Name,
                LocationId.ToString(),
                Type.ToString(),
                MaxGuest.ToString(),
                MinStayDays.ToString(),
                CancellationDays.ToString(),
                OwnerId.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            LocationId = int.Parse(values[2]);
            Type = Enum.TryParse(values[3], out Type type) ? type : Type.Apartment;
            MaxGuest = Convert.ToInt32(values[4]);
            MinStayDays = Convert.ToInt32(values[5]);
            CancellationDays = Convert.ToInt32(values[6]);
            OwnerId = int.Parse(values[7]);
        }
    }
}
