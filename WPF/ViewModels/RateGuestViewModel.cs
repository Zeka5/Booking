using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels
{
    public class RateGuestViewModel
    {
        private RateGuestService rateGuestService;
        private RatingNotificationService notificationService;
        private RatingNotificationDto notificationDto;
        public RatingDto Rating { get; set; }

        public RateGuestViewModel(RatingNotificationDto notificationDto)
        {
            rateGuestService = new RateGuestService(Injector.CreateInstance<IRatingRepository>());
            notificationService = new RatingNotificationService(Injector.CreateInstance<IRatingNotificationRepository>());
            this.notificationDto = notificationDto;
            Rating = new RatingDto();
            Rating.ReservationId = notificationDto.ReservationId;
        }

        public bool Submit()
        {
            if (!Rating.ValidateValues()) return false;
            rateGuestService.AddRating(Rating.ToRating());
            notificationService.Delete(notificationDto.Id);
            return true;
        }
    }
}
