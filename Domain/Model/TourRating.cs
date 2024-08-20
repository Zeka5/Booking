using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class TourRating : BookingApp.Serializer.ISerializable
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public int TourGuestId { get; set; }
        public string Comment { get; set; }
        public bool Valid {  get; set; }
        public TourRating(){}
        public TourRating(int rating, int tourGuestId, string comment)
        {
            Rating = rating;
            TourGuestId = tourGuestId;
            Comment = comment;
            Valid = true;
        }
        public TourRating(int id,int rating, int tourGuestId, string comment,bool valid)
        {
            Id = id;
            Rating = rating;
            TourGuestId = tourGuestId;
            Comment = comment;
            Valid = valid;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Rating = Convert.ToInt32(values[1]);
            TourGuestId = Convert.ToInt32(values[2]);
            Comment = values[3];
            Valid = Convert.ToBoolean(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Rating.ToString(),
                TourGuestId.ToString(),
                Comment,
                Valid.ToString()
            };

            return csvValues;
        }
    }
}
