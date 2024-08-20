using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public enum ResourceType
    {
        Accommodation,
        Tour,
        Driver,
        Vehicle,
        None,
        TourRating
    }

    public class Image : ISerializable
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int EntityId { get; set; }
        public ResourceType ResourceType { get; set; }

        public const string defaultAccommodationImagePath = "../../../Resources/Images/AccommodationImages/smestaj24.jpg";

        public Image()
        {
            
        }

        public Image(string path, int entityId, ResourceType resourceType)
        {
            this.Path = path;
            this.EntityId = entityId;
            this.ResourceType = resourceType;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Path = values[1];
            EntityId = int.Parse(values[2]);

            if (string.Equals(values[3], "Accommodation")) ResourceType = ResourceType.Accommodation;
            else if (string.Equals(values[3], "Tour")) ResourceType = ResourceType.Tour;
            else if (string.Equals(values[3], "Driver")) ResourceType = ResourceType.Driver;
            else if (string.Equals(values[3], "Vehicle")) ResourceType = ResourceType.Vehicle;
            else if (string.Equals(values[3], "TourRating")) ResourceType = ResourceType.TourRating;
            else ResourceType = ResourceType.None;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Path,
                EntityId.ToString(),
                ResourceType.ToString()
            };

            return csvValues;
        }
    }
}
