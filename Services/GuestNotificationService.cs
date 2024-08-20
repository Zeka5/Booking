using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Dto;
using BookingApp.Observer;
using BookingApp.WPF.View;
using BookingApp.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class GuestNotificationService
    {
        private IGuestNotificationRepository guestNotificationRepository;
        private AccommodationReservationService accommodationReservationService;

        public GuestNotificationService(IGuestNotificationRepository guestNotificationRepository, AccommodationReservationService accommodationReservationService) { 
            this.guestNotificationRepository = guestNotificationRepository;
            this.accommodationReservationService = accommodationReservationService;
        }

        public GuestNotificationService(IGuestNotificationRepository guestNotificationRepository)
        {
            this.guestNotificationRepository = guestNotificationRepository;
        }

        public void Subscribe(IObserver observer) { 
            guestNotificationRepository.Subscribe(observer);
        }
        public void Add(GuestNotification guestNotification)
        {
            guestNotificationRepository.Add(guestNotification);
        }

        public void Delete(GuestNotification guestNotification)
        {
            guestNotificationRepository.Delete(guestNotification);
        }

        public List<GuestNotification> GetAllNotReadByUser(User user)
        {
            List<GuestNotification> guestNotificationsNotRead = guestNotificationRepository.GetAllNotRead();
            List<GuestNotification> result = new List<GuestNotification>();
            foreach (GuestNotification guestNotification in guestNotificationsNotRead)
            {
                AccommodationReservation? accommodationReservation = accommodationReservationService.GetById(guestNotification.AccommodationReservationId);
                if (accommodationReservation.UserId == user.Id)
                {
                    result.Add(guestNotification);
                }
            }
            return result;
        }

    }
}
