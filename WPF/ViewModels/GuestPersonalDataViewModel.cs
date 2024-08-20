using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace BookingApp.WPF.ViewModels
{
    public class GuestPersonalDataViewModel
    {
        public List<RatingDto> Ratings { get; set; }
        public GuestDto GuestDto { get; set; }
        public NavigationService NavigationService { get; set; }
        public AccommodationReservationService AccommodationReservationService { get; set; }
        public RateGuestService RateGuestService { get; set; }
        public GuestService GuestService { get; set; }
        public AccommodationRatingService AccommodationRatingService { get; set; }
        private User user;
        public GuestPersonalDataViewModel(User user, NavigationService navigationService) { 
            this.user = user;
            NavigationService = navigationService;
            Ratings = new List<RatingDto>();
            GuestDto = new GuestDto();
            LoadServices();
            LoadRatings();
            LoadGuestInformation();
        }
        private void LoadGuestInformation()
        {
            Guest guest = GuestService.getById(user.Id);
            if (guest.Mode == Mode.SuperGuest) {
                guest = GuestService.getGuestMode(guest);
            }
            if (guest.Mode == Mode.Guest) {
                guest = AccommodationReservationService.GetGuestMode(guest);
            }
            GuestService.Update(guest);
            GuestDto = new GuestDto(guest);
        }
        private void LoadRatings()
        {
            List<Rating> ratings = RateGuestService.GetAllByUser(user.Id);
            ratings = AccommodationRatingService.SortRatings(ratings);
            foreach(Rating rating in ratings) {
                AccommodationReservation? accommodationReservation = AccommodationReservationService.GetById(rating.Id);
                Accommodation? accommodation = AccommodationReservationService.GetAccommodationById(accommodationReservation.Id);
                Ratings.Add(new RatingDto(rating , accommodation));
            }
        }
        private void LoadServices() {
            var locationService = new LocationService(Injector.CreateInstance<ILocationRepository>());
            var imageService = new ImageService(Injector.CreateInstance<IImageRepository>());
            var accommodationService = new AccommodationService(Injector.CreateInstance<IAccommodationRepository>(),locationService,imageService);
            AccommodationReservationService = new AccommodationReservationService(Injector.CreateInstance<IAccommodationReservationRepository>(),accommodationService);
            RateGuestService = new RateGuestService(Injector.CreateInstance<IRatingRepository>(),AccommodationReservationService);
            AccommodationRatingService = new AccommodationRatingService(Injector.CreateInstance<IAccommodationRatingRepository>());
            GuestService = new GuestService(Injector.CreateInstance<IGuestRepository>());
        }
    }
}
