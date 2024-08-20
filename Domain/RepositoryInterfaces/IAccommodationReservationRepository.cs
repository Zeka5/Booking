using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationReservationRepository
    {
        List<AccommodationReservation> GetAll();
        void Subscribe(IObserver observer);
        AccommodationReservation Add(AccommodationReservation accommodationReservation);
        void Update(AccommodationReservation reservation);
        void Delete(AccommodationReservation accommodationReservation);
        List<AccommodationReservation> GetAllActiveById(int id);
        AccommodationReservation? GetById(int id);
        List<AccommodationReservation> GetByAccommodationIds(IEnumerable<int> ids);
        List<AccommodationReservation> GetPastReservations(User user);
        List<AccommodationReservation> GetUpcomingReservations(User user);
    }
}
