using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class VoucherDto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int TourReservationId { get; set; }
        public string Description { get; set; }
        public int TouristId { get; set; }

        public int GuideId { get; set; }

        private string validityStart;
        public string ValidityStart
        {
            get
            {
                return validityStart;
            }
            set
            {
                if (value != validityStart)
                {
                    validityStart = value;
                    OnPropertyChanged("ValidityStart");
                }

            }
        }

        private string validityEnd;
        public string ValidityEnd
        {
            get
            {
                return validityEnd;
            }
            set
            {
                if (value != validityEnd)
                {
                    validityEnd = value;
                    OnPropertyChanged("ValidityEnd");
                }

            }
        }

        private string status;
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (value != status)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }

            }
        }

        public VoucherDto(){}
        public VoucherDto(Voucher voucher)
        {
            Id = voucher.Id;
            TouristId = voucher.TouristId;
            GuideId=voucher.GuideId;
            TourReservationId = voucher.TourReservationId;
            ValidityStart = voucher.ValidityStart.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            ValidityEnd = voucher.ValidityEnd.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            if (voucher.Status.ToString() == "VALID")
            {
                Status = "VALID";
            }
            else if (voucher.Status.ToString() == "USED")
            {
                Status = "USED";
            }
            else
            {
                Status = "EXPIRED";
            }
            Description = voucher.Description;
        }

        public Voucher toVoucher()
        {
            return new Voucher(Id,TouristId,GuideId, DateTime.ParseExact(validityStart, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture), DateTime.ParseExact(validityEnd, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),TourReservationId,Description);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }

        }
    }
}
