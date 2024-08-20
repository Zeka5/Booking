using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourRequestTourGuestService
    {
        private ITourRequestTourGuestRepository tourRequestTourGuestRepository;

        public TourRequestTourGuestService(ITourRequestTourGuestRepository tourRequestTourGuestRepository)
        {
            this.tourRequestTourGuestRepository = tourRequestTourGuestRepository;
        }
        public void Add(TourRequestTourGuest tourRequestTourGuest)
        {
            tourRequestTourGuestRepository.Add(tourRequestTourGuest);
        }

        public List<TourRequestTourGuest> GetAllByTourRequestId(int id) 
        { 
            return tourRequestTourGuestRepository.GetAll().FindAll(trtg => trtg.TourRequestId == id);
        }
    }
}
