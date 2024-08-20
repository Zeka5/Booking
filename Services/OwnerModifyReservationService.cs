using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class OwnerModifyReservationService
    {
        private int ownerId;
        private IReservationChangeRequestRepository reservationChangeRequestRepository;
        private IAccommodationReservationRepository accommodationReservationRepository;
        private IAccommodationRepository accommodationRepository;
        private IImageRepository imageRepository;
        private IGuestNotificationRepository guestNotificationRepository;
        private const string defaultImagePath = "../../../Resources/Images/account.jpg";

        public OwnerModifyReservationService(int ownerId)
        {
            this.ownerId = ownerId;
            reservationChangeRequestRepository = Injector.CreateInstance<IReservationChangeRequestRepository>();
            accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            imageRepository = Injector.CreateInstance<IImageRepository>();
            guestNotificationRepository = Injector.CreateInstance<IGuestNotificationRepository>();
        }

        private ReservationChangeRequestDto? ToDto(ReservationChangeRequest request)
        {
            var reservation = accommodationReservationRepository.GetById(request.AccommodationReservationId);
            var accommodation = accommodationRepository.GetById(reservation.AccommodationId);
            if (accommodation.OwnerId != ownerId) return null;
            var image = imageRepository.GetFirstByEntityAndType(accommodation.Id, ResourceType.Accommodation);
            return new ReservationChangeRequestDto(request.Id, request.AccommodationReservationId, request.FirstDay, request.LastDay,
                request.Status, request.Comment, (image is null) ? defaultImagePath : image.Path, false);
        }

        public void Update(ObservableCollection<ReservationChangeRequestDto> requests)
        {
            requests.Clear();
            foreach (var request in reservationChangeRequestRepository.GetAll())
            {
                if (request.Status != Status.Waiting) continue;
                var requestDto = ToDto(request);
                if (requestDto is null) continue;
                requests.Add(requestDto);
            }
        }

        public void Confirm(ReservationChangeRequestDto selectedRequest)
        {
            var reservation = accommodationReservationRepository.GetById(selectedRequest.AccommodationReservationId);
            reservation.FirstDay = selectedRequest.FirstDay;
            reservation.LastDay = selectedRequest.LastDay;
            reservation.DaysToStay = (reservation.LastDay - reservation.FirstDay).Days;
            accommodationReservationRepository.Update(reservation);
            selectedRequest.Status = Status.Accepted;
            reservationChangeRequestRepository.Update(selectedRequest.ToReservationChangeRequest());
            guestNotificationRepository.Add(new GuestNotification(selectedRequest.ToReservationChangeRequest()));
        }

        public void Reject(ReservationChangeRequestDto selectedRequest)
        {
            selectedRequest.Status = Status.Declined;
            reservationChangeRequestRepository.Update(selectedRequest.ToReservationChangeRequest());
            guestNotificationRepository.Add(new GuestNotification(selectedRequest.ToReservationChangeRequest()));
        }

        public void SubscribeRequests(IObserver observer)
        {
            reservationChangeRequestRepository.Subscribe(observer);
        }

        public static DateTime GetDateTime(int reservationId)
        {
            var reservation = Injector.CreateInstance<IAccommodationReservationRepository>().GetById(reservationId);
            return reservation.FirstDay;
        }
    }
}
