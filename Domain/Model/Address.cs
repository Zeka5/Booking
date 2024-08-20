using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Domain.Model
{
    public class Address : Location, ISerializable
    {
        public int AdressId { get; set; }
        public string Street { get; set; }
        public int StreetNumber { get; set; }
        public Address()
        {

        }
        public Address(string street, int streetNumber, int locationId)
        {
            Street = street;
            StreetNumber = streetNumber;
            Id = locationId;
        }
        public void FromCSV(string[] values)
        {
            AdressId = Convert.ToInt32(values[0]);
            Id = Convert.ToInt32(values[1]);
            Street = values[2];
            StreetNumber = Convert.ToInt32(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                AdressId.ToString(),
                Id.ToString(),
                Street,
                StreetNumber.ToString()
            };
            return csvValues;
        }
    }
}
