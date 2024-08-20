using BookingApp.Domain.Model;
using BookingApp.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGuestNotificationRepository
    {
        List<GuestNotification> GetAll();
        List<GuestNotification> GetAllNotRead();
        void Subscribe(IObserver observer);
        GuestNotification Add(GuestNotification guestNotification);
        void Delete(GuestNotification guestNotification);
    }
}
