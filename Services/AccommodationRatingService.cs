using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class AccommodationRatingService
    {
        private IAccommodationRatingRepository accommodationRatingRepository;
        private RateGuestService rateGuestService;
        private AccommodationReservationService accommodationReservationService;
        private readonly int ownerId;

        public AccommodationRatingService(IAccommodationRatingRepository accommodationRatingRepository)
        {
            this.accommodationRatingRepository = accommodationRatingRepository;
        }
        public AccommodationRatingService(IAccommodationRatingRepository accommodationRatingRepository,
            AccommodationReservationService accommodationReservationService,
            int ownerId)
        {
            this.accommodationRatingRepository = accommodationRatingRepository;
            this.accommodationReservationService = accommodationReservationService;
            this.ownerId = ownerId;
        }

        public AccommodationRatingService(
            IAccommodationRatingRepository accommodationRatingRepository,
            RateGuestService rateGuestService) { 
            this.accommodationRatingRepository = accommodationRatingRepository;
            this.rateGuestService = rateGuestService;
        }

        public void Add(AccommodationRating accommodationRating)
        {
            accommodationRatingRepository.Add(accommodationRating);
        }

        public bool GetIsRateable(AccommodationReservation accommodationReservation)
        {
            return accommodationRatingRepository.GetIsRateable(accommodationReservation);
        }

        public IEnumerable<AccommodationRating> GetAllMutual()
        {
            return accommodationRatingRepository.GetAll()
                .Where(rating => rateGuestService.RatingExists(rating.AccommodationReservationId));
        }

        public AccommodationReservation GetReservationById(int id)
        {
            return rateGuestService.GetReservationById(id);
        }

        public Image GetImageByReservationId(int id)
        {
            return rateGuestService.GetImageByReservationId(id);
        }

        public void GuestRatingSubscribe(IObserver observer)
        {
            rateGuestService.Subscribe(observer);
        }

        public List<Rating> SortRatings(List<Rating> ratings) {
            List<Rating> result = new List<Rating>();
            List<AccommodationRating> accommodationRatings = accommodationRatingRepository.GetAll();
            foreach (Rating rating in ratings) {
                if (accommodationRatings.Any(x => x.AccommodationReservationId == rating.ReservationId)) {
                    result.Add(rating);
                }
            }
            return result;
        }

        public (bool, int, double) IsSuperOwner(ref int totalRatings, ref double averageRating)
        {
            totalRatings = 0;
            averageRating = 0;

            foreach (var rating in accommodationRatingRepository.GetAll())
            {
                if (!accommodationReservationService.BelongsToOwner(rating.AccommodationReservationId, ownerId)) continue;
                totalRatings++;
                averageRating += (rating.OwnerCorrectness + rating.Cleanliness) / 2;
            }

            averageRating = averageRating / totalRatings;
            return ((totalRatings > 5 && averageRating >= 4.5) ? true : false, totalRatings, averageRating);
        }

        public void SubscribeSuperOwner(IObserver observer)
        {
            accommodationRatingRepository.Subscribe(observer);
        }
    }
}
