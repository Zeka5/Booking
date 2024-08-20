using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRequestNotificationRepository
    {
        List<TourRequestNotification> GetAll();
        TourRequestNotification GetById(int id);
        int NextId();
        TourRequestNotification Add(TourRequestNotification tourRequestNotification);
        TourRequestNotification Update(TourRequestNotification tourRequestNotification);
        void Delete(TourRequestNotification tourRequestNotification);
    }
}
