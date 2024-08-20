 using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace BookingApp.Domain.Model
{
    public enum State
    {
        Started,
        Finished,
        Cancelled,
        None
    }

    public class TourRealization : ISerializable
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public DateTime StartTime { get; set; }

        public int LastCheckPoint { get; set; }

        public int Availability { get; set; }

        public State TourState { get; set; }

        public TourRealization() { }

        public TourRealization(int id, int tourId, DateTime startTime, int availability,int lastCheckPoint)
        {
            Id = id;
            TourId = tourId;
            StartTime = startTime;
            Availability = availability;
            LastCheckPoint = lastCheckPoint;
        }

        public TourRealization(int tourId, DateTime startTime, int availability)
        {
            TourId = tourId;
            StartTime = startTime;
            Availability = availability;
            TourState = State.None;
            LastCheckPoint = -1;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            StartTime = DateTime.ParseExact(values[1], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            TourId = Convert.ToInt32(values[2]);
            Availability = Convert.ToInt32(values[3]);
            if (string.Equals(values[4], "Started")) TourState = State.Started;
            else if (string.Equals(values[4], "Finished")) TourState = State.Finished;
            else if (string.Equals(values[4], "Cancelled")) TourState = State.Cancelled;
            else TourState = State.None;
            LastCheckPoint = Convert.ToInt32(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                StartTime.ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture),
                TourId.ToString(),
                Availability.ToString(),
                TourState.ToString(),
                LastCheckPoint.ToString()
            };

            return csvValues;
        }
    }
}