using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Dto
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }

        private int cleanliness;
        public int Cleanliness
        {
            get { return cleanliness; }
            set
            {
                if (value != cleanliness)
                {
                    cleanliness = value;
                    OnPropertyChanged();
                }
            }
        }

        private int ruleFollowing;
        public int RuleFollowing
        {
            get { return ruleFollowing; }
            set
            {
                if (value != ruleFollowing)
                {
                    ruleFollowing = value;
                    OnPropertyChanged();
                }
            }
        }

        private string additionalComment;
        public string AdditionalComment
        {
            get { return additionalComment; }
            set
            {
                if (value != additionalComment)
                {
                    additionalComment = value;
                    OnPropertyChanged();
                }
            }
        }
        private string accommodationName;
        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                if (value != accommodationName)
                {
                    accommodationName = value;
                    OnPropertyChanged();
                }
            }
        }

        public RatingDto() { }

        public RatingDto(Rating rating , Accommodation accommodation)
        {
            Id = rating.Id;
            ReservationId = rating.ReservationId;
            Cleanliness = rating.Cleanliness;
            RuleFollowing = rating.RuleFollowing;
            AdditionalComment = rating.AdditionalComment;
            AccommodationName = accommodation.Name;
        }

        public Rating ToRating()
        {
            return new Rating(ReservationId, Cleanliness, RuleFollowing, AdditionalComment);
        }

        public bool ValidateValues()
        {
            if (cleanliness == 0 || ruleFollowing == 0) return false;
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
