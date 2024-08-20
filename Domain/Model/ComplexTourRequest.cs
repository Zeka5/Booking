using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public enum STATE { Pending, Accepted, Expired }
    public class ComplexTourRequest:ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public STATE State { get; set; }

        public ComplexTourRequest() { }

        public ComplexTourRequest(int touristId)
        {
            TouristId = touristId;
            State = STATE.Pending;
        }

        public ComplexTourRequest(int id,int touristId,STATE state)
        {
            Id = id;
            TouristId = touristId;
            State = state;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            if (string.Equals(values[2], "Pending")) State = STATE.Pending;
            else if (string.Equals(values[2], "Accepted")) State = STATE.Accepted;
            else if (string.Equals(values[2], "Expired")) State = STATE.Expired;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TouristId.ToString(),
                State .ToString()
            };

            return csvValues;
        }
    }
}
