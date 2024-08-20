using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourRatingService
    {
        private ITourRatingRepository tourRatingRepository;
        private TourGuestService tourGuestService;

        public TourRatingService(ITourRatingRepository tourRatingRepository,TourGuestService tourGuestService) 
        {
            this.tourRatingRepository=tourRatingRepository;
            this.tourGuestService = tourGuestService;
        }

        public List<TourRating> GetAllByTourRealizationId(int id)
        {
            List<TourRating> tourRatings = new List<TourRating>();
            foreach(var tourRating in tourRatingRepository.GetAll())
            {
                int tourRealizationId = tourGuestService.GetTourReservationById(tourRating.TourGuestId).TourRealizationId;
                if (tourRealizationId == id)
                {
                    tourRatings.Add(tourRating);
                }
            }
            return tourRatings;
        }

        public double GetAverageGradeByTourRealizationId(int id)
        {
            double sumGrade = 0;
            int count = 0;
            foreach (var tourRating in tourRatingRepository.GetAll())
            {
                int tourRealizationId = tourGuestService.GetTourReservationById(tourRating.TourGuestId).TourRealizationId;
                if (tourRealizationId == id)
                {
                    sumGrade += tourRating.Rating;
                    count++;
                }
            }
            if (count == 0) return 0;
            return sumGrade/count;
        }

        public void UpdateValidity(TourRatingDto TourRating)
        {
            tourRatingRepository.Update(TourRating.ToTourRating());
        }
        public TourRating GetLast()
        {
            return tourRatingRepository.GetLast();
        }
        public void Add(TourRating tourRating)
        {
            tourRatingRepository.Add(tourRating);
        }
    }
}
