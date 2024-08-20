using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BookingApp.Domain.Model
{
    public enum Rated
    {
        Rated,
        NotRated
    }
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TourRealizationId { get; set; }
        public int VisitorsCount { get; set; }
        public Rated IsRated { get; set; }
        public bool AcquiredVoucher { get; set; }
        public TourReservation()
        {
        }
        public TourReservation(int userId, int tourRealizationId, int visitorsCount, Rated isRated = Rated.NotRated)
        {
            UserId = userId;
            TourRealizationId = tourRealizationId;
            VisitorsCount = visitorsCount;
            IsRated = isRated;
            AcquiredVoucher = false;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            TourRealizationId = Convert.ToInt32(values[2]);
            VisitorsCount = Convert.ToInt32(values[3]);
            if (string.Equals(values[4], "Rated")) IsRated = Rated.Rated;
            else IsRated = Rated.NotRated;
            AcquiredVoucher = Convert.ToBoolean(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), TourRealizationId.ToString(), VisitorsCount.ToString(), IsRated.ToString(), AcquiredVoucher.ToString() };
            return csvValues;
        }
    }
}
