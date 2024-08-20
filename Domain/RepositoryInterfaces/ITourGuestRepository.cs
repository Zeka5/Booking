using BookingApp.Domain.Model;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourGuestRepository
    {
        List<TourGuest> GetAll();
        TourGuest GetById(int id);
        int NextId();
        TourGuest Add(TourGuest tourGuest);
        List<TourGuest> GetByTourReservationId(int tourReservationId);
        TourGuest Update(TourGuest tourGuest);
        void Delete(TourGuest tourGuest);
        void Subscribe(IObserver observer);
    }
}
