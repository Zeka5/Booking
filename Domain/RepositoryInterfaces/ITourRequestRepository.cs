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
    public interface ITourRequestRepository
    {
        List<TourRequest> GetAll();
        TourRequest GetById(int id);
        int NextId();
        TourRequest Add(TourRequest tourRequest);
        TourRequest Update(TourRequest tourRequest);
        void Delete(TourRequest tourRequest);
    }
}
