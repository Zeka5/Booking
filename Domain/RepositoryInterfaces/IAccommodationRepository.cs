using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationRepository
    {
        void Add(Accommodation accommodation);
        List<Accommodation> GetAll();
        Accommodation GetLast();
        List<Accommodation> GetByOwnerId(int id);
        Accommodation GetById(int id);
        bool GetIsCancelable(AccommodationReservation accommodationReservation);

        void Subscribe(IObserver observer);
    }
}
