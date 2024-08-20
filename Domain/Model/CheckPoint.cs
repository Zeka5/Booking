using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public enum Types { Start, Other, Finish }
    public class CheckPoint : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TourId { get; set; }
        public int Index { get; set; }
        public CheckPoint() { }

        public CheckPoint(string name, int tourId, int index)
        {
            Name = name;
            TourId = tourId;
            Index = index;
        }

        public CheckPoint(string name, int index)
        {
            Name = name;
            Index = index;
        }

        public CheckPoint(int id, string name, int tourId, int index)
        {
            Id = id;
            Name = name;
            TourId = tourId;
            Index = index;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            TourId = Convert.ToInt32(values[2]);
            Index = Convert.ToInt32(values[3]);

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                TourId.ToString(),
                Index.ToString()
            };
            return csvValues;
        }
    }
}
