using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class AccommodationSearchCriteria
    {
        public string AccommodationName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int GuestNumber { get; set; }
        public int Day {  get; set; }
        public Type SelectedType { get; set; }
        public AccommodationSearchCriteria(string accommodationName, string city, string country, int guestNumber, int day, Type selectedType)
        {
            AccommodationName = accommodationName;
            City = city;
            Country = country;
            GuestNumber = guestNumber;
            Day = day;
            SelectedType = selectedType;
        }
        public bool IsSearched(Accommodation accommodation, Location location)
        {
           // bool minStayDayMatch = accommodation.MinStayDays <= Day || Day == 0;
            //return accommodation.Name.ToLower().Contains(AccommodationName.ToLower()) && location.City.ToLower().Contains(City.ToLower()) &&
                  //  location.Country.ToLower().Contains(Country.ToLower()) && accommodation.MaxGuest >= GuestNumber &&
                   // minStayDayMatch && (accommodation.Type == SelectedType || SelectedType == Type.Any);           
            bool minStayDayMatch = accommodation.MinStayDays <= Day || Day == 0;
            bool nameMatch = accommodation.Name.ToLower().Contains(AccommodationName.ToLower());
            bool cityMatch = location.City.ToLower().Contains(City.ToLower());
            bool countryMatch = location.Country.ToLower().Contains(Country.ToLower());
            bool guestNumberMatch = accommodation.MaxGuest >= GuestNumber;
            bool typeMatch = accommodation.Type == SelectedType || SelectedType == Type.Any;

            return minStayDayMatch && nameMatch && cityMatch && countryMatch && guestNumberMatch && typeMatch;
        }
    }
}
