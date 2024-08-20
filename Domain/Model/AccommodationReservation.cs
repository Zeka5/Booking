using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public enum ReservationStatus { Active, Canceled };
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }

        public DateTime FirstDay { get; set; }

        public DateTime LastDay { get; set; }

        public int DaysToStay { get; set; }

        public int GuestNumber { get; set; }

        public int UserId { get; set; }

        public ReservationStatus Status { get; set; }

        public AccommodationReservation() { }

        public AccommodationReservation(int id, int accommodationId, DateTime firstDay, DateTime lastDay, int daysToStay, int guestNumber, int userId,ReservationStatus reservationStatus)
        {
            Id = id;
            FirstDay = firstDay;
            LastDay = lastDay;
            AccommodationId = accommodationId;
            DaysToStay = daysToStay;
            GuestNumber = guestNumber;
            UserId = userId;
            Status = reservationStatus;
        }
        public AccommodationReservation(int accommodationId, DateTime firstDay, DateTime lastDay, int daysToStay, int guestNumber, int userId, ReservationStatus reservationStatus)
        {
            FirstDay = firstDay;
            LastDay = lastDay;
            AccommodationId = accommodationId;
            DaysToStay = daysToStay;
            GuestNumber = guestNumber;
            UserId = userId;
            Status = reservationStatus;
        }

        public static int GetOverlapCounter(List<AccommodationReservation> accommodationReservations, DateTime beginDate)
        {
            int overlapCount = 0;
            foreach (AccommodationReservation accommodationReservation in accommodationReservations)
            {
                if (beginDate >= accommodationReservation.FirstDay && beginDate <= accommodationReservation.LastDay)
                {
                    overlapCount++;
                }
            }
            return overlapCount;
        }

        public AccommodationReservation ModifyDates(DateTime firstDay, DateTime lastDay)
        {
            if (((firstDay > FirstDay) && (firstDay <= LastDay))
                || ((firstDay > FirstDay) && (lastDay < LastDay)))
                    LastDay = firstDay.AddDays(-1);
            else if (firstDay <= FirstDay && lastDay < LastDay) FirstDay = lastDay.AddDays(1);
            else Status = ReservationStatus.Canceled;
            return this;
        }

        public string[] ToCSV()
        {
            string[] values = { Id.ToString(), AccommodationId.ToString(), FirstDay.ToString("dd/MM/yyyy"), LastDay.ToString("dd/MM/yyyy"), DaysToStay.ToString(), GuestNumber.ToString(), UserId.ToString(),Status.ToString() };
            return values;
        }
        public void FromCSV(string[] values)
        {
            if (values.Count() == 0) { return; }
            Id = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            string[] firstDay = values[2].Split('/');
            string[] lastDay = values[3].Split('/');
            FirstDay = new DateTime(Convert.ToInt32(firstDay[2]), Convert.ToInt32(firstDay[1]), Convert.ToInt32(firstDay[0]));
            LastDay = new DateTime(Convert.ToInt32(lastDay[2]), Convert.ToInt32(lastDay[1]), Convert.ToInt32(lastDay[0]));
            DaysToStay = Convert.ToInt32(values[4]);
            GuestNumber = Convert.ToInt32(values[5]);
            UserId = Convert.ToInt32(values[6]);
            Status = (ReservationStatus)Enum.Parse(typeof(ReservationStatus), values[7]);
        }
    }
}
