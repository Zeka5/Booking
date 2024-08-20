using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourRequestSearchParametars
    {
        public string Country { get; set; }
        public string City { get; set; }
        public int Capacity { get; set; }
        public string Language { get; set; }
        public DateOnly StartDate { get; set; }
        
        public DateOnly EndDate { get; set; }

        public TourRequestSearchParametars(string country,string city,int capacity,string language,DateOnly startDate,DateOnly endDate)
        {
            Country=country;
            City=city;
            Capacity=capacity;
            Language=language;
            StartDate=startDate;
            EndDate=endDate;
        }
        public bool IsSearched(TourRequest tourRequest, Location location, Language language)
        {
            bool countryMatch = location.Country.ToLower().Contains(Country.ToLower());
            bool cityMatch = location.City.ToLower().Contains(City.ToLower());
            bool capacityMatch = tourRequest.NumberOfGuests == Capacity || Capacity == 0;
            bool languageMatch = language.Name.ToLower().Contains(Language.ToLower());
            if (StartDate == DateOnly.FromDateTime(DateTime.Now) && EndDate == DateOnly.FromDateTime(DateTime.Now)) return cityMatch && countryMatch && capacityMatch && languageMatch;
            bool dateRangeMatch = PerfectIntersect(tourRequest) || StartDateInRange(tourRequest) || EndDateInRange(tourRequest);
            return cityMatch && countryMatch && capacityMatch && languageMatch && dateRangeMatch;
        }

        private bool PerfectIntersect(TourRequest tourRequest)
        {
            return StartDateInRange(tourRequest) && EndDateInRange(tourRequest);
        }

        private bool StartDateInRange(TourRequest tourRequest)
        {
            return StartDate <= tourRequest.StartDate && tourRequest.StartDate <= EndDate;
        }

        private bool EndDateInRange(TourRequest tourRequest)
        {
            return StartDate <= tourRequest.EndDate && tourRequest.EndDate <= EndDate;
        }
    }
}
