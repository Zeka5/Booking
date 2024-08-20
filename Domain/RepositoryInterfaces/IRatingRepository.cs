using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRatingRepository
    {
        void Add(Rating rating);
        List<Rating> GetAll();
        Rating? GetByReservationId(int id);
        void Subscribe(IObserver observer);
    }
}
