using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class RateGuestService
    {
        private IRatingRepository ratingRepository;
        private AccommodationReservationService reservationService;

        public RateGuestService(IRatingRepository ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        public RateGuestService(IRatingRepository ratingRepository, AccommodationReservationService reservationService)
        {
            this.ratingRepository = ratingRepository;
            this.reservationService = reservationService;
        }   

        public void AddRating(Rating rating)
        {
            ratingRepository.Add(rating);
        }

        public bool RatingExists(int reservationId)
        {
            return (ratingRepository.GetAll().FirstOrDefault(rating => rating.ReservationId == reservationId) != null);
        }

        public AccommodationReservation GetReservationById(int id)
        {
            return reservationService.GetById(id);
        }

        public Image GetImageByReservationId(int id)
        {
            return reservationService
                .GetImageByEntityIdAndType(reservationService.GetAccommodationByReservationId(id).Id, ResourceType.Accommodation);
        }

        public void Subscribe(IObserver observer)
        {
            ratingRepository.Subscribe(observer);
        }
        public List<Rating> GetAllByUser(int userId) {
            List<Rating> ratings = ratingRepository.GetAll();
            List<Rating> result = new List<Rating>();
            foreach(Rating rating in ratings) {
                AccommodationReservation? accommodationReservation = reservationService.GetById(rating.ReservationId);
                if (accommodationReservation.UserId == userId) { 
                    result.Add(rating);
                }
            }
            return result;
        }
    }
}
