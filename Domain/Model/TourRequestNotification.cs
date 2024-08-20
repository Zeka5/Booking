using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourRequestNotification:ISerializable
    {
        public int Id { get; set; }
        public int TourRequestId {  get; set; }
        public string Title { get; set; }
        public string Text {  get; set; }
        public DateTime CreationDate { get; set; }

        public bool IsRead {  get; set; }

        public TourRequestNotification() { }

        public TourRequestNotification(int tourRequestId, string text, DateTime creationDate)
        {
            TourRequestId = tourRequestId;
            Title = "Tour request accepted";
            Text = text;
            CreationDate = creationDate;
            IsRead = false;
        }

        public TourRequestNotification(int id,int tourRequestId, string title, string text, DateTime creationDate,bool isRead)
        {
            Id = id;
            TourRequestId = tourRequestId;
            Title = title;
            Text = text;
            CreationDate = creationDate;
            IsRead = isRead;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TourRequestId = Convert.ToInt32(values[1]);
            Title = values[2];
            Text = values[3];
            CreationDate = DateTime.ParseExact(values[4], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            IsRead = Convert.ToBoolean(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TourRequestId.ToString(),
                Title,
                Text,
                CreationDate.ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture),
                IsRead.ToString()
            };

            return csvValues;
        }
    }
}
