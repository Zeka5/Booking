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
    public interface ITourRealizationRepository
    {
        List<TourRealization> GetAll();
        TourRealization GetById(int id);
        int NextId();
        TourRealization Add(TourRealization tourRealization);
        TourRealization Update(TourRealization tourRealization);
        void Delete(TourRealization tourRealization);
        List<TourRealization> GetByTourId(int id);
        void Subscribe(IObserver observer);
    }
}
