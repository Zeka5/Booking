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
    public interface ITourRepository
    {
        List<Tour> GetAll();
        Tour GetById(int id);
        int NextId();
        Tour Add(Tour tour);
        void Delete(Tour tour);
        Tour GetLast();
        Tour Update(Tour tour);
        void Subscribe(IObserver observer);
    }
}
