using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRatingNotificationRepository
    {
        void Add(RatingNotification ratingNotification);
        void Update(RatingNotification notification);
        void Remove(int id);
        RatingNotification? GetById(int id);
        List<RatingNotification> GetAll();
        bool ExistsWithReservationId(int id);
        void Subscribe(IObserver observer);
        void Notify();
    }
}
