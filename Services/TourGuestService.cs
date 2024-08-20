using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourGuestService
    {
        private ITourGuestRepository tourGuestRepository;
        private TourReservationService tourReservationService;

        public TourGuestService(ITourGuestRepository tourGuestRepository, TourReservationService tourReservationService) {
            this.tourGuestRepository = tourGuestRepository;
            this.tourReservationService = tourReservationService;
        }

        public List<TourGuest> GetTourGuests(int tourRealizationId)
        {
            List<TourGuest> tourGuests=new List<TourGuest>();
            foreach(var tourGuest in tourGuestRepository.GetAll())
            {
                TourReservation tourReservation = tourReservationService.GetById(tourGuest.TourReservationId);
                if(tourReservation.TourRealizationId== tourRealizationId && !tourGuest.CheckPointName.Equals("Not arrived"))
                {
                    tourGuests.Add(tourGuest);
                }
            }
            return tourGuests;
        }

        public List<TourGuest> GetRemainingTourists(int tourRealizationId)
        {
            List<TourGuest> tourGuests= new List<TourGuest>();
            foreach (var tourGuest in tourGuestRepository.GetAll())
            {
                TourReservation tourReservation = tourReservationService.GetById(tourGuest.TourReservationId);
                if (tourReservation.TourRealizationId == tourRealizationId && tourGuest.CheckPointName.Equals("Not arrived"))
                {
                    tourGuests.Add(tourGuest);
                }
            }
            return tourGuests;
        }

        public TourReservation GetTourReservationById(int TourGuestid)
        {
            TourGuest tourGuest=tourGuestRepository.GetById(TourGuestid);
            return tourReservationService.GetById(tourGuest.TourReservationId);
        }

        public TourGuest GetById(int id) {
            return tourGuestRepository.GetById(id);
        }

        public void Update(TourGuestDto tourGuestDto,string checkPointName)
        {
            TourGuest tourGuest = tourGuestDto.toTourGuest();
            tourGuest.CheckPointName= checkPointName;
            tourGuestRepository.Update(tourGuest);
        }
        public List<TourGuest> GetByTourReservationId(int tourReservationId)
        {
            return tourGuestRepository.GetByTourReservationId(tourReservationId);
        }

        public void Add(TourGuest tourGuest)
        {
            tourGuestRepository.Add(tourGuest);
        }
    }
}
