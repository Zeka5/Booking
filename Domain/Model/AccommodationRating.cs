using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class AccommodationRating : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationReservationId { get; set; }
        public int Cleanliness { get; set; }
        public int OwnerCorrectness { get; set; }
        public string? AdditionalComment { get; set; }

        public AccommodationRating() { }

        public AccommodationRating(int id, int accommodationReservationId, int cleanliness, int ownerCorrectness, string? additionalComment)
        {
            Id = id;
            AccommodationReservationId = accommodationReservationId;
            Cleanliness = cleanliness;
            OwnerCorrectness = ownerCorrectness;
            AdditionalComment = additionalComment;
        }

        public AccommodationRating(int accommodationReservationId, int cleanliness, int ownerCorrectness, string? additionalComment)
        {
            AccommodationReservationId = accommodationReservationId;

            Cleanliness = cleanliness;
            OwnerCorrectness = ownerCorrectness;
            AdditionalComment = additionalComment;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationReservationId = int.Parse(values[1]);
            Cleanliness = int.Parse(values[2]);
            OwnerCorrectness = int.Parse(values[3]);
            AdditionalComment = values[4];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationReservationId.ToString(),
                Cleanliness.ToString(),
                OwnerCorrectness.ToString(),
                AdditionalComment
            };

            return csvValues;
        }
    }
}
