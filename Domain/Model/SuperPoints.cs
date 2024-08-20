using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class SuperPoints : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Value { get; set; }
        public DateTime ReceivingDate { get; set; }

        public SuperPoints() { }

        public SuperPoints(int userId, int value)
        {
            UserId = userId;
            ReceivingDate = DateTime.Now;
            Value = value;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            Value = int.Parse(values[2]);
            ReceivingDate = DateTime.ParseExact(values[3], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                UserId.ToString(),
                Value.ToString(),
                ReceivingDate.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
            };

            return csvValues;
        }
    }
}
