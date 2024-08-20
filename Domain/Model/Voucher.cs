using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public enum ValidityStatus { VALID,USED,EXPIRED }
    public class Voucher:ISerializable
    {
        public int Id { get; set; }
        public int TouristId { get; set; } //tvoj user id
        public int GuideId { get; set; }
        public string Description { get; set; }
        public DateTime ValidityStart {  get; set; }
        public DateTime ValidityEnd { get; set;}
        public ValidityStatus Status { get; set; }
        public int TourReservationId {  get; set; }//bice -1 dok ti ne rezervises turu

        public Voucher(){}

        public Voucher(int touristId,int duration,string description,int guideId = -1)
        {
            TouristId = touristId;
            GuideId= guideId;
            ValidityStart = DateTime.Now;
            ValidityEnd = ValidityStart.AddMonths(duration);
            Status = ValidityStatus.VALID;
            TourReservationId = -1;
            Description = description;
        }
        public Voucher(int touristId, int monthsCount)
        {
            TouristId = touristId;
            ValidityStart = DateTime.Now;
            ValidityEnd = ValidityStart.AddMonths(monthsCount);
            Status = ValidityStatus.VALID;
            TourReservationId = -1;
            Description = "Received after a tour canceling";
        }

        public Voucher(int id, int touristId,int guideId,DateTime validityStart,DateTime validityEnd,int tourReservationId, string description)
        {
            Id = id;
            TouristId = touristId;
            GuideId= guideId;
            ValidityStart = validityStart;
            ValidityEnd = validityEnd;
            TourReservationId = tourReservationId;
            Description = description;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            TouristId = Convert.ToInt32(values[1]);
            GuideId = Convert.ToInt32(values[2]);
            ValidityStart = DateTime.ParseExact(values[3], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            ValidityEnd = DateTime.ParseExact(values[4], "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            if (string.Equals(values[5], "VALID")) Status = ValidityStatus.VALID;
            else if (string.Equals(values[5], "USED")) Status = ValidityStatus.USED;
            else Status = ValidityStatus.EXPIRED;
            Description = values[6];
            TourReservationId = Convert.ToInt32(values[7]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                TouristId.ToString(),
                GuideId.ToString(),
                ValidityStart.ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture),
                ValidityEnd.ToString("dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture),
                Status.ToString(),
                Description,
                TourReservationId.ToString()
            };

            return csvValues;
        }

    }
}
