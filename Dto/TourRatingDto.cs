using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class TourRatingDto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int TourGuestId { get; set; }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (value != imagePath)
                {
                    imagePath = value;
                    OnPropertyChanged("ImagePath");
                }
            }

        }

        private string tourGuestName;

        public string TourGuestName
        {
            get
            {
                return tourGuestName;
            }
            set
            {
                if (value != tourGuestName)
                {
                    tourGuestName = value;
                    OnPropertyChanged("TourGuestName");
                }

            }
        }

        private string checkPointName;
        public string CheckPointName
        {
            get
            {
                return checkPointName;
            }
            set
            {
                if (value != checkPointName)
                {
                    checkPointName = value;
                    OnPropertyChanged("CheckPointName");
                }

            }
        }

        private bool validityChecked;
        public bool ValidityChecked
        {
            get { return validityChecked; }
            set
            {
                if (validityChecked != value)
                {
                    validityChecked = value;
                    OnPropertyChanged("ValidityChecked");
                }
            }
        }

        private string valid;
        public string Valid
        {
            get
            {
                return valid;
            }
            set
            {
                if (value != valid)
                {
                    valid = value;
                    OnPropertyChanged("Valid");
                }

            }
        }

        private string comment;
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged("Comment");
                }

            }
        }

        private int rating;
        public int Rating
        {
            get
            {
                return rating;
            }
            set
            {
                if (value != rating)
                {
                    rating = value;
                    OnPropertyChanged("Rating");
                }

            }
        }
        public TourRatingDto()
        {

        }
        public TourRatingDto(TourRating tourRating)
        {
            Id = tourRating.Id;
            TourGuestId = tourRating.TourGuestId;
            Rating = tourRating.Rating;
            Comment = tourRating.Comment;
            if (tourRating.Valid)
            {
                Valid ="VALID";
            }
            else
            {
                Valid = "NOT VALID";
                ValidityChecked = true;
            }

        }
        public TourRating ToTourRating()
        {
            if(Valid.Equals("VALID"))
            {
                return new TourRating(Id, Rating, TourGuestId, Comment,true);
            }
            else
            {
                return new TourRating(Id, Rating, TourGuestId, Comment,false);
            }
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
