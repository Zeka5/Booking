using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.Repository;
using BookingApp.WPF.View.Guest;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class AccommodationReservationService
    {
        public int DaysToStay { get; set; }
        public int AccommodationId { get; set; }
        private IAccommodationReservationRepository accommodationReservationRepository;
        private AccommodationService accommodationService;

        public AccommodationReservationService(IAccommodationReservationRepository accommodationReservationRepository)
        {
            this.accommodationReservationRepository = accommodationReservationRepository;
        }


        public AccommodationReservationService(IAccommodationReservationRepository accommodationReservationRepository, AccommodationService accommodationService)
        {
            this.accommodationReservationRepository = accommodationReservationRepository;
            this.accommodationService = accommodationService;
        }

        public Accommodation? GetAccommodationById(int accommodationId)
        {
            return accommodationService.GetAccommodationById(accommodationId);
        }

        public void Add(AccommodationReservation accommodationReservation)
        {
            accommodationReservationRepository.Add(accommodationReservation);
        }

        public void Update(AccommodationReservation reservation)
        {
            accommodationReservationRepository.Update(reservation);
        }

        public AccommodationReservation? GetById(int id)
        {
            return accommodationReservationRepository.GetById(id);
        }

        public List<DateTime> GetFreeDates(DateTime beginDate, DateTime endDate)
        {
            List<AccommodationReservation> accommodationReservations = accommodationReservationRepository.GetAllActiveById(AccommodationId);
            List<DateTime> freeDates = new List<DateTime>();

            while (beginDate <= endDate)
            {
                int overlapCounter = AccommodationReservation.GetOverlapCounter(accommodationReservations, beginDate);
                if (overlapCounter == 0)
                {
                    freeDates.Add(beginDate);
                }
                beginDate = beginDate.AddDays(1);
            }
            return freeDates;
        }

        public (List<ReservationDate>, bool) GetReservationOptions(List<DateTime> freeDates, DateTime rangeBegin, DateTime rangeEnd)
        {
            List<ReservationDate> reservationOptions = GetReservationOptionsInRange(freeDates, rangeEnd);
            bool messageBoxFlag = false;
            if (reservationOptions.Count != 0) return (reservationOptions, messageBoxFlag);
            messageBoxFlag = true;

            reservationOptions = GetReservationOptionsOutRange(reservationOptions, rangeBegin, rangeEnd);
            return (reservationOptions, messageBoxFlag);
        }

        private List<ReservationDate> GetReservationOptionsOutRange(List<ReservationDate> reservationOptions, DateTime rangeBegin, DateTime rangeEnd)
        {
            while (reservationOptions.Count() <= 4)
            {
                rangeEnd = rangeEnd.AddDays(1);

                if (rangeBegin > DateTime.Today)
                {
                    rangeBegin = rangeBegin.AddDays(-1);
                }
                List<DateTime> freeDates = GetFreeDates(rangeBegin, rangeEnd);

                reservationOptions = GetReservationOptionsInRange(freeDates, rangeEnd);
            }
            return reservationOptions;
        }

        private List<ReservationDate> GetReservationOptionsInRange(List<DateTime> freeDates, DateTime rangeEnd)
        {
            List<ReservationDate> reservationOptions = new List<ReservationDate>();

            foreach (DateTime dateTime in freeDates)
            {
                bool addReservationFlag = IsReservable(dateTime, freeDates);
                if (addReservationFlag && dateTime.AddDays(DaysToStay - 1) <= rangeEnd)
                {
                    ReservationDate reservationDate = new ReservationDate(dateTime, dateTime.AddDays(DaysToStay - 1));
                    reservationOptions.Add(reservationDate);
                }
            }
            return reservationOptions;
        }

        private bool IsReservable(DateTime dateTime, List<DateTime> freeDates)
        {
            DateTime nextDateTime;
            DateTime freeDate;

            for (int i = 1; i < DaysToStay; i++)
            {
                nextDateTime = dateTime.AddDays(i);
                freeDate = freeDates.Find(date => date == nextDateTime);
                if (freeDate != nextDateTime)
                {
                    return false;
                }
            }
            return true;
        }

        public void CancelReservationClick(AccommodationReservation accommodationReservation)
        {
            accommodationReservationRepository.Delete(accommodationReservation);
        }

        public void Subscribe(IObserver observer)
        {
            accommodationReservationRepository.Subscribe(observer);
        }

        public List<AccommodationReservation> GetUpcomingReservations(User user)
        {
            return accommodationReservationRepository.GetUpcomingReservations(user);
        }

        public Accommodation GetAccommodationByReservationId(int id)
        {
            var reservation = accommodationReservationRepository.GetById(id);
            return accommodationService.GetAccommodationById(reservation.AccommodationId);
        }

        public bool BelongsToOwner(int reservationId, int ownerId)
        {
            return (GetAccommodationByReservationId(reservationId).OwnerId != ownerId) ? false : true;
        }

        public IEnumerable<AccommodationReservation> GetActiveReservationsByOwnerId(int id)
        {
            var accommodations = accommodationService.GetAccommodationsByOwnerId(id);
            return accommodationReservationRepository
                .GetByAccommodationIds(accommodations.Select(accommodation => accommodation.Id))
                .Where(reservation => reservation.Status == ReservationStatus.Active);
        }

        public Image GetImageByEntityIdAndType(int id, ResourceType type)
        {
            return accommodationService.GetImageByEntityIdAndType(id, type);
        }

        public List<AccommodationReservation> GetPastReservations(User user)
        {
            return accommodationReservationRepository.GetPastReservations(user);
        }

        public IEnumerable<AccommodationReservation> GetOverlappingReservations(ReservationChangeRequest request)
        {
            var accommodation = GetAccommodationByReservationId(request.AccommodationReservationId);
            // dobavi sve reservacije koje se preklapaju sa request-om na bilo koji nacin, samo deo || request preklapa celu rezervaciju
            // || request unutar rezervacije
            return accommodationReservationRepository.GetAll().Where(reservation =>
                (reservation.AccommodationId == accommodation.Id) &&
                (reservation.Status == ReservationStatus.Active) &&
                ((((reservation.FirstDay >= request.FirstDay) && (reservation.FirstDay <= request.LastDay)) ||
                    ((reservation.LastDay >= request.FirstDay) && (reservation.LastDay <= request.LastDay))) ||
                (request.FirstDay >= reservation.FirstDay && request.LastDay <= reservation.LastDay))
            );
        }

        public Guest GetGuestMode(Guest guest) {
            var counter = 0;
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddYears(-1);
            foreach (AccommodationReservation accommodationReservation in accommodationReservationRepository.GetAll()) {
                if (accommodationReservation.UserId == guest.Id && accommodationReservation.FirstDay > dateTime) {
                    counter++;
                }
            }
            if (counter >= 10)
            {
                guest.Mode = Mode.SuperGuest;
                guest.BonusPoints = 5;
                guest.SuperGuestConfigured = DateTime.Now;
                return guest;
            }
            return guest;
        }

        public IEnumerable<AccommodationReservation> GetReservationsByAccommodationId(int id)
        {
            return accommodationReservationRepository.GetAll().Where(reservation => reservation.AccommodationId == id);
        }

        public IEnumerable<AccommodationReservation> GetReservationsByAccommodationIdAndYear(int id, int year)
        {
            return GetReservationsByAccommodationId(id).Where(reservation => reservation.FirstDay.Year == year);
        }

        public bool IsDateFree(DateTime date, int accommodationId)
        {
            return !GetReservationsByAccommodationId(accommodationId).Any(reservation =>
                (reservation.FirstDay <= date) && (reservation.LastDay >= date));
        }
    }
}
