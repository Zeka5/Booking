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
    public interface ITourRatingRepository
    {
        List<TourRating> GetAll();
        TourRating GetById(int id);
        int NextId();
        TourRating GetLast();
        TourRating Add(TourRating tourRating);
        TourRating Update(TourRating tourRating);
        void Delete(TourRating tourRating);
        void Subscribe(IObserver observer);
    }
}
