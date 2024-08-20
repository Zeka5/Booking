using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class ReviewsService
    {
        private IAccommodationRatingRepository accommodationRatingRepository;
        private IAccommodationReservationRepository accommodationReservationRepository;
        private IAccommodationRepository accommodationRepository;
        private IImageRepository imageRepository;
        private IUserRepository userRepository;
        private IRatingRepository ratingRepository;
        private int ownerId;
        private const string defaultImagePath = "../../../Resources/Images/account.jpg";

        public ReviewsService(int ownerId)
        {
            accommodationRatingRepository = Injector.CreateInstance<IAccommodationRatingRepository>();
            accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            imageRepository = Injector.CreateInstance<IImageRepository>();
            userRepository = Injector.CreateInstance<IUserRepository>();
            ratingRepository = Injector.CreateInstance<IRatingRepository>();
            this.ownerId = ownerId;
        }

        private AccommodationRatingDto? ToDto(AccommodationRating review)
        {
            var reservation = accommodationReservationRepository.GetById(review.AccommodationReservationId);
            var accommodation = accommodationRepository.GetById(reservation.AccommodationId);
            var rating = ratingRepository.GetByReservationId(reservation.Id);
            if (accommodation.OwnerId != ownerId || rating is null) return null;

            var image = imageRepository.GetFirstByEntityAndType(accommodation.Id, ResourceType.Accommodation);
            string guestUsername = userRepository.GetById(reservation.UserId).Username;
            return new AccommodationRatingDto(review.Id, reservation.Id, review.Cleanliness, review.OwnerCorrectness,
                review.AdditionalComment, (image is null) ? defaultImagePath : image.Path, guestUsername,
                reservation.LastDay.ToString("dd/MM/yyyy"));
        }

        public void Load(ObservableCollection<AccommodationRatingDto> reviews)
        {
            reviews.Clear();
            foreach (var review in accommodationRatingRepository.GetAll())
            {
                var reviewDto = ToDto(review);
                if (reviewDto is null) continue;
                reviews.Add(reviewDto);
            }
                
        }
    }
}
