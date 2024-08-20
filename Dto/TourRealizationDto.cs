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
    public class TourRealizationDto:INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int TourId { get; set; }
        public string TourName {  get; set; }
        public string TourCountry { get; set; }
        public string TourCity { get; set; }
        public string EndTime { get; set; }
        public string Checkpoint { get; set; }

        private string startTime;
        public string StartTime 
        {
            get
            {
                return startTime;
            }
            set
            {
                if (value != startTime)
                {
                    startTime= value;
                    OnPropertyChanged("StartTime");
                }

            }
        }

        private string time;
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                if (value != time)
                {
                    time = value;
                    OnPropertyChanged("Time");
                }

            }
        }

        private int availability;
        public int Availability
        {
            get
            {
                return availability;
            }
            set
            {
                if (value != availability)
                {
                    availability = value;
                    OnPropertyChanged("Availability");
                }

            }
        }

        private string tourState;

        public string TourState
        {
            get { return tourState; }
            set
            {
                if (tourState != value)
                {
                    tourState = value;
                    OnPropertyChanged("TourState");
                }
            }
        }

        private int lastCheckPoint;
        public int LastCheckPoint
        {
            get
            {
                return lastCheckPoint;
            }
            set
            {
                if (value != lastCheckPoint)
                {
                    lastCheckPoint = value;
                    OnPropertyChanged("LastCheckPoint");
                }

            }
        }


        public TourRealizationDto()
        {

        }
        public TourRealizationDto(TourRealization tourRealization)
        {
            Id = tourRealization.Id;
            StartTime = tourRealization.StartTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            Time=StartTime.Split(' ')[1];
            TourId= tourRealization.TourId;
            Availability = tourRealization.Availability;
            if (tourRealization.TourState.ToString() == "Started")
            {
                TourState = "Started";
            }
            else if(tourRealization.TourState.ToString() == "Finished")
            {
                TourState = "Finished";
            }
            else if (tourRealization.TourState.ToString() == "Cancelled")
            {
                TourState = "Cancelled";
            }
            else
            {
                TourState = "None";
            }
            LastCheckPoint = tourRealization.LastCheckPoint;
        }
        public TourRealizationDto(TourRealization tourRealization, string tourName, string tourCountry, string tourCity, string endTime)
        {
            Id = tourRealization.Id;
            StartTime = tourRealization.StartTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            Time = StartTime.Split(' ')[1];
            TourId = tourRealization.TourId;
            Availability = tourRealization.Availability;
            if (tourRealization.TourState.ToString() == "Started")
            {
                TourState = "Started";
            }
            else if (tourRealization.TourState.ToString() == "Finished")
            {
                TourState = "Finished";
            }
            else if (tourRealization.TourState.ToString() == "Cancelled")
            {
                TourState = "Cancelled";
            }
            else
            {
                TourState = "None";
            }
            LastCheckPoint = tourRealization.LastCheckPoint;

            TourName = tourName;
            TourCountry = tourCountry;
            TourCity = tourCity;
            EndTime = endTime;
        }
        public TourRealizationDto(TourRealization tourRealization, string tourName, string tourCountry, string tourCity, string checkpoint, string imagePath, DateTime endTime)
        {
            Id = tourRealization.Id;
            StartTime = tourRealization.StartTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            Time = StartTime.Split(' ')[1];
            TourId = tourRealization.TourId;
            Availability = tourRealization.Availability;
            if (tourRealization.TourState.ToString() == "Started")
            {
                TourState = "Started";
            }
            else if (tourRealization.TourState.ToString() == "Finished")
            {
                TourState = "Finished";
            }
            else
            {
                TourState = "None";
            }
            LastCheckPoint = tourRealization.LastCheckPoint;

            TourName = tourName;
            TourCountry = tourCountry;
            TourCity = tourCity;
            Checkpoint = checkpoint;
            ImagePath = imagePath;
            EndTime = endTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        }
        public TourRealizationDto(TourRealization tourRealization, string tourName, string tourCountry, string tourCity, string imagePath, DateTime endTime)
        {
            Id = tourRealization.Id;
            StartTime = tourRealization.StartTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            Time = StartTime.Split(' ')[1];
            TourId = tourRealization.TourId;
            Availability = tourRealization.Availability;
            if (tourRealization.TourState.ToString() == "Started")
            {
                TourState = "Started";
            }
            else if (tourRealization.TourState.ToString() == "Finished")
            {
                TourState = "Finished";
            }
            else
            {
                TourState = "None";
            }
            LastCheckPoint = tourRealization.LastCheckPoint;

            TourName = tourName;
            TourCountry = tourCountry;
            TourCity = tourCity;
            ImagePath = imagePath;
            EndTime = endTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
        }

        public TourRealization ToTourRealization()
        {

            return new TourRealization(Id,TourId,DateTime.ParseExact(StartTime,"dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),availability,lastCheckPoint);
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
