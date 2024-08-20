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
    public class AccommodationRatingDto : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int accommodationReservationId;
        public int AccommodationReservationId { 
            get { return accommodationReservationId; }
            set {
                if (value != accommodationReservationId) { 
                accommodationReservationId = value;
                OnPropertyChanged(nameof(AccommodationReservationId));
                }
            }
        }
        private int cleanliness;
        public int Cleanliness
        {
            get { return cleanliness; }
            set
            {
                if (value != cleanliness)
                {
                    cleanliness = value;
                    OnPropertyChanged(nameof(Cleanliness));
                }
            }
        }
        private int ownerCorrectness;
        public int OwnerCorrectness
        {
            get { return ownerCorrectness; }
            set
            {
                if (value != ownerCorrectness)
                {
                    ownerCorrectness = value;
                    OnPropertyChanged(nameof(OwnerCorrectness));
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
                    OnPropertyChanged(nameof(AdditionalComment));
                }
            }
        }

        private string imagePath;
        public string ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    OnPropertyChanged();
                }
            }
        }

        private string guestUsername;
        public string GuestUsername
        {
            get { return guestUsername; }
            set
            {
                if (guestUsername != value)
                {
                    guestUsername = value;
                    OnPropertyChanged();
                }
            }
        }

        private string dateLeft;
        public string DateLeft
        {
            get { return dateLeft; }
            set
            {
                if (dateLeft != value)
                {
                    dateLeft = value;
                    OnPropertyChanged();
                }
            }
        }

        public AccommodationRatingDto() { }
        public AccommodationRatingDto(AccommodationRating accommodationRating) {
            accommodationReservationId = accommodationRating.AccommodationReservationId;
            additionalComment = accommodationRating.AdditionalComment;
            ownerCorrectness = accommodationRating.OwnerCorrectness;
            cleanliness = accommodationRating.Cleanliness;
        }

        public AccommodationRatingDto(int id, int accommodationReservationId, int cleanliness, int ownerCorrectness,
            string additionalComment, string imagePath, string guestUsername, string dateLeft)
        {
            Id = id;
            AccommodationReservationId = accommodationReservationId;
            Cleanliness = cleanliness;
            OwnerCorrectness = ownerCorrectness;
            AdditionalComment = additionalComment;
            ImagePath = imagePath;
            GuestUsername = guestUsername;
            DateLeft = dateLeft;
        }

        public AccommodationRating ToAccommodationRating() {
            return new AccommodationRating(accommodationReservationId,cleanliness,ownerCorrectness,additionalComment);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
