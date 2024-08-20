using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class ReviewsViewModel : ViewModelBase, IObserver
    {
        private AccommodationRatingService accommodationRatingService;
        private UserService userService;

        public ObservableCollection<AccommodationRatingDto> Reviews { get; set; }

        public ReviewsViewModel(int ownerId)
        {
            accommodationRatingService =
                new AccommodationRatingService
                (
                    Injector.CreateInstance<IAccommodationRatingRepository>(),
                    new RateGuestService
                    (
                        Injector.CreateInstance<IRatingRepository>(),
                        new AccommodationReservationService
                        (
                            Injector.CreateInstance<IAccommodationReservationRepository>(),
                            new AccommodationService
                            (
                                Injector.CreateInstance<IAccommodationRepository>(),
                                new LocationService(Injector.CreateInstance<ILocationRepository>()),
                                new ImageService(Injector.CreateInstance<IImageRepository>())
                            )
                        )
                    )
                );
            userService = new UserService(Injector.CreateInstance<IUserRepository>());

            accommodationRatingService.GuestRatingSubscribe(this);
            Reviews = new ObservableCollection<AccommodationRatingDto>();
            Update();
        }

        public void Update()
        {
            Reviews.Clear();
            foreach (var rating in accommodationRatingService.GetAllMutual())
            {
                var reservation = accommodationRatingService.GetReservationById(rating.AccommodationReservationId);
                var image = accommodationRatingService.GetImageByReservationId(reservation.Id);
                string guestUsername = userService.GetById(reservation.UserId).Username;
                Reviews.Add(new AccommodationRatingDto(rating.Id, rating.AccommodationReservationId, rating.Cleanliness,
                    rating.OwnerCorrectness, rating.AdditionalComment,
                    image is null ? Image.defaultAccommodationImagePath : image.Path, guestUsername,
                    reservation.LastDay.ToString("dd/MM/yyyy")));
            }
        }
    }
}
