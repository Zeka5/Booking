using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class GuestService
    {
        private IGuestRepository guestRepository;
        public GuestService(IGuestRepository guestRepository)
        {
            this.guestRepository = guestRepository;
        }
        public Guest getById(int id) {
            return guestRepository.GetById(id);
        }
        public Guest getGuestMode(Guest guest) { 
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddYears(-1);
            if (guest.SuperGuestConfigured < dateTime) {
                guest.Mode = Mode.Guest;
                guest.BonusPoints = 0;
            }
            return guest;
        }
        public void Update(Guest guest) { 
            guestRepository.Update(guest);
        }
        public void RemoveBonusPoint(int id) { 
            Guest guest = guestRepository.GetById(id);
            if (guest.Mode == Mode.SuperGuest && guest.BonusPoints > 0) {
                guest.BonusPoints--;
            }
            Update(guest);
        }
    }
}
