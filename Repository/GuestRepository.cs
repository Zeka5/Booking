using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class GuestRepository : IGuestRepository
    {
        private const string FilePath = "../../../Resources/Data/guests.csv";
        private readonly Serializer<Guest> serializer;
        private List<Guest> guests;
        public Subject GuestSubject;

        public GuestRepository()
        {
            serializer = new Serializer<Guest>();
            guests = serializer.FromCSV(FilePath);
            GuestSubject = new Subject();
        }

        private int GenerateId()
        {
            if (guests.Count == 0) return 1;
            else return guests[^1].Id + 1;
        }

        public void Add(Guest guest)
        {
            guest.Id = GenerateId();
            guests.Add(guest);
            serializer.ToCSV(FilePath, guests);
            GuestSubject.NotifyObservers();
        }

        public List<Guest> GetAll()
        {
            return guests;
        }
        public Guest? GetById(int id)
        {
            return guests.Find(x => x.Id == id);
        }
        public void Update(Guest guest)
        {
            var oldGuest = GetById(guest.Id);
            if (oldGuest is null) return;
            oldGuest.SuperGuestConfigured = guest.SuperGuestConfigured;
            oldGuest.BonusPoints = guest.BonusPoints;
            oldGuest.Mode = guest.Mode;

            serializer.ToCSV(FilePath, guests);
            GuestSubject.NotifyObservers();
        }
    }
}
