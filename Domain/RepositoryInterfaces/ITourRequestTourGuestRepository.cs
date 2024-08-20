using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRequestTourGuestRepository
    {
        List<TourRequestTourGuest> GetAll();
        TourRequestTourGuest GetById(int id);
        int NextId();
        TourRequestTourGuest Add(TourRequestTourGuest tourGuest);
        List<TourRequestTourGuest> GetByTourRequestId(int tourReservationId);
        TourRequestTourGuest Update(TourRequestTourGuest tourGuest);
        void Delete(TourRequestTourGuest tourGuest);
        void Subscribe(IObserver observer);
    }
}
