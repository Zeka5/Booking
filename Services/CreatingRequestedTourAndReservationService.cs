using BookingApp.Domain.Model;
using BookingApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class CreatingRequestedTourAndReservationService
    { 
        private SavingTourService savingTourService;
        private TourRealizationService tourRealizationService;
        private TourReservationService tourReservationService;
        private TourGuestService tourGuestService;
        private TourRequestTourGuestService tourRequestTourGuestService;

        public CreatingRequestedTourAndReservationService(SavingTourService savingTourService,TourRealizationService tourRealizationService,TourReservationService tourReservationService, TourGuestService tourGuestService, TourRequestTourGuestService tourRequestTourGuestService) 
        { 
            this.savingTourService = savingTourService;
            this.tourGuestService = tourGuestService;
            this.tourReservationService = tourReservationService;
            this.tourRequestTourGuestService = tourRequestTourGuestService;
            this.tourRealizationService = tourRealizationService;
        }

        public void CreateTourAndReserve(Tour tour, List<DateTime> tourStarts, List<CheckPoint> checkPoints, List<string> imagePaths,TourRequest tourRequest)
        {
            savingTourService.SaveTour(tour, tourStarts, checkPoints, imagePaths);
            CreateTourReservation(tourRequest, savingTourService.GetLastTourRealizationId());
        }

        private void CreateTourReservation(TourRequest tourRequest,int tourRealizationId)
        {
            tourReservationService.Add(new TourReservation(tourRequest.TouristId,tourRealizationId,tourRequest.NumberOfGuests));
            tourRealizationService.UpdateAvailability(tourRealizationId);
            SaveTourGuests(tourRequest.Id,tourReservationService.GetLastId());
        }

        private void SaveTourGuests(int tourRequestId,int tourReservationId)
        {
            List<TourRequestTourGuest> tourguests=tourRequestTourGuestService.GetAllByTourRequestId(tourRequestId);
            foreach(var tourguest in tourguests)
            {
                tourGuestService.Add(new TourGuest(tourguest.FullName, tourguest.Age, tourReservationId));
            }
        }
    }
}
