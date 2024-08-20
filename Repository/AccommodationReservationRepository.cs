using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Observer;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingApp.Repository
{
    public class AccommodationReservationRepository : IAccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationReservation.csv";
        private readonly Serializer<AccommodationReservation> serializer;
        private List<AccommodationReservation> accommodationReservations;
        public Subject AccommodationReservationSubject;

        public AccommodationReservationRepository()
        {
            serializer = new Serializer<AccommodationReservation>();
            accommodationReservations = serializer.FromCSV(FilePath);
            AccommodationReservationSubject = new Subject();
        }
        public List<AccommodationReservation> GetAll()
        {
            return accommodationReservations;
        }
        public void Subscribe(IObserver observer)
        {
            AccommodationReservationSubject.Subscribe(observer);
        }

        public int GenerateId() { 
            accommodationReservations=serializer.FromCSV(FilePath);
            if (accommodationReservations.Count < 1) {
                return 1;
            }
            return accommodationReservations.Max(accommodationReservation =>accommodationReservation.Id)+1;
        }

        public AccommodationReservation Add(AccommodationReservation accommodationReservation) {

            if (File.ReadLines(FilePath).Count() != 0)
            {
                accommodationReservations = serializer.FromCSV(FilePath);

            }
            else {
                accommodationReservations = new List<AccommodationReservation>();
            }
            accommodationReservation.Id=GenerateId();
            accommodationReservations.Add(accommodationReservation);
            serializer.ToCSV(FilePath, accommodationReservations);
            AccommodationReservationSubject.NotifyObservers();
            return accommodationReservation;
        }

        public void Update(AccommodationReservation reservation)
        {
            var oldReservation = GetById(reservation.Id);
            if (oldReservation is null) return;
            oldReservation.FirstDay = reservation.FirstDay;
            oldReservation.LastDay = reservation.LastDay;
            oldReservation.DaysToStay = reservation.DaysToStay;
            oldReservation.Status = reservation.Status;
            //PROSIRITI PO POTREBI

            serializer.ToCSV(FilePath, accommodationReservations);
            AccommodationReservationSubject.NotifyObservers();
        }

        public void Delete(AccommodationReservation accommodationReservation)
        {
            accommodationReservations = serializer.FromCSV(FilePath);
            AccommodationReservation? founded = accommodationReservations.Find(c => c.Id == accommodationReservation.Id);
            int index = accommodationReservations.IndexOf(founded);
            AccommodationReservation changed = founded;
            changed.Status = ReservationStatus.Canceled;
            accommodationReservations.Remove(founded);
            accommodationReservations.Insert(index, changed);
            serializer.ToCSV(FilePath, accommodationReservations);
            AccommodationReservationSubject.NotifyObservers();
        }
        public List<AccommodationReservation> GetAllActiveById(int id) {
            return accommodationReservations.FindAll(a => a.AccommodationId==id && a.Status==ReservationStatus.Active);
        }
        public AccommodationReservation? GetById(int id)
        {
            return accommodationReservations.Find(a => a.Id == id);
        }

        public List<AccommodationReservation> GetByAccommodationIds(IEnumerable<int> ids)
        {
            return accommodationReservations.Where(x => ids.Contains(x.AccommodationId)).ToList();
        }
        public List<AccommodationReservation> GetPastReservations(User user) {
            List<AccommodationReservation> allPastReservations=accommodationReservations.FindAll(x => x.LastDay < DateTime.Today);
            List<AccommodationReservation> result = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in allPastReservations)
            {
                if (accommodationReservation.UserId == user.Id)
                {
                    result.Add(accommodationReservation);
                }
            }
            return result;
        }
        public List<AccommodationReservation> GetUpcomingReservations(User user)
        {
            List<AccommodationReservation> allUpcomingReservations=accommodationReservations.FindAll(x => x.LastDay >= DateTime.Today && x.Status==ReservationStatus.Active);
            List<AccommodationReservation> result = new List<AccommodationReservation>();
            foreach(AccommodationReservation accommodationReservation in allUpcomingReservations)
            {
                if (accommodationReservation.UserId == user.Id) {
                    result.Add(accommodationReservation);
                }
            }
            return result;
        }
    }
}
