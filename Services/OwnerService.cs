using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.WPF.View;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class OwnerService
    {
        private IAccommodationRatingRepository accommodationRatingRepository;
        private IAccommodationRepository accommodationRepository;
        private IAccommodationReservationRepository accommodationReservationRepository;
        private int ownerId;

        public OwnerService(int ownerId)
        {
            accommodationRatingRepository = Injector.CreateInstance<IAccommodationRatingRepository>();
            accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            this.ownerId = ownerId;
        }

        public void Subscribe(IObserver observer)
        {
            accommodationRatingRepository.Subscribe(observer);
        }

        private bool ThisOwnerRating(AccommodationRating rating)
        {
            var reservation = accommodationReservationRepository.GetById(rating.AccommodationReservationId);
            var accommodation = accommodationRepository.GetById(reservation.AccommodationId);
            if (accommodation.OwnerId != ownerId) return false;
            return true;
        }

        public void Update(ref int totalRatings, ref double averageRating)
        {
            totalRatings = 0;
            averageRating = 0;

            double sum = 0;
            foreach (var rating in accommodationRatingRepository.GetAll())
            {
                if (!ThisOwnerRating(rating)) continue;
                totalRatings++;
                sum += (rating.OwnerCorrectness + rating.Cleanliness) / 2;
            }
            averageRating = sum / totalRatings;
        }
    }
}
