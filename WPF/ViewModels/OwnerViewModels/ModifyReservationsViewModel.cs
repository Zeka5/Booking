using BookingApp.Commands;
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
using System.Windows.Controls;
using System.Windows.Input;

namespace BookingApp.WPF.ViewModels.OwnerViewModels
{
    public class ModifyReservationsViewModel : ViewModelBase, IObserver
    {
        private ReservationChangeRequestService requestService;
        private GuestNotificationService notificationService;
        private int ownerId;

        public ICommand AcceptRequest { get; }
        public ICommand DeclineRequest { get; }

        public ObservableCollection<ReservationChangeRequestDto> Requests { get; set; }

        public ModifyReservationsViewModel(int ownerId)
        {
            requestService =
                new ReservationChangeRequestService
                (
                    Injector.CreateInstance<IReservationChangeRequestRepository>(),
                    new AccommodationReservationService
                    (
                        Injector.CreateInstance<IAccommodationReservationRepository>(),
                        new AccommodationService
                        (
                            Injector.CreateInstance<IAccommodationRepository>(),
                            new LocationService(Injector.CreateInstance<ILocationRepository>()),
                            new ImageService(Injector.CreateInstance<IImageRepository>())
                        )
                    ),
                    new RatingNotificationService(Injector.CreateInstance<IRatingNotificationRepository>())
                );
            notificationService = new GuestNotificationService(Injector.CreateInstance<IGuestNotificationRepository>());
            this.ownerId = ownerId;

            requestService.RequestsSubscribe(this);
            Requests = new ObservableCollection<ReservationChangeRequestDto>();

            AcceptRequest = new AcceptModificationRequestCommand(this);
            DeclineRequest = new DeclineModificationRequestCommand(this);
            
            Update();
        }

        public void Update()
        {
            Requests.Clear();
            foreach (var request in requestService.GetAllWaiting())
            {
                if (!requestService.BelongsToOwner(request.AccommodationReservationId, ownerId)) continue;
                var accommodation = requestService.GetAccommodationByReservationId(request.AccommodationReservationId);

                Domain.Model.Image? image = requestService
                    .GetImageByEntityIdAndType(accommodation.Id, ResourceType.Accommodation);
                bool isFree = requestService.IsOverlapping(request);
                Requests.Add(new ReservationChangeRequestDto(request.Id, request.AccommodationReservationId, request.FirstDay,
                    request.LastDay, request.Status, request.Comment,
                    image is null ? Domain.Model.Image.defaultAccommodationImagePath : image.Path, isFree));
            }
        }

        public void Accept(ReservationChangeRequest reservationChangeRequest)
        {
            requestService.Accept(reservationChangeRequest);
            requestService.NotifyRatingNotification();
            // notifies guest that request is served
            notificationService.Add(new GuestNotification(reservationChangeRequest));
        }

        public void Decline(ReservationChangeRequest reservationChangeRequest)
        {
            requestService.Decline(reservationChangeRequest);
            // notifies guest that request is served
            notificationService.Add(new GuestNotification(reservationChangeRequest));
        }
    }
}
