using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IAccommodationRatingRepository
    {
        void Add(AccommodationRating accommodationRating);
        List<AccommodationRating> GetAll();
        bool GetIsRateable(AccommodationReservation accommodationReservation);
        void Subscribe(IObserver observer);
    }
}
