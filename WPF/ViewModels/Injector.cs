using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModels
{
    public class Injector
    {
        private static Dictionary<Type, object> implementations = new Dictionary<Type, object>
        {
            { typeof(IAccommodationRepository), new AccommodationRepository() },
            { typeof(IImageRepository), new ImageRepository() },
            { typeof(ILocationRepository), new LocationRepository() },
            { typeof(IAccommodationReservationRepository), new AccommodationReservationRepository()},
            { typeof(IAccommodationRatingRepository), new AccommodationRatingRepository() },
            { typeof(IReservationChangeRequestRepository), new ReservationChangeRequestRepository() },
            { typeof(IGuestNotificationRepository), new GuestNotificationRepository() },
            { typeof(IRatingNotificationRepository), new RatingNotificationRepository() },
            { typeof(IUserRepository), new UserRepository() },
            { typeof(IRatingRepository), new RatingRepository() },
            { typeof(ILanguageRepository), new LanguageRepository() },
            { typeof(ITourRepository), new TourRepository() },
            { typeof(ITourRealizationRepository), new TourRealizationRepository() },
            { typeof(ICheckPointRepository), new CheckPointRepository() },
            { typeof(ITourGuestRepository), new TourGuestRepository() },
            { typeof(ITourReservationRepository), new TourReservationRepository() },
            { typeof(IVoucherRepository), new VoucherRepository() },
            { typeof(ITourRatingRepository), new TourRatingRepository() },
            { typeof(IDriverRepository), new DriverRepository() },
            { typeof(IDriveRepository), new DriveRepository() },
            { typeof(IDriveReservationRepository), new DriveReservationRepository() },
            { typeof(IAddressRepository), new AddressRepository() },
            { typeof(IVehicleRepository), new VehicleRepository() },
            { typeof(IVehicleLocationsRepository), new VehicleLocationsRepository() },
            { typeof(IVehicleLanguageRepository), new VehicleLanguageRepository() },
            { typeof(ISuperPointsRepository), new SuperPointsRepository() },
            { typeof(IRenovateSuggestionRepository), new RenovateSuggestionRepository() },
            { typeof(IGuestRepository), new GuestRepository()},
            { typeof(IRenovationRepository), new RenovationRepository() },
            { typeof(ITourRequestRepository), new TourRequestRepository() },
            { typeof(ITourRequestTourGuestRepository), new TourRequestTourGuestRepository() },
            { typeof(ITourRequestNotificationRepository), new TourRequestNotificationRepository() },
            { typeof(IGuideRepository), new GuideRepository() },
            { typeof(IComplexTourRequestRepository), new ComplexTourRequestRepository() },
            { typeof(IGroupDriveReservationRepository), new GroupDriveReservationRepository() }
        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);
            
            if (implementations.ContainsKey(type))
            {
                return (T)implementations[type];
            }

            throw new ArgumentException($"No implementation found for type {type}");
        }
    }
}
