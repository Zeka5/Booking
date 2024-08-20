using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class Rating : ISerializable
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int Cleanliness { get; set; }
        public int RuleFollowing { get; set; }
        public string? AdditionalComment { get; set; }

        public Rating() { }

        public Rating(int id, int reservationId, int cleanliness, int ruleFollowing, string? additionalComment)
        {
            Id = id;
            ReservationId = reservationId;
            Cleanliness = cleanliness;
            RuleFollowing = ruleFollowing;
            AdditionalComment = additionalComment;
        }

        public Rating(int reservationId, int cleanliness, int ruleFollowing, string? additionalComment)
        {
            ReservationId = reservationId;
            Cleanliness = cleanliness;
            RuleFollowing = ruleFollowing;
            AdditionalComment = additionalComment;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            Cleanliness = int.Parse(values[2]);
            RuleFollowing = int.Parse(values[3]);
            AdditionalComment = values[4];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReservationId.ToString(),
                Cleanliness.ToString(),
                RuleFollowing.ToString(),
                AdditionalComment
            };

            return csvValues;
        }
    }
}
