using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourReservationRepository
    {
        List<TourReservation> GetAll();
        TourReservation GetById(int id);
        TourReservation GetByTourRelizationIdAndUserId(int tourRealizationId, int userId);
        TourReservation GetByTourStartTimeId(int id);
        void Update(TourReservation tourReservation);
        int NextId();
        TourReservation Add(TourReservation tourReservation);
        void Delete(TourReservation tourReservation);
    }
}
