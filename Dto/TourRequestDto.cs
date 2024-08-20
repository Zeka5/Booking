using BookingApp.Domain.Model;
using BookingApp.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class TourRequestDto:INotifyPropertyChanged
    {
        public int Id { get; set; }

        public int LocationId { get; set; }

        public Location Location { get; set; }

        private string locationString;
        public string LocationString
        {
            get
            {
                return locationString;
            }
            set
            {
                if (value != locationString)
                {
                    locationString = value;
                    OnPropertyChanged("LocationString");
                }

            }
        }


        public int LanguageId { get; set; }

        public Language Language { get; set; }

        private string languageString;
        public string LanguageString
        {
            get
            {
                return languageString;
            }
            set
            {
                if (value != languageString)
                {
                    languageString = value;
                    OnPropertyChanged("LanguageString");
                }

            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value != description)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }

            }
        }

        private int numberOfGuests;
        public int NumberOfGuests
        {
            get
            {
                return numberOfGuests;
            }
            set
            {
                if (value != numberOfGuests)
                {
                    numberOfGuests = value;
                    OnPropertyChanged("NumberOfGuests");
                }

            }
        }
        private string startDate;
        public string StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                if (value != startDate)
                {
                    startDate = value;
                    OnPropertyChanged("StartDate");
                }

            }
        }
        private string endDate;
        public string EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                if (value != endDate)
                {
                    endDate = value;
                    OnPropertyChanged("EndDate");
                }

            }
        }
        public int TouristId { get; set; }
        public int GuideId {  get; set; }
        public int ComplexTourRequestId { get; set; }

        private string touristName;
        public string TouristName
        {
            get
            {
                return touristName;
            }
            set
            {
                if (value != touristName)
                {
                    touristName = value;
                    OnPropertyChanged("TouristName");
                }

            }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        public DateTime CreationDate {  get; set; }

        public TourRequestDto()
        {
        }
        public TourRequestDto(TourRequest tourRequest, Location location, Language language)
        {
            Id = tourRequest.Id;
            LocationId = tourRequest.LocationId;
            Location = location;
            LocationString = location.ToString();
            Description = tourRequest.Description;
            LanguageId = tourRequest.LanguageId;
            Language = language;
            LanguageString = language.ToString();
            NumberOfGuests = tourRequest.NumberOfGuests;
            StartDate = tourRequest.StartDate.ToString("dd/MM/yyyy");
            EndDate = tourRequest.EndDate.ToString("dd/MM/yyyy");
            TouristId = tourRequest.TouristId;
            ComplexTourRequestId = tourRequest.ComplexTourId;
            GuideId=tourRequest.GuideId;
            if (tourRequest.Status.ToString().Equals("Pending"))
            {
                Status = "Pending";
            }
            else if (tourRequest.Status.ToString().Equals("Accepted"))
            {
                Status = "Accepted";
            }
            else
            {
                Status = "Expired";
            }
            CreationDate = tourRequest.CreationDate;
        }
        public TourRequest ToTourRequest()
        {
            STATUS requestStatus;
            if (Status.Equals("Pending"))
            {
                requestStatus = STATUS.Pending;
            }
            else if (Status.ToString().Equals("Accepted"))
            {
                requestStatus = STATUS.Accepted;
            }
            else
            {
                requestStatus = STATUS.Expired;
            }

            return new TourRequest(Id,LocationId,Description,LanguageId,NumberOfGuests,DateOnly.ParseExact(StartDate,"dd/MM/yyyy"),DateOnly.ParseExact(EndDate,"dd/MM/yyyy"),TouristId,requestStatus,CreationDate,GuideId,ComplexTourRequestId);
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
