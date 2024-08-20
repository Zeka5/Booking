using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace BookingApp.Domain.Model
{
    public class Location : ISerializable
    {
        public int Id { get; set; }
        public string City { get; set; }

        public string Country { get; set; }

        public Location() { }
        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }
        public Location(int id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }

        public override string ToString()
        {
            return City + "," + Country;
        }

        public Location Parse(string text)
        {
            const string pattern = @"(?<City>.+),(?<Country>.+)";
            Match match = Regex.Match(text, pattern);
            if (match.Success)
            {
                City = match.Groups["City"].Value;
                Country = match.Groups["Country"].Value;
                return new Location(City, Country);
            }
            else
            {
                //mogao bi se dodati window pop-up ili nesto
                //ako je lose upisana adresa --Zeka
                return new Location();
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                City,
                Country
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = values[1];
            Country = values[2];
        }
    }
}
