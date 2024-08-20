using BookingApp.Dto;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public enum STATUS { Pending, Accepted , Expired }
    public class TourRequest : ISerializable
    {
        public int Id {  get; set; }
        public int LocationId {  get; set; }
        public string Description{ get; set; }
        public int LanguageId {  get; set; } 
        public int NumberOfGuests {  get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int TouristId { get; set; }

        public int GuideId {  get; set; }

        public STATUS Status {  get; set; }

        public DateTime CreationDate { get; set; }

        public int ComplexTourId { get; set; }


        public TourRequest() { }    

        public TourRequest(int id,int locationId,string description,int languageId,int numberOfGuests,DateOnly startDate,DateOnly endDate,int touristId, STATUS status,DateTime creationDate,int guideId,int complexTourId) 
        {
            Id = id;
            LocationId = locationId;
            Description = description;
            LanguageId = languageId;
            NumberOfGuests = numberOfGuests;
            StartDate = startDate;
            EndDate = endDate;
            TouristId = touristId;
            Status = status;
            CreationDate = creationDate;
            GuideId= guideId;
            ComplexTourId = complexTourId;
        }

        public TourRequest(int locationId, string description, int languageId, int numberOfGuests, DateOnly startDate, DateOnly endDate, int touristId,int complexTourId=-1) 
        {
            LocationId = locationId;
            Description = description;
            LanguageId = languageId;
            NumberOfGuests = numberOfGuests;
            StartDate = startDate;
            EndDate = endDate;
            TouristId = touristId;
            this.Status = STATUS.Pending;
            this.CreationDate=DateTime.Now;
            GuideId = -1;
            ComplexTourId= complexTourId;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            LocationId = Convert.ToInt32(values[1]);
            Description = values[2];
            LanguageId = Convert.ToInt32(values[3]);
            NumberOfGuests = Convert.ToInt32(values[4]);
            StartDate = DateOnly.ParseExact(values[5], "dd/MM/yyyy");
            EndDate = DateOnly.ParseExact(values[6], "dd/MM/yyyy");
            TouristId = Convert.ToInt32(values[7]);
            if (string.Equals(values[8], "Pending")) Status = STATUS.Pending;
            else if (string.Equals(values[8], "Accepted")) Status = STATUS.Accepted;
            else if (string.Equals(values[8], "Expired")) Status = STATUS.Expired;
            CreationDate = DateTime.ParseExact(values[9], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            GuideId = Convert.ToInt32(values[10]);
            ComplexTourId = Convert.ToInt32(values[11]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                LocationId.ToString(),
                Description,
                LanguageId.ToString(),
                NumberOfGuests.ToString(),
                StartDate.ToString("dd/MM/yyyy"),
                EndDate.ToString("dd/MM/yyyy"),
                TouristId.ToString(),
                Status.ToString(),
                CreationDate.ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture),
                GuideId.ToString(),
                ComplexTourId.ToString()
            };

            return csvValues;
        }
    }
}
