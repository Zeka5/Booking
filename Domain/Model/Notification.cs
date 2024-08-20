using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BookingApp.Domain.Model
{
    public class Notification : ISerializable
    {
        public int Id { get; set; }

        public Notification() { }

        public Notification(int id)
        {
            Id = id;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString()
            };

            return csvValues;
        }
    }
}
