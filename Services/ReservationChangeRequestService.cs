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
    public class ReservationChangeRequestService
    {
        private IReservationChangeRequestRepository reservationChangeRequestRepository;
        private AccommodationReservationService accommodationReservationService;
        private RatingNotificationService ratingNotificationService;

        public ReservationChangeRequestService(IReservationChangeRequestRepository reservationChangeRequestRepository)
        {
            this.reservationChangeRequestRepository = reservationChangeRequestRepository;
        }

        public ReservationChangeRequestService(
            IReservationChangeRequestRepository reservationChangeRequestRepository,
            AccommodationReservationService accommodationReservationService)
        {
            this.reservationChangeRequestRepository = reservationChangeRequestRepository;
            this.accommodationReservationService = accommodationReservationService;
        }

        public ReservationChangeRequestService(
            IReservationChangeRequestRepository reservationChangeRequestRepository,
            AccommodationReservationService accommodationReservationService,
            RatingNotificationService ratingNotificationService)
        {
            this.reservationChangeRequestRepository = reservationChangeRequestRepository;
            this.accommodationReservationService = accommodationReservationService;
            this.ratingNotificationService = ratingNotificationService;
        }
        public void Add(ReservationChangeRequest reservationChangeRequest)
        {
            reservationChangeRequestRepository.Add(reservationChangeRequest);
        }
        public List<ReservationChangeRequest> GetAllByUser(User user)
        {
            List<ReservationChangeRequest> reservationChangeRequests = reservationChangeRequestRepository.GetAll();
            List<ReservationChangeRequest> result = new List<ReservationChangeRequest>();
            foreach (ReservationChangeRequest reservationChangeRequest in reservationChangeRequests)
            {
                AccommodationReservation? accommodationReservation = accommodationReservationService.GetById(reservationChangeRequest.AccommodationReservationId);
                if (accommodationReservation.UserId == user.Id)
                {
                    result.Add(reservationChangeRequest);
                }
            }
            return result;
        }
        public bool IsClickable(int id) { 
            return reservationChangeRequestRepository.IsClickable(id);
        }
        // public void UpdateIsClickable(ObservableCollection<AccommodationReservationDto>upcomingReservations) {
         //   foreach(AccommodationReservationDto accommodationReservationDto in upcomingReservations)
           // {
             //   accommodationReservationDto.IsClicked = IsClickable(accommodationReservationDto.Id);
            //}
        //}

        public IEnumerable<ReservationChangeRequest> GetAllWaiting()
        {
            return reservationChangeRequestRepository.GetAll().Where(request => request.IsWaiting());
        }

        public bool BelongsToOwner(int reservationId, int ownerId)
        {
            return accommodationReservationService.BelongsToOwner(reservationId, ownerId);
        }

        public Accommodation GetAccommodationByReservationId(int id)
        {
            return accommodationReservationService.GetAccommodationByReservationId(id);
        }

        public Image GetImageByEntityIdAndType(int id, ResourceType type)
        {
            return accommodationReservationService.GetImageByEntityIdAndType(id, type);
        }

        public void Accept(ReservationChangeRequest request)
        {
            var reservations = accommodationReservationService.GetOverlappingReservations(request);
            foreach (var res in reservations)
            {
                if (res.Id == request.AccommodationReservationId) continue;
                accommodationReservationService.Update(res.ModifyDates(request.FirstDay, request.LastDay));
            }
            var reservation = accommodationReservationService.GetById(request.AccommodationReservationId);
            reservation.FirstDay = request.FirstDay;
            reservation.LastDay = request.LastDay;
            reservation.DaysToStay = (reservation.LastDay - reservation.FirstDay).Days;
            accommodationReservationService.Update(reservation);
            

            request.Status = Status.Accepted;
            reservationChangeRequestRepository.Update(request);
        }

        public void Decline(ReservationChangeRequest request)
        {
            request.Status = Status.Declined;
            reservationChangeRequestRepository.Update(request);
        }

        public void RequestsSubscribe(IObserver observer)
        {
            reservationChangeRequestRepository.Subscribe(observer);
        }

        public void NotifyRatingNotification()
        {
            ratingNotificationService.NotifyRatingNotifications();
        }

        public bool IsOverlapping(ReservationChangeRequest request)
        {
            return (!accommodationReservationService.GetOverlappingReservations(request).Any()) ? true : false;
        }

        public int RequestsCountByReservationId(int id)
        {
            return reservationChangeRequestRepository.RequestCountByReservationId(id);
        }
    }
}
