using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourReservationService
    {
        private ITourReservationRepository tourReservationRepository;
        public TourReservationService(ITourReservationRepository tourReservationRepository)
        {
            this.tourReservationRepository = tourReservationRepository;
        }

        public void Add(TourReservation tourReservation)
        {
            tourReservationRepository.Add(tourReservation);
        }

        public List<TourReservation> GetAll()
        {
            return tourReservationRepository.GetAll();
        }
        public TourReservation GetByTourRelizationIdAndUserId(int tourRealizationId, int userId)
        {
            return tourReservationRepository.GetByTourRelizationIdAndUserId(tourRealizationId, userId);
        }
        public TourReservation GetById(int id)
        { 
            return tourReservationRepository.GetById(id);
        }

        public TourReservation GetByTourRealization(int id)
        {
            return tourReservationRepository.GetByTourStartTimeId(id);
        }
        public bool DoesTourReservationExist(int tourRealizationid)
        {
            TourReservation tourReservation = tourReservationRepository.GetByTourStartTimeId(tourRealizationid);
            if (tourReservation == null)
            {
                return false;
            }
            return true;
        }
        public void Update(TourReservation tourReservation)
        {
            tourReservationRepository.Update(tourReservation);
        }

        public int GetLastId()
        {
            return tourReservationRepository.GetAll()[^1].Id;
        }
    }
}
